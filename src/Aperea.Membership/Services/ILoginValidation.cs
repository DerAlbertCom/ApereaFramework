
namespace Aperea.Services
{
    public interface ILoginValidation
    {
        bool ValidateLoginForLogon(string loginName, string password);
        bool IsValidLogin(string loginName);
    }
}