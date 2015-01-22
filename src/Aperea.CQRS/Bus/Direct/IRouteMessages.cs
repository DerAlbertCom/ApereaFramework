namespace Aperea.CQRS.Bus.Direct
{
    public interface IRouteMessages
    {
        void Route(object message);
    }
}