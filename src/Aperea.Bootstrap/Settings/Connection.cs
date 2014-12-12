namespace Aperea.Settings
{
    public class Connection
    {
        public Connection(string connectionString)
        {
            ConnectionString = connectionString;
            ProviderName = string.Empty;
        }

        public Connection(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
        }

        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }
    }
}