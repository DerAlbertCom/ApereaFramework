namespace Aperea.Settings
{
    public interface IMembershipSettings
    {
        string AdministratorLogin { get; }
        string AdministratorEMail { get; }
        string AdministratorPassword { get; }
    }
}