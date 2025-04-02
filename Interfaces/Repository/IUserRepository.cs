using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(Guid id);
        User CreateUser(User user);
        User UpdateUser(User user);

        void DeleteUser(Guid id);

    }
}
