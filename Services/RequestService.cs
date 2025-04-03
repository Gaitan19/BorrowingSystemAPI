using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.DTOs.RequestDTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;
using BorrowingSystemAPI.Repositories;

namespace BorrowingSystemAPI.Services
{
    public class RequestService
    {
        //private readonly IRequestRepository _requestRepository;
        //private readonly IMapper _mapper;

        //public RequestService(IRequestRepository requestRepository, IMapper mapper)
        //{
        //    _requestRepository = requestRepository;
        //    _mapper = mapper;
        //}

        //public Request CreateRequest(RequestDTO requestDTO)
        //{
        //    var request = _mapper.Map<Request>(requestDTO);

        //    return _requestRepository.CreateRequest(request);
        //}

        //public IEnumerable<Request> GetAllRequests()
        //{
        //    var requests = _requestRepository.GetAllRequests();
        //    return requests;
        //}

        //public Request? GetRequestById(Guid id)
        //{
        //    return _requestRepository.GetRequestById(id);
        //}

        //public Request? UpdateRequest(Guid id ,RequestDTO requestDTO)
        //{
        //    var existingRequest = _requestRepository.GetRequestById(id);
        //    if (existingRequest == null) return null;
        //    _mapper.Map(requestDTO, existingRequest);
        //    return _requestRepository.UpdateRequest(existingRequest);
        //}

        //public void DeleteRequest(Guid id)
        //{
        //    _requestRepository.DeleteRequest(id);
        //}

        private readonly IRequestRepository _requestRepository;
        private readonly IRequestItemRepository _requestItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IMovementTypeRepository _movementTypeRepository;

        public RequestService(IRequestRepository requestRepository, IRequestItemRepository requestItemRepository, IItemRepository itemRepository, IMovementRepository movementRepository, IMovementTypeRepository movementTypeRepository)
        {
            _requestRepository = requestRepository;
            _requestItemRepository = requestItemRepository;
            _itemRepository = itemRepository;
            _movementRepository = movementRepository;
            _movementTypeRepository = movementTypeRepository;
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

            var createdRequest = _requestRepository.CreateRequest(newRequest);

            foreach (var reqItem in requestDto.RequestItems)
            {
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
            if (request == null) return "Solicitud no encontrada.";

            if (dto.IsApproved)
            {
                var movementTypeOut = _movementTypeRepository.GetMovementTypeByName("out");
                if (movementTypeOut == null) return "Tipo de movimiento 'out' no encontrado.";

                foreach (var reqItem in request.RequestItems)
                {
                    var item = _itemRepository.GetItemById(reqItem.ItemId);
                    if (item == null) return $"Item {reqItem.ItemId} no encontrado.";

                    if (item.Quantity < reqItem.Quantity)
                        return $"Stock insuficiente para el item {item.Name}.";

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
                var item = _itemRepository.GetItemById(reqItem.ItemId);
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
