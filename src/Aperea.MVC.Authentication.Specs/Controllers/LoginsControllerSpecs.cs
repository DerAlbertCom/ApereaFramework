using System.Web.Mvc;
using Aperea.MVC.Authentication.Controllers;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.MVC.Authentication.Specs.Controllers

{
    public class When_showing_the_list_of_logins : WithSubject<LoginsController>
    {
        Because of = () => result = Subject.All();

        It should_returen_a_view_result = () => result.ShouldBeOfType<ViewResult>();


        static ActionResult result;
    }
}