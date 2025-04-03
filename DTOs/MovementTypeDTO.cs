using System.ComponentModel.DataAnnotations;

namespace BorrowingSystemAPI.DTOs
{
    public class MovementTypeDTO
    {

        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }
        
        [Required(ErrorMessage = "Operation is required")]

        public int Operation { get; set; }
    }
}
