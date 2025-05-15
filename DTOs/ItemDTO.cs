using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs
{
    public class ItemDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [MinLength(3, ErrorMessage = "The Name field must have at least 5 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [MinLength(10, ErrorMessage = "The Description field must have at least 5 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Quantity field must be a positive integer.")]
        public int Quantity { get; set; }
    }
}