namespace BorrowingSystemAPI.Models
{
    public enum MovementType
    {
        Entry,
        Exit
    }

    public class ItemMovement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
