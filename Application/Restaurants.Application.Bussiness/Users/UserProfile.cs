using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Bussiness;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpdateUserCommand, User>();
    }
}