using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems();
        Item? GetItemById(Guid id);
        Item CreateItem(Item item);
        Item UpdateItem(Item item);
        void DeleteItem(Guid id);

        bool ItemExists(Guid id);

        int GetItemQuantity(Guid id);
    }
}
