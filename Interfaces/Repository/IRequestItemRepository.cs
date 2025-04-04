using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IRequestItemRepository
    {
        IEnumerable<RequestItem> GetAllRequestItems();
        RequestItem? GetRequestItemById(Guid id);
        RequestItem CreateRequestItem(RequestItem requestItem);
        RequestItem UpdateRequestItem(RequestItem requestItem);
        void DeleteRequestItem(Guid id);

        void DeleteItemsByRequestIdExcluding(Guid requestId, List<RequestItem> newRequestItems);

    }
}
