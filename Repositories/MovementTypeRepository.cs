using BorrowingSystemAPI.Context;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Repositories
{
    public class MovementTypeRepository : IMovementTypeRepository
    {

        private readonly BorrowingContext _context;

        public MovementTypeRepository(BorrowingContext context)
        {
            _context = context;
        }

        public MovementType CreateMovementType(MovementType movementType)
        {
            var newMovementType = _context.MovementTypes.Add(movementType);
            _context.SaveChanges();
            return newMovementType.Entity;
        }

        public void DeleteMovementType(Guid id)
        {
            var movementTypeDeleted = _context.MovementTypes.FirstOrDefault(u => u.Id == id);
            if (movementTypeDeleted != null)
            {
                movementTypeDeleted.DeletedAt = DateTime.Now;
                _context.MovementTypes.Update(movementTypeDeleted);
                _context.SaveChanges();
            }
        }

        public IEnumerable<MovementType> GetAllMovementTypes()
        {
            return _context.MovementTypes.ToList();

        }

        public MovementType? GetMovementTypeById(Guid id)
        {
            return _context.MovementTypes.FirstOrDefault(u => u.Id == id);

        }

        public MovementType? GetMovementTypeByName(string name)
        {
            return _context.MovementTypes.FirstOrDefault(u => u.Name == name);
        }

        public MovementType UpdateMovementType(MovementType movementType)
        {
            var updatedMovementType= _context.MovementTypes.Update(movementType);
            _context.SaveChanges();

            return updatedMovementType.Entity;
        }
    }
}
