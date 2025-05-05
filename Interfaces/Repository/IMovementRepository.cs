using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IMovementRepository
    {
        IEnumerable<Movement> GetAllMovements();
        Movement? GetMovementById(Guid id);
        Movement CreateMovement(Movement movement);
        Movement UpdateMovement(Movement movement);
        void DeleteMovement(Guid id);
    }
}
