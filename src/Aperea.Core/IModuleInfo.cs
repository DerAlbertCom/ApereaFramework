namespace Aperea
{
    public interface IModuleInfo
    {
        string Name { get; }
        string Description { get; }
        int Version { get; }
        string Website { get; }
    }
}