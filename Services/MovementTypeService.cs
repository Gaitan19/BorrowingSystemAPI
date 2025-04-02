using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Services
{
    public class MovementTypeService
    {
        private readonly IMovementTypeRepository _movementTypeRepository;
        private readonly IMapper _mapper;

        public MovementTypeService(IMapper mapper, IMovementTypeRepository movementTypeRepository)
        {
            _mapper = mapper;
            _movementTypeRepository = movementTypeRepository;
        }

        public IEnumerable<MovementType> GetAllMovementTypes()
        {
            return _movementTypeRepository.GetAllMovementTypes();
        }

        public MovementType? GetMovementTypeById(Guid id)
        {
            return _movementTypeRepository.GetMovementTypeById(id);
        }

        public MovementType? GetMovementTypeByName(string name)
        {
            return _movementTypeRepository.GetMovementTypeByName(name);
        }

        public MovementType CreateMovementType(MovementTypeDTO movementTypeDto)
        {
            var movementType = _mapper.Map<MovementType>(movementTypeDto);
            return _movementTypeRepository.CreateMovementType(movementType);
        }

        public MovementType? UpdateMovementType(Guid id, MovementTypeDTO movementTypeDto)
        {
            var existingMovementType = _movementTypeRepository.GetMovementTypeById(id);
            if (existingMovementType == null) return null;
            _mapper.Map(movementTypeDto, existingMovementType);
            return _movementTypeRepository.UpdateMovementType(existingMovementType);
        }

        public void DeleteMovementType(Guid id)
        {
            _movementTypeRepository.DeleteMovementType(id);
        }
    }
}
