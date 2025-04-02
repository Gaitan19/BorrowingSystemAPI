using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;
using BorrowingSystemAPI.Repositories;

namespace BorrowingSystemAPI.Services
{
    public class RequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public RequestService(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public Request CreateRequest(RequestDTO requestDTO)
        {
            var request = _mapper.Map<Request>(requestDTO);
            
            return _requestRepository.CreateRequest(request);
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

        public Request? UpdateRequest(Guid id ,RequestDTO requestDTO)
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
    }
}
