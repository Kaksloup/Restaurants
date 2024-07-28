﻿using MediatR;

namespace Restaurants.Application.Bussiness;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}