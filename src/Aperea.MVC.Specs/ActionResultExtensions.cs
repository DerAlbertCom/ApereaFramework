using System.Web.Mvc;
using Machine.Specifications;

namespace Aperea.MVC.Specs
{
    public static class ActionResultExtensions
    {
         public static void ShouldBeViewResult(this ActionResult result)
         {
             result.ShouldBeOfType<ViewResult>();
         }
    }
}