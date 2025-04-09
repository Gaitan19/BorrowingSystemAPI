using System;

namespace BorrowingSystemAPI.Exceptions
{
    public enum ErrorCode
    {
        NotFound,
        BadRequest,
        Unauthorized,
        Forbidden,
        InternalServerError
    }

    public class ServiceException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public ServiceException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
