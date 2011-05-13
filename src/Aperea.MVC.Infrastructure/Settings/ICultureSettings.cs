namespace Aperea.MVC.Settings
{
    public interface ICultureSettings
    {
        string[] PossibleCultures { get; }
        string DefaultCulture { get; }
    }
}