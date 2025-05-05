namespace BorrowingSystemAPI.DTOs
{
    public class MovementDTO
    {
        public Guid ItemId { get; set; }
        public Guid? MovementTypeId { get; set; }
        public DateTime? MovementDate { get; set; }
        public int Quantity { get; set; }
    }
}
