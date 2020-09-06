using Refit;
using Semdelion.DAL.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Semdelion.DAL.Providers
{
    public abstract class BaseProvider
    {
        protected async Task<RequestResult<TResult>> PackageServiceResult<TResult>(Func<Task<RequestResult<TResult>>> serviceFunc) where TResult : class
        {
            try
            {
                return await serviceFunc();
            }
            catch (ApiException ex)
            {
                return new RequestResult<TResult>(ex, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return new RequestResult<TResult>(ex, ex.Message);
            }
        }
    }
}
