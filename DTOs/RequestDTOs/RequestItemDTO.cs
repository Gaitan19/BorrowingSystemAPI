using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class RequestItemDTO
    {
        [Required]
        public Guid ItemId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]


        public int Quantity { get; set; }
    }
}
