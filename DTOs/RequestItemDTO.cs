namespace BorrowingSystemAPI.DTOs
{
    public class RequestItemDTO
    {
        public Guid? Id { get; set; }  
        public Guid RequestId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
