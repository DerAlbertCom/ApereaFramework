using Machine.Fakes;

namespace Aperea.Specs.Services
{
    public abstract class FakeSubject<T> : WithSubject<T> where T : class 
    {
        protected static IFakeAccessor Accessor 
        {
            get { return _specificationController; }
        }
    }
}