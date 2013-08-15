using System.Linq;

namespace Aperea.Data
{
    public interface IQuery<out T>  : IQueryable<T> where T : class 
    {
         
    }
}