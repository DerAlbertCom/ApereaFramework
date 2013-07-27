using System.IdentityModel.Metadata;

namespace Aperea.Identity.Settings
{
    public interface IMetadataContactSettings
    {
        ContactPerson Contact { get; }
        Organization Organization { get; }
    }
}