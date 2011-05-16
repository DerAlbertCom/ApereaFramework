using Aperea.EntityModels;
using Machine.Specifications;

namespace Aperea.Specs.EntityModels
{
    [Subject(typeof (SystemLanguage))]
    public class When_a_language_with_the_culture_de_de_is_created
    {
        Establish that =
            () => system_language = new SystemLanguage("de");

        It should_displayname_is_deutsch =
            () => system_language.DisplayName.ShouldEqual("Deutsch");

        static SystemLanguage system_language;
    }

    [Subject(typeof (SystemLanguage))]
    public class When_a_language_with_the_culture_en_is_created
    {
        Establish context =
            () => system_language = new SystemLanguage("en");


        It should_displayname_is_english =
            () => system_language.DisplayName.ShouldEqual("English");

        static SystemLanguage system_language;
    }

    [Subject(typeof (SystemLanguage))]
    public class When_a_language_with_the_culture_de_is_created_and_changed_to_en
    {
        Establish that = () => system_language = new SystemLanguage("de");

        Because of = () => system_language.Culture = "en";

        It should_displayname_is_english =
            () => system_language.DisplayName.ShouldEqual("English");

        static SystemLanguage system_language;
    }
}