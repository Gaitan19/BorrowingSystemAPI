using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Models;

namespace BorrowingSystemAPI.Mapper
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User,AuthDTO>().ReverseMap();
            CreateMap<MovementType, MovementTypeDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();

        }
    }
}
