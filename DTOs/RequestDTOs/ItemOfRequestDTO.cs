using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs.RequestDTOs
{
    public class ItemOfRequestDTO
    {
        [Required]

        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]
        public int Quantity { get; set; }

    }
}
