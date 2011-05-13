namespace Aperea.Services
{
    public interface IHashing
    {
        string GetHash(string text, string salt);
    }
}