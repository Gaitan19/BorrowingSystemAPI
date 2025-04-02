using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs
{
    public class AuthDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

      
        public string Email { get; set; }

      
        public string Password { get; set; }

       
        public string Role { get; set; }

        public string Token { get; set; }


    }
}
