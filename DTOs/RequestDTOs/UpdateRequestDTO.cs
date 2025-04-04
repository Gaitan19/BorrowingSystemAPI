namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class UpdateRequestDTO
    {
        public string Description { get; set; }
        public List<RequestItemDTO> RequestItems { get; set; }
    }
}
