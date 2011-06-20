using Aperea.MVC.PortableAreas;

namespace Aperea.MVC.Membership.Areas.Views
{
    public class MembershipAreaRegistration : PortableAreaRegistration
    {
        public override string AreaName
        {
            get { return "Membership"; }
        }
    }
}