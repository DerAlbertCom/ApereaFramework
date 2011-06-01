using Aperea.EntityModels;
using Aperea.MVC.Security;
using Aperea.Repositories;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.MVC.Specs.Security
{
    public class When_creating_an_principal_for_a_onegroup_login : WithSubject<object>
    {
        Establish that = () =>
        {
            With<BehaviorApereaPrincipal>();
            var login = new Login("theuser", "foo");
            var group = new LoginGroup("admin");
            group.AddRole(new SecurityRole("Administrator"));
            login.AddGroup(group);
            login.Confirm();
            The<IRepository<Login>>().Add(login);
            The<IRepository<Login>>().SaveAllChanges();
            principal = new ApereaPrincipal(new Login("theuser", ""));
        };

        It should_has_the_adminstrator_role = () => principal.IsInRole("Administrator").ShouldBeTrue();
        It should_has_not_the_user_role = () => principal.IsInRole("User").ShouldBeFalse();
        It should_has_not_the_authorized_role = () => principal.IsInRole("Authorized").ShouldBeFalse();

        static ApereaPrincipal principal;
    }

    public class When_creating_an_principal_for_a_twogroup_login : WithSubject<object>
    {
        Establish that = () =>
        {
            With<BehaviorApereaPrincipal>();

            var login = new Login("theuser", "foo");
            var adminGroup = new LoginGroup("admin");
            adminGroup.AddRole(new SecurityRole("Administrator"));
            var userGroup = new LoginGroup("user");
            userGroup.AddRole(new SecurityRole("User"));
            login.AddGroup(adminGroup);
            login.AddGroup(userGroup);
            login.Confirm();
            The<IRepository<Login>>().Add(login);
            The<IRepository<Login>>().SaveAllChanges();
            principal = new ApereaPrincipal(new Login("theuser", ""));
        };

        It should_has_the_adminstrator_role = () => principal.IsInRole("Administrator").ShouldBeTrue();
        It should_has_the_user_role = () => principal.IsInRole("User").ShouldBeTrue();
        It should_has_not_the_authorized_role = () => principal.IsInRole("Authorized").ShouldBeFalse();
        static ApereaPrincipal principal;
    }

    public class When_creating_an_principal_for_a_twogroup_login_and_three_roles : WithSubject<object>
    {
        Establish that = () =>
        {
            With<BehaviorApereaPrincipal>();

            var login = new Login("theuser", "foo");
            var adminGroup = new LoginGroup("admin");
            adminGroup.AddRole(new SecurityRole("Administrator"));
            var userGroup = new LoginGroup("user");
            userGroup.AddRole(new SecurityRole("User"));
            userGroup.AddRole(new SecurityRole("Authorized"));
            login.AddGroup(adminGroup);
            login.AddGroup(userGroup);
            login.Confirm();
            The<IRepository<Login>>().Add(login);
            The<IRepository<Login>>().SaveAllChanges();
            principal = new ApereaPrincipal(new Login("theuser", ""));
        };

        It should_has_the_adminstrator_role = () => principal.IsInRole("Administrator").ShouldBeTrue();
        It should_has_the_user_role = () => principal.IsInRole("User").ShouldBeTrue();
        It should_has_the_authorized_role = () => principal.IsInRole("Authorized").ShouldBeTrue();
        static ApereaPrincipal principal;
    }
}