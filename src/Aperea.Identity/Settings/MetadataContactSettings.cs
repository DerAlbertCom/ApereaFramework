using System;
using System.Globalization;
using System.IdentityModel.Metadata;

namespace Aperea.Identity.Settings
{
    public class MetadataContactSettings : IMetadataContactSettings
    {
        public ContactPerson Contact
        {
            get
            {
                var contactPerson = new ContactPerson(ContactType.Administrative)
                {
                    Company = "dotnet Köln/Bonn e.V.",
                    GivenName = "Albert",
                    Surname = "Weinert"
                };

                contactPerson.EmailAddresses.Add("albert.weinert@dotnet-koelnbonn.de");
                contactPerson.EmailAddresses.Add("info@dotnet-koelnbonn.de");
                return contactPerson;
            }
        }

        public Organization Organization
        {
            get
            {
                var invariantCulture = CultureInfo.InvariantCulture;
                var organization = new Organization();
                organization.DisplayNames.Add(new LocalizedName("dotnet Köln/Bonn e.V.", invariantCulture));
                organization.Names.Add(new LocalizedName("dotnet Köln/Bonn e.V.", invariantCulture));
                organization.Urls.Add(new LocalizedUri(new Uri("http://dotnet-koelnbonn.de"), invariantCulture));
                return organization;
            }
        }
    }
}