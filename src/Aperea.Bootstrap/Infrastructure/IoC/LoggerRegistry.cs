using StructureMap;
using StructureMap.Configuration.DSL;

namespace Aperea.Infrastructure.IoC
{
    public class LoggerRegistry : Registry
    {
        public LoggerRegistry()
        {
            For<ILogger>().Use<ILogger>("Logger with",CreateILogForSpecificType);
        }

        private ILogger CreateILogForSpecificType(IContext context)
        {
            return Logger.GetLogger(context.ParentType ?? context.GetType());
        }
    }
}