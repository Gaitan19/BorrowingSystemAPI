using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Repositories
{
    public class MovementRepository : IMovementRepository
    {

        private readonly BorrowingContext _context;

        public MovementRepository(BorrowingContext context)
        {
            _context = context;
        }

        public Movement CreateMovement(Movement movement)
        {
            var newMovement = _context.Movements.Add(movement);
            _context.SaveChanges();
            return newMovement.Entity;
        }

        public void DeleteMovement(Guid id)
        {
            var movementDeleted = _context.Movements.FirstOrDefault(u => u.Id == id);
            if (movementDeleted != null)
            {
                movementDeleted.DeletedAt = DateTime.Now;
                _context.Movements.Update(movementDeleted);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Movement> GetAllMovements()
        {
            return _context.Movements.ToList();
        }

        public Movement? GetMovementById(Guid id)
        {
            return _context.Movements.FirstOrDefault(u => u.Id == id);
        }

        public Movement UpdateMovement(Movement movement)
        {
            var updatedMovement = _context.Movements.Update(movement);
            _context.SaveChanges();
            return updatedMovement.Entity;
        }

    }
}
