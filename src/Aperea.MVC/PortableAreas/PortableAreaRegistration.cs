using System;
using System.IO;
using System.Web.Mvc;
using Aperea.Messaging;
using Microsoft.Practices.ServiceLocation;

// based on http://mvccontrib.codeplex.com/

namespace Aperea.MVC.PortableAreas
{
    public abstract class PortableAreaRegistration : AreaRegistration
    {
        public static Action CheckAreasWebConfigExists = () => { EnsureAreasWebConfigExists(); };

        protected virtual string AreaRoutePrefix
        {
            get { return AreaName; }
        }

        protected virtual void RegisterArea(AreaRegistrationContext context, IMessageBus bus)
        {
            bus.Send(new PortableAreaStartupMessage(AreaName));

            RegisterDefaultRoutes(context);

            RegisterAreaEmbeddedResources();
        }

        public void CreateStaticResourceRoute(AreaRegistrationContext context, string subfolderName)
        {
            context.MapRoute(AreaName + "-" + subfolderName,
                             AreaRoutePrefix + "/" + subfolderName + "/{resourceName}",
                             new
                             {
                                 controller = "EmbeddedResource",
                                 action = "Index",
                                 resourcePath = "Content." + subfolderName
                             },null);
        }

        protected string GetNamespace()
        {
            return GetType().Namespace;
        }

        protected virtual void RegisterDefaultRoutes(AreaRegistrationContext context)
        {
            CreateStaticResourceRoute(context, "images");
            CreateStaticResourceRoute(context, "styles");
            CreateStaticResourceRoute(context, "scripts");
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterArea(context, ServiceLocator.Current.GetInstance<IMessageBus>());
            CheckAreasWebConfigExists();
        }

        void RegisterAreaEmbeddedResources()
        {
            var resourceStore = new AssemblyResourceStore(GetType(),  AreaRoutePrefix + "/", GetNamespace());
            AssemblyResourceManager.RegisterAreaResources(resourceStore);
        }

        static void EnsureAreasWebConfigExists()
        {
            var config = System.Web.HttpContext.Current.Server.MapPath("~/areas/web.config");
            if (!File.Exists(config))
            {
                throw new Exception(
                    "Portable Areas require a ~/Areas/Web.config file in your host application. Copy the config from ~/views/web.config into a ~/Areas/ folder.");
            }
        }
    }
}