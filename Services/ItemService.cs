using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Interfaces.Repository;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemRepository.GetAllItems();
        }

        public Item? GetItemById(Guid id)
        {
            return _itemRepository.GetItemById(id);
        }

        public Item CreateItem(ItemDTO itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            return _itemRepository.CreateItem(item);
        }

        public Item? UpdateItem(Guid id, ItemDTO itemDto)
        {
            var existingItem = _itemRepository.GetItemById(id);
            if (existingItem == null) return null;
            _mapper.Map(itemDto, existingItem);
            return _itemRepository.UpdateItem(existingItem);
        }

        public void DeleteItem(Guid id)
        {
            _itemRepository.DeleteItem(id);
        }

    }
}
