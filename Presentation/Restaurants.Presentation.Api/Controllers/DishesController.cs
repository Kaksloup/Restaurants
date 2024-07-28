using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Bussiness;
using Restaurants.Infrastructure.Application.Constants;

namespace Restaurants.Presentation.Api;

[ApiController]
[Route("api/Restaurant/{restaurantId}/[controller]")]
[Produces("application/json")]
[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Creates an Dish.
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST api/Employee
    ///     {
    ///     "firstName": "Mike",
    ///     "lastName": "Andrew",
    ///     "emailId": "Mike.Andrew@gmail.com"
    ///     }
    /// </remarks>
    /// <param name="employee"></param>
    /// <returns>A newly created employee</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    public async Task<IActionResult> CreateDishAsync([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (command.RestaurantId != restaurantId)
            return BadRequest("L'identifiant du restaurant est différents entre le route et le corps");

        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.HasAtLeast20YearOld)]
    public async Task<ActionResult<IEnumerable<ReadDishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<ReadDishDto>> GetByIdForRestaurant([FromRoute] int restaurantId,
        [FromRoute] int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
        return NoContent();
    }
}