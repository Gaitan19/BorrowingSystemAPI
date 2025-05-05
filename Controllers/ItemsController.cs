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
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemsController(ItemService itemService)
        {
            _itemService = itemService;
        }

       

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAllItems([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var items = _itemService.GetAllItems(page, pageSize);
            return Ok(items);
        }


        [HttpGet("{id:guid}")]
        public ActionResult<Item> GetItemById(Guid id)
        {
            var item = _itemService.GetItemById(id);
            if (item == null) return NotFound(new { message = "Item not found" });
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Item> CreateItem([FromBody] ItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newItem = _itemService.CreateItem(item);
                return Ok(newItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the item", error = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public ActionResult<string> UpdateItem(Guid id, [FromBody] ItemDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedItem = _itemService.UpdateItem(id, item);
                if (updatedItem == null) return NotFound(new { message = "Item not found" });
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the item", error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public ActionResult DeleteItem(Guid id)
        {
            try
            {
                var item = _itemService.GetItemById(id);
                if (item == null) return NotFound(new { message = "Item not found" });
                _itemService.DeleteItem(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the item", error = ex.Message });
            }
        }
    }
}
