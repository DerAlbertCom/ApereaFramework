namespace Aperea.Services
{
    public interface ISendMail
    {
        void Send(string recipient, string subject, string body);
    }
}