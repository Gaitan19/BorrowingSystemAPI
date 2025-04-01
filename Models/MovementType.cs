namespace BorrowingSystemAPI.Models
{
    public class MovementType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }  
        public int Operation { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
