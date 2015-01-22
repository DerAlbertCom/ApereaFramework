using System;

namespace Aperea.CQRS.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}