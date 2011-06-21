﻿using System;
using System.Web.Mvc;
using Aperea.MVC.Areas.Account.Controllers;
using Aperea.MVC.PortableAreas;

namespace Aperea.MVC.Areas.Account
{
    public class MembershipAreaRegistration : PortableAreaRegistration
    {
        public override string AreaName
        {
            get { return "account"; }
        }


        protected override void RegisterDefaultRoutes(AreaRegistrationContext context)
        {
            base.RegisterDefaultRoutes(context);
            context.MapRoute("Account" + "-Default"
                 , "{culture}/"+AreaRoutePrefix+"/{controller}/{action}/{id}",
                 new
                 {
                     action = "Index",
                     id = UrlParameter.Optional
                 },
                 null);


            context.MapRoute(
                AreaName + "HashKey",
                "{culture}/{area}/{controller}/{action}/{hash}/{key}"
                );
        }
    }
}