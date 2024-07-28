using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Bussiness;
using Restaurants.Application.Bussiness.Commons;
using Restaurants.Application.Bussiness.UploadRestaurantLogo;
using Restaurants.Domain.Entities.Constants;
using Restaurants.Infrastructure.Application.Constants;

namespace Restaurants.Presentation.Api;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PageResult<ReadRestaurantDto>>> GetRestaurantsAsync(
        [FromQuery] GetRestaurantsQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id}")]
    // [Authorize(Policy = PolicyNames.HasFirstName)]
    public async Task<ActionResult<ReadRestaurantDto>> GetRestaurantByIdAsync(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        if (restaurant == null)
            return NotFound();

        return Ok(restaurant);
    }

    [HttpPost]
    [Authorize(Roles = $"{UtilRoles.Administrator}, {UtilRoles.Owner}")]
    public async Task<IActionResult> CreateRestaurantAsync([FromBody] CreateRestaurantCommand restaurantCommand)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await mediator.Send(restaurantCommand);
        return CreatedAtAction(nameof(GetRestaurantByIdAsync), new { id }, null);
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogoAsync(int id, IFormFile file)
    {
        await using var fileStream = file.OpenReadStream();

        var command = new UploadRestaurantLogoCommand
        {
            RestaurantId = id,
            FileName = file.FileName,
            File = fileStream
        };

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurantByIdAsync(int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurantByIdAsync(int id,
        [FromBody] UpdateRestaurantCommand restaurantCommand)
    {
        restaurantCommand.Id = id;
        await mediator.Send(restaurantCommand);
        return NoContent();
    }
}