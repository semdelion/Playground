namespace Semdelion.DAL.Enums
{
    public enum RequestStatus
    {
		Unknown = 0,
		Ok = 200,
		NotModified = 304,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		NotFound = 404,
		InternalServerError = 500,
		ServiceUnavailable = 503,
		Canceled = 1001,
		InvalidRequest = 1002,
		SerializationError = 1003,
		DatabaseError = 1100,
		FileAccessError = 2001
	}
}
