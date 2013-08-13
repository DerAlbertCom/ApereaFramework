using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Commands
{
    [UsedImplicitly]
    public sealed class CommandExecutor : ICommandExecutor
    {
        private static readonly Lazy<ICommandExecutor> TheExecutor =
            new Lazy<ICommandExecutor>(() => ServiceLocator.Current.GetInstance<ICommandExecutor>());

        public static void Execute(params ICommand[] commands)
        {
            TheExecutor.Value.ExecuteCommands(commands);
        }

        public static IEnumerable<ValidationResult> Validate(params ICommand[] commands)
        {
            return TheExecutor.Value.ValidateCommands(commands);
        }

        private class Executor
        {
            public readonly Action<ICommandHandler, ICommand> ExecuteHandler;

            private readonly PropertyInfo resultProperty;
            private readonly MethodInfo executeMethod;
            private readonly MethodInfo executeResultMethod;

            public Executor(Type commandType, Type handlerType)
            {
                resultProperty = commandType.GetProperty("Result");
                if (resultProperty != null)
                {
                    var resultHandlerType = typeof (ICommandHandler<,>).MakeGenericType(commandType,
                        resultProperty.PropertyType);
                    if (resultHandlerType.IsAssignableFrom(handlerType))
                    {
                        executeResultMethod = resultHandlerType.GetMethod("Execute");
                    }
                }
                executeMethod = handlerType.GetMethod("Execute");

                if (executeResultMethod != null)
                {
                    ExecuteHandler =
                        (handler, command) =>
                            resultProperty.SetValue(command, executeResultMethod.Invoke(handler, new object[] {command}));
                }
                else
                {
                    ExecuteHandler = (handler, command) => executeMethod.Invoke(handler, new object[] {command});
                }
            }
        }

        private readonly ICommandDispatcher dispatcher;

        public CommandExecutor(ICommandDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public IEnumerable<ValidationResult> ValidateCommands(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                var validators = dispatcher.GetValidators(command);
                foreach (var validator in validators)
                {
                    foreach (var validationResult in validator.Validate(command))
                    {
                        yield return validationResult;
                    }
                }
            }
        }

        public void ExecuteCommands(IEnumerable<ICommand> commands)
        {
            var arrayCommands = commands.ToArraySafe();
            var validationResults = ValidateCommands(arrayCommands).ToArraySafe();

            if (validationResults.Any())
            {
                throw new CommandValidationException(validationResults);
            }

            foreach (var command in arrayCommands)
            {
                var handlers = dispatcher.GetHandlers(command);
                foreach (var handler in handlers)
                {
                    var executorInfo = GetExecutorInfo(command, handler);
                    executorInfo.ExecuteHandler(handler, command);
                }
            }
        }

        private static readonly ConcurrentDictionary<string, Executor> Executors = new ConcurrentDictionary<string, Executor>();

        private Executor GetExecutorInfo(ICommand command, ICommandHandler handler)
        {
            var commandHandlerName = string.Format("{0}+{1}", command.GetType().FullName, handler.GetType().FullName);
            return Executors.GetOrAdd(commandHandlerName, CreateExecutor(command, handler));
        }

        private static Executor CreateExecutor(ICommand command, ICommandHandler handler)
        {
            var commandType = command.GetType();
            foreach (var interfaceType in commandType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && typeof (ICommand).IsAssignableFrom(interfaceType))
                {
                    var resultType = interfaceType.GetGenericArguments()[0];
                    var handlerType = typeof (ICommandHandler<,>).MakeGenericType(commandType, resultType);
                    if (handlerType.IsInstanceOfType(handler))
                    {
                        return new Executor(commandType, handlerType);
                    }
                }
                else if (typeof (ICommand).IsAssignableFrom(interfaceType))
                {
                    var handlerType = typeof (ICommandHandler<>).MakeGenericType(commandType);
                    return new Executor(commandType, handlerType);
                }
            }
            throw new InvalidOperationException(
                "don't create CommandHandlers which derives directory from ICommandHandler");
        }
    }
}