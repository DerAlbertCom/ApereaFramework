using System;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Aperea.Infrastructure.IoC
{
    public class RegistryForAspNetMvc : Registry
    {
        public RegistryForAspNetMvc()
        {
            Scan(x =>
            {
                x.AssembliesForApplication();
                x.TheCallingAssembly();
                x.AddAllTypesOf<ModelValidatorProvider>();
                x.AddAllTypesOf<ModelMetadataProvider>();
                x.AddAllTypesOf<ValueProviderFactory>();
                x.AddAllTypesOf<IModelBinderProvider>();
                x.AddAllTypesOf<IControllerActivator>();
                x.AddAllTypesOf<IViewPageActivator>();
                x.AddAllTypesOf<IFilterProvider>();
                x.With(new ControllerRegistryConvention());
            });

            For<HttpContextBase>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Add(context => new HttpContextWrapper(HttpContext.Current));

            For<HttpRequestBase>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Add(context => context.GetInstance<HttpContextBase>().Request);

            For<HttpResponseBase>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Add(context => context.GetInstance<HttpContextBase>().Response);

            For<HttpSessionStateBase>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Add(context => context.GetInstance<HttpContextBase>().Session);

            For<HttpServerUtilityBase>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Add(context => context.GetInstance<HttpContextBase>().Server);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(RegisterStructureMap.Container));
        }
    }
}