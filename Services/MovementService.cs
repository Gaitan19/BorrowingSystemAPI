using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Services
{
    public class MovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMapper _mapper;

        public MovementService(IMovementRepository movementRepository, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
        }

        public IEnumerable<Movement> GetAllMovements()
        {
            return _movementRepository.GetAllMovements();
        }

        public Movement? GetMovementById(Guid id)
        {
            return _movementRepository.GetMovementById(id);
        }

        public Movement CreateMovement(MovementDTO movementDto)
        {
            var movement = _mapper.Map<Movement>(movementDto);
            return _movementRepository.CreateMovement(movement);
        }

        public Movement? UpdateMovement(Guid id, MovementDTO movementDto)
        {
            var existingMovement = _movementRepository.GetMovementById(id);
            if (existingMovement == null) return null;
            _mapper.Map(movementDto, existingMovement);
            return _movementRepository.UpdateMovement(existingMovement);
        }

        public void DeleteMovement(Guid id)
        {
            _movementRepository.DeleteMovement(id);
        }


    }
}
