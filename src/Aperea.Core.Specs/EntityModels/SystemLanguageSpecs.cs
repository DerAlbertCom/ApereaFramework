using Aperea.EntityModels;
using Machine.Specifications;

namespace Aperea.Specs.EntityModels
{
    [Subject(typeof (SystemLanguage))]
    public class When_a_language_with_the_culture_de_de_is_created
    {
        private Establish that =
            () => system_language = new SystemLanguage("de");

        private It should_displayname_is_deutsch =
            () => system_language.DisplayName.ShouldEqual("Deutsch");

        private static SystemLanguage system_language;
    }

    [Subject(typeof (SystemLanguage))]
    public class When_a_language_with_the_culture_en_us_is_created
    {
        private Establish context =
            () => system_language = new SystemLanguage("en");


        private It should_displayname_is_english =
            () => system_language.DisplayName.ShouldEqual("English");

        private static SystemLanguage system_language;
    }
}