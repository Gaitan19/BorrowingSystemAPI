using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystemAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly BorrowingContext _context;

        public AuthRepository(BorrowingContext context)
        {
            _context = context;
        }

        public  AuthDTO? Login(string Email, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null) return null;

            if (!VerifyPasswordHash(Password, user.Password)) return null;

            var authUser = new AuthDTO {
                Id = user.Id,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role
            };

            return authUser;
        }

        private bool VerifyPasswordHash(string password1, string password2)
        {
            if (password1 != password2) return false;
            return true;
        }

        public  User Register(User user)
        {
            var newUser = _context.Users.Add(user);
            _context.SaveChanges();
            return newUser.Entity;
        }
        public async Task<bool> UserExists(string Email)
        {
            if(await _context.Users.AnyAsync(u => u.Email == Email)) return true;

            return false;

        }
    }
}
