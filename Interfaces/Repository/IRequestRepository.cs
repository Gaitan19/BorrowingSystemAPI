using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IRequestRepository
    {
        Request CreateRequest(Request request);
        void DeleteRequest(Guid id);
        IEnumerable<Request> GetAllRequests();
        Request? GetRequestById(Guid id);
        Request UpdateRequest(Request request);
    }
}
