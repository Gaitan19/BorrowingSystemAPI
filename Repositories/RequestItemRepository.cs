using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Repositories
{
    public class RequestItemRepository : IRequestItemRepository
    {

        private readonly BorrowingContext _context;

        public RequestItemRepository(BorrowingContext context)
        {
            _context = context;
        }

        public RequestItem CreateRequestItem(RequestItem requestItem)
        {
            var newRequestItem = _context.RequestItems.Add(requestItem);
            _context.SaveChanges();
            return newRequestItem.Entity;
        }

        public void DeleteRequestItem(Guid id)
        {
            var requestItemDeleted = _context.RequestItems.FirstOrDefault(u => u.Id == id);
            if (requestItemDeleted != null)
            {
                requestItemDeleted.DeletedAt = DateTime.Now;
                _context.RequestItems.Update(requestItemDeleted);
                _context.SaveChanges();
            }
        }

        public IEnumerable<RequestItem> GetAllRequestItems()
        {
            return _context.RequestItems.ToList();
        }

        public RequestItem? GetRequestItemById(Guid id)
        {
            return _context.RequestItems.FirstOrDefault(u => u.Id == id);
        }

        public RequestItem UpdateRequestItem(RequestItem requestItem)
        {
            var updatedRequestItem = _context.RequestItems.Update(requestItem);
            _context.SaveChanges();
            return updatedRequestItem.Entity;
        }

        public void DeleteItemsByRequestIdExcluding(Guid requestId, List<RequestItem> newRequestItems)
        {
            var newItemIds = newRequestItems.Select(item => item.Id).ToHashSet();

            var itemsToDelete = _context.RequestItems
                .Where(ri => ri.RequestId == requestId && !newItemIds.Contains(ri.Id))
                .ToList();

            if (itemsToDelete.Any())
            {
                _context.RequestItems.RemoveRange(itemsToDelete);
                _context.SaveChanges();
            }
        }

    }
}
