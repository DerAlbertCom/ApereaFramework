using Aperea.EntityModels;

namespace Aperea.UrlBuilder
{
    public interface IRemoteActionUrlBuilder
    {
        string GetUrl(RemoteAction remoteAction);
    }
}