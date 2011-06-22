using Aperea.EntityModels;
using Machine.Specifications;

namespace Aperea.Specs.EntityModels
{
    [Subject(typeof(SecurityRole),"Creating")]
    public class When_creating_a_security_role
    {
        Because of = () => role = new SecurityRole("The Role");

        It should_has_the_roleName_set =()=>role.RoleName.ShouldEqual("The Role");

        static SecurityRole role;
    }
}