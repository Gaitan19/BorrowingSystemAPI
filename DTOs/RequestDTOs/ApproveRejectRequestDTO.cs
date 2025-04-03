namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class ApproveRejectRequestDTO
    {
        public Guid RequestId { get; set; }
        public bool IsApproved { get; set; }
    }
}
