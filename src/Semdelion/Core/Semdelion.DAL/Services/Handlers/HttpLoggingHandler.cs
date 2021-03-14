using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Semdelion.DAL.Services.Handlers
{
    public class HttpLoggingHandler: DelegatingHandler
    {
        public HttpLoggingHandler(HttpMessageHandler httpMessageHandler)
            : base(httpMessageHandler)
        {
        }
    }
}
