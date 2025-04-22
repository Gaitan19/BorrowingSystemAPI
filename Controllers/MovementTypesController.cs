using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Models;
using BorrowingSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowingSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovementTypesController : ControllerBase
    {
        private readonly MovementTypeService _movementTypeService;
        private readonly IMapper _mapper;

        public MovementTypesController(MovementTypeService movementTypeService, IMapper mapper)
        {
            _movementTypeService = movementTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovementType>> GetAllMovementTypes()
        {
            var movementTypes = _movementTypeService.GetAllMovementTypes();
            return Ok(movementTypes);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<MovementType> GetMovementTypeById(Guid id)
        {
            var movementType = _movementTypeService.GetMovementTypeById(id);
            if (movementType == null) return NotFound(new { message = "MovementType not found" });

            return Ok(movementType);
        }

        [HttpPost]
        public ActionResult<MovementType> CreateMovementType([FromBody] MovementTypeDTO movementTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newMovementType= _movementTypeService.CreateMovementType(movementTypeDto);
                return Ok(newMovementType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the movementType", error = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public ActionResult<MovementType> UpdateMovementType(Guid id, [FromBody] MovementTypeDTO movementTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedMovementType = _movementTypeService.UpdateMovementType(id, movementTypeDto);
                if (updatedMovementType== null) return NotFound(new { message = "MovementType not found" });

                return Ok(updatedMovementType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the movementType", error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteMovementType(Guid id)
        {
            var movementType = _movementTypeService.GetMovementTypeById(id);
            if (movementType == null) return NotFound(new { message = "MovementType not found" });

            _movementTypeService.DeleteMovementType(id);
            return Ok();
        }
    }
}
