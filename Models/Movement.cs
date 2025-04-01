namespace BorrowingSystemAPI.Models
{

    public class Movement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ItemId { get; set; }
        public Guid MovementTypeId { get; set; }
        public Item Item { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; }
        public DateTime? DeletedAt { get; set; }
        public MovementType MovementType { get; set; }
    }
}
