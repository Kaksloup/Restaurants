using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Bussiness;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<CreateDishCommand, Dish>();

        CreateMap<Dish, ReadDishDto>();
    }
}