using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Bussiness;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
            {
                Street = src.Street,
                City = src.City,
                PostalCode = src.PostalCode
            }));

        CreateMap<UpdateRestaurantCommand, Restaurant>();

        CreateMap<Restaurant, ReadRestaurantDto>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
    }
}