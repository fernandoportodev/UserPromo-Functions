using AutoMapper;
using UserPromo.DTOs;
using UserPromo.Models;

namespace UserPromo.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
