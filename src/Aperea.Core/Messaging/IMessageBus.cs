namespace Aperea.Messaging
{
    public interface IMessageBus
    {
        void Publish(object message);
        void Send(object message);
    }
}