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

        public string UpdateItem(Item item)
        {

            var trackedItem = _context.Items.Local.FirstOrDefault(i => i.Id == item.Id);
            if (trackedItem != null)
            {
                _context.Entry(trackedItem).State = EntityState.Detached; // 🔹 Se desconecta el anterior
            }

            _context.Attach(item); // 🔹 Se adjunta el nuevo sin forzar la actualización
            _context.Entry(item).State = EntityState.Modified; // 🔹 Se marcan solo los cambios
            _context.SaveChanges();

            return "Item Updated Successfuly";
        }
    }
}
