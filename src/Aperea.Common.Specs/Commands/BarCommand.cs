using Aperea.Commands;

namespace Aperea.Common.Specs.Commands
{
    public class BarCommand : ICommand<string>
    {
        public int Executed;

        public string Result { get; set; }
    }
}