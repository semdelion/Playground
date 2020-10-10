using System;
using System.Net;

namespace Semdelion.DAL
{

	public class RequestResult<T> : RequestResult 
	{
		public readonly T Data;
		public override bool IsValid => StatusCode == HttpStatusCode.OK && Data != null && Exception == null;

		public RequestResult(T data):base()
		{
			Data = data;
		}

		public RequestResult(Exception exception, string message = null) 
			: base(exception, message)
		{
		}

		public RequestResult(Exception exception, HttpStatusCode statusCode, string message) 
			: base(exception, statusCode, message)
		{
		}

		public override string ToString() { return $@"Result: {StatusCode}, Data: {Data}, Message: {Message}, Exception: {Exception}"; }
	}

	public class RequestResult
	{
		public readonly string Message;
		public readonly HttpStatusCode StatusCode;
		public readonly Exception Exception;
		public virtual bool IsValid => StatusCode == HttpStatusCode.OK && Exception == null;

		public RequestResult()
		{
			StatusCode = HttpStatusCode.OK;
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

		public override string ToString() { return $@"Result: {StatusCode}, Message: {Message}, Exception: {Exception}"; }
	}
}
