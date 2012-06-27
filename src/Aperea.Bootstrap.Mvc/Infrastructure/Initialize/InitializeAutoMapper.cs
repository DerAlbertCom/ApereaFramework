using System;
using System.Collections.Generic;
using Aperea.Infrastructure.Bootstrap;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Infrastructure.Initialize
{
    public class InitializeAutoMapper : BootstrapItem
    {
        readonly IEnumerable<Profile> mappingProfiles;

        public InitializeAutoMapper(IEnumerable<Profile> mappingProfiles)
        {
            this.mappingProfiles = mappingProfiles;
        }

        public override void Execute()
        {
            Mapper.Initialize(c =>
                {
                    c.ConstructServicesUsing(ServiceLocator.Current.GetInstance);
                    foreach (var profile in mappingProfiles)
                    {
                        c.AddProfile(profile);
                    }
                });
        }
    }
}