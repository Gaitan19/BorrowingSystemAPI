using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems();
        Item? GetItemById(Guid id);
        Item CreateItem(Item item);
        string UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
}
