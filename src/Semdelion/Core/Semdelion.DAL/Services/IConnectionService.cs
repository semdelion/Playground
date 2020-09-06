using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Semdelion.DAL.Services
{
    /// <summary>
    ///     Иинтерфейс объекта, который возвращает инстанс HttpClient.
    /// </summary>
    public interface IConnectionService
    {
        Lazy<HttpClient> _lazyHttpClient { get; }

        Task<HttpResponseMessage> SendAsync(string url,
                                            HttpMethod method,
                                            HttpContent httpContent = null,
                                            IDictionary<string, IEnumerable<string>> headers = null,
                                            IDictionary<string, object> properties = null,
                                            CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> POST(string url,
                                       HttpContent httpContent,
                                       IDictionary<string, IEnumerable<string>> headers = null,
                                       IDictionary<string, object> properties = null,
                                       CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> GET(string url,
                                      IDictionary<string, IEnumerable<string>> headers = null,
                                      IDictionary<string, object> properties = null,
                                      CancellationToken cancellationToken = default);

        void AddHeader(string key, IEnumerable<string> value);

        void RemoveHeader(string key);
    }
}
