using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.DTOs.RequestDTOs;
using BorrowingSystemAPI.Exceptions;
using BorrowingSystemAPI.Services;
using BorrowingSystemAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowingSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestsController(RequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public ActionResult<RequestDTO> CreateRequest([FromBody] CreateRequestDTO requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var createdRequest = _requestService.CreateRequest(requestDto);
                return Ok(createdRequest);
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the request", error = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<RequestDTO>> GetAllRequests()
        {
            var requests = _requestService.GetAllRequests();
            return Ok(requests);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<RequestDTO> GetRequestById(Guid id)
        {
            var request = _requestService.GetRequestById(id);
            if (request == null) return NotFound(new { message = "Request not found" });
            return Ok(request);
        }

        [HttpPut("{id:guid}")]
        public ActionResult<RequestDTO> UpdateRequest(Guid id, [FromBody] UpdateRequestDTO requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedRequest = _requestService.UpdateRequest(id, requestDto);
                if (updatedRequest == null) return NotFound(new { message = "Request not found" });
                return Ok(updatedRequest);
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the request", error = ex.Message });
            }
        }

        [HttpPost("approve-reject")]
        public IActionResult ApproveOrRejectRequest([FromBody] ApproveRejectRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = _requestService.ApproveOrRejectRequest(dto);
                return Ok(new { message = result });
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request", error = ex.Message });
            }
        }

        [HttpPost("return/{requestId:guid}")]
        public IActionResult ReturnItems(Guid requestId)
        {
            try
            {
                var result = _requestService.ReturnItems(requestId);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the return", error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteRequest(Guid id)
        {
            try
            {
                _requestService.DeleteRequest(id);
                return NoContent();
            }
            catch (ServiceException ex)
            {
                return StatusCode(ExceptionMapping.MapExceptionToControllers(ex.ErrorCode), new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the request", error = ex.Message });
            }
        }
    }
}
