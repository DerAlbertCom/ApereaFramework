using System;

namespace Aperea.CQRS.Commands
{
    [Serializable]
    public class Command : ICommand
    {
        public Guid Id { get; private set; }

        public Command(Guid id)
        {
            Id = id;
        }

        public Command():this(Guid.NewGuid())
        {
            
        }
    }
}