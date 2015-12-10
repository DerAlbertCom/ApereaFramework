using AutoMapper;
using StructureMap;

namespace Aperea.Infrastructure.IoC
{
    [UsedImplicitly]
    public class RegistryForAutoMapper : Registry
    {
        public RegistryForAutoMapper()
        {
            Scan(s =>
            {
                s.AssembliesForApplication();
                s.AddAllTypesOf<Profile>();
            });
            For<IMappingEngine>().Use(c => Mapper.Engine);
        }
    }
}