namespace Aperea.Settings
{
    public interface IDatabaseSettings
    {
        string DatabaseName { get; }
        string ConnectionStringName { get; }
    }
}