using MvvmCross.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross;

namespace Semdelion.DAL.Services.Handlers
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        private IMvxLog _logger;
        protected IMvxLog Logger => _logger ??= Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor(nameof(HttpLoggingHandler));

        public HttpLoggingHandler(HttpMessageHandler httpMessageHandler)
            : base(httpMessageHandler) { }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Logger.Info($"[Request]\n" +
                $"==================Start====================\n" + 
                $"{request.Method} {request.RequestUri.PathAndQuery} {request.RequestUri.Scheme}/{request.Version}\n" + 
                $"Host: {request.RequestUri.Scheme}://{request.RequestUri.Host}");

            foreach (var header in request.Headers)
                Logger.Info($"{header.Key}: {string.Join(", ", header.Value)}");

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                    Logger.Info($"{header.Key}: {string.Join(", ", header.Value)}");

                if (request.Content is StringContent || IsTextBasedContentType(request.Headers) ||
                    IsTextBasedContentType(request.Content.Headers))
                {
                    var result = await request.Content.ReadAsStringAsync();
                    Logger.Info($"Content:\n{result}");
                }
            }

            var start = DateTime.Now;
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var end = DateTime.Now;

            Logger.Info($"Duration: {end - start}\n" +
                $"====================End====================");

            Logger.Info($"[Response]\n" +
                $"===================Start===================\n"+
                $"{request.RequestUri.Scheme.ToUpper()}/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}");

            foreach (var header in response.Headers)
                Logger.Info($"{header.Key}: {string.Join(", ", header.Value)}");

            if (response.Content != null)
            {
                foreach (var header in response.Content.Headers)
                    Logger.Info($"{header.Key}: {string.Join(", ", header.Value)}");

                if (response.Content is StringContent || IsTextBasedContentType(response.Headers) ||
                    IsTextBasedContentType(response.Content.Headers))
                {
                    start = DateTime.Now;
                    var result = await response.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    Logger.Info($"Content:\n" + 
                        $"{result}\n" + 
                        $"Duration: {end - start}");
                }
            }

            Logger.Info($"====================End====================");
            return response;
        }

        bool IsTextBasedContentType(HttpHeaders headers)
        {
            if (!headers.TryGetValues("Content-Type", out IEnumerable<string> values))
                return false;
            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }
    }
}
