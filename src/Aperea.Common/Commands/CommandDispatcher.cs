using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceLocator locator;

        public CommandDispatcher(IServiceLocator locator)
        {
            this.locator = locator;
        }

        public IEnumerable<ICommandValidator> GetValidators(ICommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var validatorType = typeof (ICommandValidator<>).MakeGenericType(command.GetType());
            return locator.GetAllInstances(validatorType).Cast<ICommandValidator>();
        }

        public IEnumerable<ICommandHandler> GetHandlers(ICommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
            var commandType = command.GetType();
            var handlerType = typeof (ICommandHandler<>).MakeGenericType(commandType);
            return locator.GetAllInstances(handlerType).Cast<ICommandHandler>();
        }
    }
}