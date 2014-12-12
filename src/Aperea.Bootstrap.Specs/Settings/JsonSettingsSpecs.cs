using Aperea.Settings;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Bootstrap.Specs.Settings
{
    public class BaseJsonSettings : WithSubject<JsonSettings>
    {
        Establish context = () =>
        {
            var reader = The<ISettingJsonReader>();
            reader.WhenToldTo(r => r.Load()).Return(@"{
    ""appSettings"": {
        ""setting1"": ""foo"",
        ""setting2"": ""bar""
    },
    ""connectionStrings"": {
        ""serviceBus"": ""theConnectedString"",
        ""sqlServer"": {
            ""connectionString"": ""theStringIsConnected"",
            ""providerName"" :  ""theNameOfTheProvider""
        }
    }
}");
        };
    }

    public class When_getting_setting1 : BaseJsonSettings
    {
        Because of = () => result = Subject.Get("setting1", () => "");
        It should_the_value_is_foo = () => result.Should().Be("foo");
        static string result;
    }

    public class When_getting_setting2 : BaseJsonSettings
    {
        Because of = () => result = Subject.Get("setting2", () => "");
        It should_the_value_is_bar = () => result.Should().Be("bar");
        static string result;
    }

    public class When_getting_serviceBus_connectionString : BaseJsonSettings
    {
        Because of = () => result = Subject.GetConnectionString("serviceBus");

        It should_the_connectionString_is_theConnectedString =
            () => result.ConnectionString.Should().Be("theConnectedString");

        It should_the_providerName_isEmpty = () => result.ProviderName.Should().Be("");
        static Connection result;
    }

    public class When_getting_sqlServer_connectionString : BaseJsonSettings
    {
        Because of = () => result = Subject.GetConnectionString("sqlServer");

        It should_the_connectionString_is_theStringIsConnected =
            () => result.ConnectionString.Should().Be("theStringIsConnected");

        It should_the_providerName_is_theNameOfTheProvider =
            () => result.ProviderName.Should().Be("theNameOfTheProvider");

        static Connection result;
    }
}