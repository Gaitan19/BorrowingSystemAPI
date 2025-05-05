using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class UpdateRequestDTO
    {
        [Required(ErrorMessage = "The Description field is required.")]
        [MinLength(10, ErrorMessage = "The Description field must have at least 10 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The RequestStatus field is required.")]
        public List<RequestItemDTO> RequestItems { get; set; }
    }
}
