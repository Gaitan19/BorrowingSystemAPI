using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public AuthDTO? Login(string email, string password)
        {
            return _authRepository.Login(email, password);
        }

        public User? Register(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return _authRepository.Register(user);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _authRepository.UserExists(email);
        }
    }
}
