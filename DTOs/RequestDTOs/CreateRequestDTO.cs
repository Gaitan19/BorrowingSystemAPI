using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class CreateRequestDTO
    {

        [Required(ErrorMessage = "The Description field is required.")]
        [MinLength(10, ErrorMessage = "The Description field must have at least 10 characters.")]
        public string Description { get; set; }

        [Required]
        public Guid RequestedByUserId { get; set; }

        [Required]
        public List<RequestItemDTO> RequestItems { get; set; }
    }
}
