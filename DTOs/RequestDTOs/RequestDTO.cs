using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class RequestDTO
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public Guid RequestedByUserId { get; set; }
        public List<RequestItemDTO> RequestItems { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public ReturnStatus ReturnStatus { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
