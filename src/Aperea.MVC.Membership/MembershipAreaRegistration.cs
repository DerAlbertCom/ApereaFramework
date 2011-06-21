using System;
using Aperea.MVC.Areas.Membership.Controllers;
using Aperea.MVC.PortableAreas;

namespace Aperea.MVC.Areas.Membership
{
    public class MembershipAreaRegistration : PortableAreaRegistration
    {
        public override string AreaName
        {
            get { return "Membership"; }
        }

        protected override string AreaRoutePrefix
        {
            get { return "account"; }
        }

        protected override Type ControllerType
        {
            get { return typeof (LogonController); }
        }
    }
}