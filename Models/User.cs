namespace BorrowingSystemAPI.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<Request> Requests { get; set; } = new List<Request>();
    }
}
