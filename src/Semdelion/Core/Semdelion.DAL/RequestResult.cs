
using System;
using System.Net;
using System.Net.Http;

namespace Semdelion.DAL
{
	public class RequestResult<T> where T : class
	{
		public readonly string Message;
		public readonly HttpStatusCode StatusCode = HttpStatusCode.OK;
		public readonly Exception Exception;
		public readonly T Data;
		public bool IsValid => StatusCode == HttpStatusCode.OK && Data != null && Exception == null;

		public RequestResult(T data)
		{
			Data = data;
		}

		public RequestResult(T data, HttpStatusCode statusCode, string message = null)
		{
			Data = data;
			StatusCode = statusCode;
			Message = message;
		}

		public RequestResult(Exception exception, string message = null)
		{
			Message = message;
			Exception = exception ?? throw new ArgumentNullException(nameof(exception));
		}

		public RequestResult(Exception exception, HttpStatusCode statusCode, string message)
		{
			StatusCode = statusCode;
			Message = message;
			Exception = exception ?? throw new ArgumentNullException(nameof(exception));
		}

		public override string ToString() { return $@"Result: {StatusCode}, Data: {Data}, Message: {Message}, Exception: {Exception}"; }

	}
}
