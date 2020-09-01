using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Semdelion.DAL.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly Uri _baseUri = new Uri("https://www.hltv.org/");

        public Lazy<HttpClient> _lazyHttpClient { get; }

        public IDictionary<string, IEnumerable<string>> Headers { get; }

        public ConnectionService(Func<HttpMessageHandler> httpHandlerFunc)
        {
            _lazyHttpClient = new Lazy<HttpClient>(() => new HttpClient(httpHandlerFunc()));
            _lazyHttpClient.Value.BaseAddress = _baseUri;
            Headers = new Dictionary<string, IEnumerable<string>>();
        }

        public Task<HttpResponseMessage> SendAsync(string url,
                                                   HttpMethod method,
                                                   HttpContent httpContent = null,
                                                   IDictionary<string, IEnumerable<string>> headers = null,
                                                   IDictionary<string, object> properties = null,
                                                   CancellationToken cancellationToken = default)
        {
            var requestMessage = new HttpRequestMessage 
            { 
                RequestUri = new Uri(url), 
                Method = method, 
                Content = httpContent 
            };

            AddRequestHeaders(requestMessage, headers);

            return _lazyHttpClient.Value.SendAsync(requestMessage, cancellationToken);
        }

        public Task<HttpResponseMessage> POST(string url,
                                              HttpContent httpContent,
                                              IDictionary<string, IEnumerable<string>> headers = null,
                                              IDictionary<string, object> properties = null,
                                              CancellationToken cancellationToken = default) =>
            SendAsync(url, HttpMethod.Post, httpContent, headers, properties, cancellationToken);

        public Task<HttpResponseMessage> GET(string url,
                                             IDictionary<string, IEnumerable<string>> headers = null,
                                             IDictionary<string, object> properties = null,
                                             CancellationToken cancellationToken = default) =>
            SendAsync(url, HttpMethod.Get, null, headers, properties, cancellationToken);

        public void AddHeader(string key, IEnumerable<string> value) => Headers.Add(key, value);

        public void RemoveHeader(string key) => Headers.Remove(key);

        private void AddRequestHeaders(HttpRequestMessage requestMessage, IDictionary<string, IEnumerable<string>> headers)
        {
            IDictionary<string, IEnumerable<string>> allHeaders = null;

            if (Headers == null || headers == null)
                allHeaders = Headers ?? headers;
            else
                allHeaders = headers?.Concat(Headers.Where(x => !headers.Keys.Contains(x.Key))).ToDictionary(x => x.Key, x => x.Value);

            foreach (var pair in allHeaders)
                requestMessage.Headers.Add(pair.Key, pair.Value);
        }

    }
}
