namespace Aperea.Messaging
{
    public interface  IMessageHandler
    {
        
    }
    public interface IHandler<in TMessage> : IMessageHandler
    {
        void Handle(TMessage confirmendMessage);
    }
}