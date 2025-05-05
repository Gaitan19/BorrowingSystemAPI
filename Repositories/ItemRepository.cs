using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystemAPI.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private readonly BorrowingContext _context;

        public ItemRepository(BorrowingContext context)
        {
            _context = context;
        }

        public Item CreateItem(Item item)
        {
            var newItem = _context.Items.Add(item);
            _context.SaveChanges();
            return newItem.Entity;
        }

        public void DeleteItem(Guid id)
        {
            var ItemDeleted = _context.Items.FirstOrDefault(u => u.Id == id);
            if (ItemDeleted != null)
            {
                ItemDeleted.DeletedAt = DateTime.Now;
                _context.Items.Update(ItemDeleted);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _context.Items.ToList();

        }

        public Item? GetItemById(Guid id)
        {
            return _context.Items.AsNoTracking().FirstOrDefault(i => i.Id == id);

        }

        public Item UpdateItem(Item item)
        {

            var updatedItem = _context.Items.Update(item);
            _context.SaveChanges();
            return updatedItem.Entity;
        }

        public bool ItemExists(Guid id)
        {
            return _context.Items.Any(i => i.Id == id);
        }

        public int GetItemQuantity(Guid id)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                return item.Quantity;
            }
            return 0;
        }
    }
}
