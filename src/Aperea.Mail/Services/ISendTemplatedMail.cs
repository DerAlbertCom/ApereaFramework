namespace Aperea.Services
{
    public interface ISendTemplatedMail
    {
        void SendMail<T>(string recipient, string templateName, T model);
    }
}