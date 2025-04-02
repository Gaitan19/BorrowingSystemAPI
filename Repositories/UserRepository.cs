using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly BorrowingContext _context;

        public UserRepository(BorrowingContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            var newUser = _context.Users.Add(user);
            _context.SaveChanges();
            return newUser.Entity;

        }

        public void DeleteUser(Guid id)
        {
            var userDeleted = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userDeleted != null)
            {
                userDeleted.DeletedAt = DateTime.Now;
                _context.Users.Update(userDeleted);
                _context.SaveChanges();
            }

        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }


        public User UpdateUser(User user)
        {
            var updatedUser = _context.Users.Update(user);
            _context.SaveChanges();

            return updatedUser.Entity;
        }
    }
}
