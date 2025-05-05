using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper; 

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User? GetUserById(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public User CreateUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto); 
            return _userRepository.CreateUser(user);
        }

        public User? UpdateUser(Guid id,UserDTO userDto)
        {

            var existingUser = _userRepository.GetUserById(id);

            if (existingUser == null) return null; 

            _mapper.Map(userDto, existingUser );

            return _userRepository.UpdateUser(existingUser);
        }

        public void DeleteUser(Guid id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}
