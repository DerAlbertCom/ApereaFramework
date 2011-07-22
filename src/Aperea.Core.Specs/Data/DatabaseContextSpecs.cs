using Aperea.Infrastructure.Data;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Data
{
    [Subject(typeof(DatabaseContext),"Creating")]
    public class When_getting_the_DbContext_from_the_DatabaseContext : WithSubject<DatabaseContext>
    {
        Because of = () =>
        {
            var a = Subject.DbContext;
        };

        It should_call_the_dbcontextfactory = ()=> The<IDocumentSessionFactory>().WasToldTo(cf => cf.CreateDocumentSession());
    }
}