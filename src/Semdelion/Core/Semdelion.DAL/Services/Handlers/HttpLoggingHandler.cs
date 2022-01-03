using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross;
using Microsoft.Extensions.Logging;

namespace Semdelion.DAL.Services.Handlers
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        private ILogger _logger;
        protected ILogger Logger => _logger ??= Mvx.IoCProvider.Resolve<ILoggerFactory>().CreateLogger(nameof(HttpLoggingHandler));

        public HttpLoggingHandler(HttpMessageHandler httpMessageHandler)
            : base(httpMessageHandler) { }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Logger.Log(LogLevel.Information, $"[Request]\n" +
                $"==================Start====================\n" + 
                $"{request.Method} {request.RequestUri.PathAndQuery} {request.RequestUri.Scheme}/{request.Version}\n" + 
                $"Host: {request.RequestUri.Scheme}://{request.RequestUri.Host}");

            foreach (var header in request.Headers)
                Logger.Log(LogLevel.Information, $"{header.Key}: {string.Join(", ", header.Value)}");

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                    Logger.Log(LogLevel.Information, $"{header.Key}: {string.Join(", ", header.Value)}");

                if (request.Content is StringContent || IsTextBasedContentType(request.Headers) ||
                    IsTextBasedContentType(request.Content.Headers))
                {
                    var result = await request.Content.ReadAsStringAsync();
                    Logger.Log(LogLevel.Information, $"Content:\n{result}");
                }
            }

            var start = DateTime.Now;
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var end = DateTime.Now;

            Logger.Log(LogLevel.Information, $"Duration: {end - start}\n" +
                $"====================End====================");

            Logger.Log(LogLevel.Information, $"[Response]\n" +
                $"===================Start===================\n"+
                $"{request.RequestUri.Scheme.ToUpper()}/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}");

            foreach (var header in response.Headers)
                Logger.Log(LogLevel.Information, $"{header.Key}: {string.Join(", ", header.Value)}");

            if (response.Content != null)
            {
                foreach (var header in response.Content.Headers)
                    Logger.Log(LogLevel.Information, $"{header.Key}: {string.Join(", ", header.Value)}");

                if (response.Content is StringContent || IsTextBasedContentType(response.Headers) ||
                    IsTextBasedContentType(response.Content.Headers))
                {
                    start = DateTime.Now;
                    var result = await response.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    Logger.Log(LogLevel.Information, $"Content:\n" + 
                        $"{result}\n" + 
                        $"Duration: {end - start}");
                }
            }

            Logger.Log(LogLevel.Information, $"====================End====================");
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
