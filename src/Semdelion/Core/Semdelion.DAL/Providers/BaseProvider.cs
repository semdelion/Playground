using System;
using System.Threading.Tasks;

namespace Semdelion.DAL.Providers
{
    public abstract class BaseProvider
    {
        protected Task<RequestResult<TResult>> RequestResult<TResult>(Func<Task<TResult>> apiFunc) where TResult : class
        {
            return null;
        }
    }
}
