using System.Collections.Generic;
using Aperea.EntityModels;
using Machine.Specifications;

namespace Aperea.Specs.EntityModels
{
    [Subject(typeof (LoginGroup), "Creating")]
    public class Whenn_creating_the_logingroup
    {
        Because of = () => loginGroup = new LoginGroup("The GroupName");

        It should_has_the_groupname_set = () => loginGroup.GroupName.ShouldEqual("The GroupName");

        It should_has_zero_roles = () => loginGroup.Roles.ShouldBeEmpty();

        static LoginGroup loginGroup;
    }

    [Subject(typeof (LoginGroup))]
    public class When_adding_a_role_to_the_logingroup
    {
        static SecurityRole role;

        Establish context = () =>
        {
            group = new LoginGroup("Administrator");
            role = new SecurityRole("Mail_Delete");
        };

        Because of = () => group.AddRole(role);

        It should_added_the_group_to_to = () => group.Roles.ShouldContainOnly(new[] {role});

        static LoginGroup group;
    }

    [Subject(typeof (LoginGroup))]
    public class When_adding_a_role_twite_to_the_logingroup
    {
        static SecurityRole role;

        Establish context = () =>
        {
            group = new LoginGroup("Administrator");
            role = new SecurityRole("Mail_Delete");
        };

        Because of = () =>
        {
            group.AddRole(role);
            group.AddRole(role);
        };

        It should_added_the_group_only_once = () => group.Roles.ShouldEqual(new[] {role});

        It should_has_only_one_entry_in_role = () => group.Roles.Count.ShouldEqual(1);

        static LoginGroup @group;
    }

    [Subject(typeof (LoginGroup))]
    public class When_remote_a_role_from_the_login_group
    {
        Establish context = () =>
        {
            group = new LoginGroup("Admini");
            role = new SecurityRole("Edit");
            group.AddRole(new SecurityRole("Show"));
            group.AddRole(new SecurityRole("Delete"));
            group.AddRole(role);
        };

        Because of = () => group.RemoveRole(role);

        It should_to_has_the_role_in_the_role_collection = () => group.Roles.Contains(role).ShouldBeFalse();

        static LoginGroup @group;
        static SecurityRole role;
    }

    [Subject(typeof (LoginGroup))]
    public class When_setting_a_list_of_roles_a_logingroup
    {
        Establish context = () =>
        {
            group = new LoginGroup("Admini");
            role = new SecurityRole("Edit");
            group.AddRole(new SecurityRole("Show"));
            group.AddRole(new SecurityRole("Delete"));
            group.AddRole(role);
            newRoles = new List<SecurityRole>(group.Roles);
            newRoles.Remove(role);
        };

        Because of = () => group.SetRoles(newRoles);
        It should_has_only_these_list_of_roles = () => group.Roles.ShouldContainOnly(newRoles);

        static LoginGroup @group;
        static SecurityRole role;
        static List<SecurityRole> newRoles;
    }
}