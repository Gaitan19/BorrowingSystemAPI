using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IAuthRepository
    {
        User? Register(User user);
        AuthDTO? Login(string Email, string Password);
        Task<bool> UserExists(string Email);
    }
}
