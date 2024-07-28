using AutoMapper;
using Restaurants.Application.Bussiness.Adresses.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Bussiness.Adresses;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, ReadAddressDto>();
    }
}