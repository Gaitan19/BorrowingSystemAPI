using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.DTOs
{
    public class RequestDTO
    {
        public string Description { get; set; }
        public Guid RequestedByUserId { get; set; }
        public RequestStatus? RequestStatus { get; set; }  
        public ReturnStatus? ReturnStatus { get; set; }  
        public DateTime? RequestDate { get; set; }  
        public List<RequestItemDTO> RequestItems { get; set; } = new List<RequestItemDTO>();
    }
}
