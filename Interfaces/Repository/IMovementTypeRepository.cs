using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Interfaces.Repository
{
    public interface IMovementTypeRepository
    {
        IEnumerable<MovementType> GetAllMovementTypes();
        MovementType? GetMovementTypeById(Guid id);
        MovementType? GetMovementTypeByName(string name);

        MovementType CreateMovementType(MovementType movementType);
        MovementType UpdateMovementType(MovementType movementType);

        void DeleteMovementType(Guid id);
    }
}
