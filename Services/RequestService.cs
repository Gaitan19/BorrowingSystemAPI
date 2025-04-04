using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.DTOs.RequestDTOs;
using BorrowingSystemAPI.Exceptions;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;
using BorrowingSystemAPI.Repositories;

namespace BorrowingSystemAPI.Services
{
    public class RequestService
    {

        private readonly IRequestRepository _requestRepository;
        private readonly IRequestItemRepository _requestItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IMovementTypeRepository _movementTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RequestService(IUserRepository userRepository, IRequestRepository requestRepository, IRequestItemRepository requestItemRepository, IItemRepository itemRepository, IMovementRepository movementRepository, IMovementTypeRepository movementTypeRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _requestRepository = requestRepository;
            _requestItemRepository = requestItemRepository;
            _itemRepository = itemRepository;
            _movementRepository = movementRepository;
            _movementTypeRepository = movementTypeRepository;
            _mapper = mapper;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            var requests = _requestRepository.GetAllRequests();
            return requests;
        }

        public Request? GetRequestById(Guid id)
        {
            return _requestRepository.GetRequestById(id);
        }

        public Request? UpdateRequest(Guid id, RequestDTO requestDTO)
        {
            var existingRequest = _requestRepository.GetRequestById(id);
            if (existingRequest == null) return null;
            _mapper.Map(requestDTO, existingRequest);
            return _requestRepository.UpdateRequest(existingRequest);
        }

        public void DeleteRequest(Guid id)
        {
            _requestRepository.DeleteRequest(id);
        }

        public RequestDTO CreateRequest(CreateRequestDTO requestDto)
        {
            var newRequest = new Request
            {
                Description = requestDto.Description,
                RequestedByUserId = requestDto.RequestedByUserId,
                RequestStatus = RequestStatus.Pending,
                ReturnStatus = ReturnStatus.Pending
            };

            var user = _userRepository.GetUserById(requestDto.RequestedByUserId);
            if (user == null)
                throw new ServiceException("Usuario no encontrado.", 404);

            var createdRequest = _requestRepository.CreateRequest(newRequest);

            foreach (var reqItem in requestDto.RequestItems)
            {
                var item = _itemRepository.GetItemById(reqItem.ItemId);
                if (item == null)
                    throw new ServiceException($"Item {reqItem.ItemId} no encontrado.", 404);

                if (item.Quantity < reqItem.Quantity)
                    throw new ServiceException($"Stock insuficiente para el item {item.Name}.", 400);

                var requestItem = new RequestItem
                {
                    RequestId = createdRequest.Id,
                    ItemId = reqItem.ItemId,
                    Quantity = reqItem.Quantity
                };

                _requestItemRepository.CreateRequestItem(requestItem);
            }

            return new RequestDTO
            {
                Id = createdRequest.Id,
                Description = createdRequest.Description,
                RequestedByUserId = createdRequest.RequestedByUserId,
                RequestStatus = createdRequest.RequestStatus,
                ReturnStatus = createdRequest.ReturnStatus,
                RequestDate = createdRequest.RequestDate,
                RequestItems = requestDto.RequestItems
            };
        }

        public string ApproveOrRejectRequest(ApproveRejectRequestDTO dto)
        {
            var request = _requestRepository.GetRequestById(dto.RequestId);
            if (request == null)
                throw new ServiceException("Solicitud no encontrada.", 404);

            if (dto.IsApproved)
            {
                var movementTypeOut = _movementTypeRepository.GetMovementTypeByName("out");
                if (movementTypeOut == null)
                    throw new ServiceException("Tipo de movimiento 'out' no encontrado.", 500);

                foreach (var reqItem in request.RequestItems)
                {
                    var item = reqItem.Item;
                    if (item == null)
                        throw new ServiceException($"Item {reqItem.ItemId} no encontrado.", 404);

                    if (item.Quantity < reqItem.Quantity)
                        throw new ServiceException($"Stock insuficiente para el item {item.Name}.", 400);

                    item.Quantity -= reqItem.Quantity;
                    _itemRepository.UpdateItem(item);

                    var movement = new Movement
                    {
                        ItemId = item.Id,
                        MovementTypeId = movementTypeOut.Id,
                        Quantity = reqItem.Quantity
                    };

                    _movementRepository.CreateMovement(movement);
                }

                request.RequestStatus = RequestStatus.Approved;
            }
            else
            {
                request.RequestStatus = RequestStatus.Rejected;
            }

            _requestRepository.UpdateRequest(request);
            return dto.IsApproved ? "Solicitud aprobada y stock actualizado." : "Solicitud rechazada.";
        }


        public string ReturnItems(Guid requestId)
        {
            var request = _requestRepository.GetRequestById(requestId);
            if (request == null) return "Solicitud no encontrada.";

            if (request.RequestStatus != RequestStatus.Approved || request.ReturnStatus == ReturnStatus.Returned)
                return "La solicitud no es elegible para devolución.";

            var movementTypeIn = _movementTypeRepository.GetMovementTypeByName("in");
            if (movementTypeIn == null) return "Tipo de movimiento 'in' no encontrado.";

            foreach (var reqItem in request.RequestItems)
            {
                var item = reqItem.Item;

                if (item == null) return $"Item {reqItem.ItemId} no encontrado.";

                item.Quantity += reqItem.Quantity;
                _itemRepository.UpdateItem(item);

                var movement = new Movement
                {
                    ItemId = item.Id,
                    MovementTypeId = movementTypeIn.Id,
                    Quantity = reqItem.Quantity
                };

                _movementRepository.CreateMovement(movement);
            }

            request.ReturnStatus = ReturnStatus.Returned;
            _requestRepository.UpdateRequest(request);

            return "Los ítems fueron devueltos correctamente.";
        }
    }
}
