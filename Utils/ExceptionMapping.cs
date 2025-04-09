using BorrowingSystemAPI.Exceptions;

namespace BorrowingSystemAPI.Utils
{
    static public class ExceptionMapping
    {
        static public int MapExceptionToControllers(ErrorCode ex)
        {
            return ex switch
            {
                ErrorCode.NotFound => 404,
                ErrorCode.BadRequest => 400,
                ErrorCode.Unauthorized => 401,
                ErrorCode.Forbidden => 403,
                _ => 500
            };
        }
    }
}
