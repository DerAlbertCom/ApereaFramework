namespace Aperea.Settings
{
    public interface ICultureSettings
    {
        string[] PossibleCultures { get; }
        string DefaultCulture { get; }
    }
}