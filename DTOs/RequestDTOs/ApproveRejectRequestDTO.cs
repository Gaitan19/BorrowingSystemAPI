using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class ApproveRejectRequestDTO
    {

        [Required(ErrorMessage = "Request ID is required")]
        public Guid RequestId { get; set; }

        [Required]
        public bool IsApproved { get; set; }
    }
}
