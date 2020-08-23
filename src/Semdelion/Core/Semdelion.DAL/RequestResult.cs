using Semdelion.DAL.Enums;
using System.Net.Http;

namespace Semdelion.DAL
{
	public class RequestResult<T> where T : HttpContent
	{
		public readonly string Message;
		public readonly RequestStatus Status;
		public readonly T Data;
		public bool IsValid => Status == RequestStatus.Ok && Data != null;

		public RequestResult(T data, RequestStatus status, string message = null)
		{
			Data = data;
			Status = status;
			Message = message;
		}

		public override string ToString() { return $@"Result: {Status}, Data: {Data}, Message: {Message}"; }
	}
}
