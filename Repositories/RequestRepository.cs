using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystemAPI.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BorrowingContext _context;
        public RequestRepository(BorrowingContext context)
        {
            _context = context;
        }
        public Request CreateRequest(Request request)
        {
            var newRequest = _context.Requests.Add(request);
            _context.SaveChanges();
            return newRequest.Entity;
        }
        public void DeleteRequest(Guid id)
        {
            var requestDeleted = _context.Requests.FirstOrDefault(u => u.Id == id);
            if (requestDeleted != null)
            {
                requestDeleted.DeletedAt = DateTime.Now;
                _context.Requests.Update(requestDeleted);
                _context.SaveChanges();
            }
        }

        public void DeleteRequestPermanently(Guid id)
        {
            var requestToDelete = _context.Requests.FirstOrDefault(u => u.Id == id);
            if (requestToDelete != null)
            {
                _context.Requests.Remove(requestToDelete);
                _context.SaveChanges();
            }
        }


        public IEnumerable<Request> GetAllRequests()
        {
            return _context.Requests
                .AsNoTracking() 
                .Include(r => r.RequestedByUser)
                .Include(r => r.RequestItems)
                    .ThenInclude(ri => ri.Item)
                .ToList();
        }


        public Request? GetRequestById(Guid id, bool includeRelations = true)
        {
            if (includeRelations)
            {
                return _context.Requests
                   .AsNoTracking()
                   .Include(r => r.RequestedByUser)
                   .Include(r => r.RequestItems)
                       .ThenInclude(ri => ri.Item)
                   .FirstOrDefault(r => r.Id == id);
            }

            return _context.Requests
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
        }



        public Request UpdateRequest(Request request)
        {
            var updatedRequest = _context.Requests.Update(request);
            _context.SaveChanges();
            return updatedRequest.Entity;
        }
    }
}
