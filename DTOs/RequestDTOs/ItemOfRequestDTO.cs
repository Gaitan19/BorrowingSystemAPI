namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class ItemOfRequestDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
