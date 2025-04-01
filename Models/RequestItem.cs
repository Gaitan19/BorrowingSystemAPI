namespace BorrowingSystemAPI.Models
{
    public class RequestItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public Request Request { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
