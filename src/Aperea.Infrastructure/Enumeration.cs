using System.Resources;

namespace Aperea
{
    public abstract class Enumeration<T>
    {
        private readonly T _value;
        private readonly string _resultResourceStringName;

        protected Enumeration(T value, string resultResourceStringName)
        {
            _value = value;
            _resultResourceStringName = resultResourceStringName;
        }

        public static implicit operator T(Enumeration<T> e)
        {
            return e._value;
        }

        protected abstract ResourceManager Resource { get; }

        public string Text
        {
            get
            {
                if (_resultResourceStringName == null)
                    return _value.ToString();
                return Resource.GetString(_resultResourceStringName);
            }
        }
    }
}