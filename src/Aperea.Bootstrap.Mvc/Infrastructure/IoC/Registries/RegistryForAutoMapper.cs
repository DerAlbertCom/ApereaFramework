using System;
using AutoMapper;
using StructureMap.Configuration.DSL;

namespace Aperea.Infrastructure.IoC
{
    public class RegistryForAutoMapper : Registry
    {
        public RegistryForAutoMapper()
        {
            Scan(c =>
                {
                    c.AssembliesFromApplicationBaseDirectory(StructureMapAssemblyFilter.Filter);
                    c.AddAllTypesOf<Profile>();
                });

            For<IMappingEngine>().Use(c => Mapper.Engine);
        }
    }
}