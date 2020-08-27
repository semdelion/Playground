using Plugin.Connectivity.Abstractions;
using Semdelion.DAL.Exceptions;
using Semdelion.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Semdelion.DAL.Services.Filters
{
    /// <summary>
    ///     Фильтр проверки доступа к интернету.
    ///     Приоритет: 0.
    /// </summary>
    public class NetworkConnectionServiceFilter : IServiceFilter
    {
        private readonly IConnectivity connectivity;

        /// <summary>
        ///     Создает новый экземпляр класса <see cref="NetworkConnectionServiceFilter"/>.
        /// </summary>
        public NetworkConnectionServiceFilter(IConnectivity connectivity)
        {
            this.connectivity = connectivity;
        }

        /// <inheritdoc />
        public int? Order => 0;

        public Task<ServiceFilterResult> CanDoRequest(ServiceContext serviceContext, ApiMethodContext apiContext)
        {
            return this.CanDoRequest();
        }

        public Task<ServiceFilterResult> CanDoRequest()
        {
            var hasNetwork = this.connectivity.IsConnected;
            return Task.FromResult(hasNetwork
                ? ServiceFilterResult.Skip
                : ServiceFilterResult.Error(new NetworkConnectionException()));
        }

        /// <inheritdoc />
        public Task<ServiceFilterResult> ValidateResult<Response>(RequestResult<Response> requestResult, ApiMethodContext apiContext)
            where Response : class
        {
            return Task.FromResult(ServiceFilterResult.Skip);
        }

        /// <inheritdoc />
        public Task<ServiceFilterResult> TryResolveException(ServiceContext serviceContext, Exception exception,
            ApiMethodContext apiContext)
        {
            return Task.FromResult(ServiceFilterResult.Skip);
        }
    }
}
