using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
