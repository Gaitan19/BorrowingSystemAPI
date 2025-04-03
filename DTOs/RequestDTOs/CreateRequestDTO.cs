namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class CreateRequestDTO
    {
        public string Description { get; set; }
        public Guid RequestedByUserId { get; set; }
        public List<RequestItemDTO> RequestItems { get; set; }
    }
}
