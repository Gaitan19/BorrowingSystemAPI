using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Services
{
    public class RequestItemService
    {
        private readonly IRequestItemRepository _requestItemRepository;
        private readonly IMapper _mapper;

        public RequestItemService(IRequestItemRepository requestItemRepository, IMapper mapper)
        {
            _requestItemRepository = requestItemRepository;
            _mapper = mapper;
        }

        public IEnumerable<RequestItem> GetAllRequestItems()
        {
            return _requestItemRepository.GetAllRequestItems();
        }

        public RequestItem? GetRequestItemById(Guid id)
        {
            return _requestItemRepository.GetRequestItemById(id);
        }

        public RequestItem CreateRequestItem(RequestItemDTO requestItemDto)
        {
            var requestItem = _mapper.Map<RequestItem>(requestItemDto);

            return _requestItemRepository.CreateRequestItem(requestItem);
        }

        public RequestItem? UpdateRequestItem(Guid id, RequestItemDTO requestItem)
        {
            var existingRequestItem = _requestItemRepository.GetRequestItemById(id);
            if (existingRequestItem == null) return null;
            _mapper.Map(requestItem, existingRequestItem);
            return _requestItemRepository.UpdateRequestItem(existingRequestItem);
        }

        public void DeleteRequestItem(Guid id)
        {
            _requestItemRepository.DeleteRequestItem(id);
        }


    }
}
