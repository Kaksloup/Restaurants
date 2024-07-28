using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Bussiness;
using Restaurants.Application.Bussiness.Commands.AssignRoleToUser;
using Restaurants.Application.Bussiness.Commands.UnassignRoleFromUser;
using Restaurants.Domain.Entities.Constants;

namespace Restaurants.Presentation.Api;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class IdentitiesController(IMediator mediator) : ControllerBase
{
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserByIdAsync(int id, [FromBody] UpdateUserCommand command)
    {
        command.UserId = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:int}/AssignRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = UtilRoles.Administrator)]
    public async Task<IActionResult> AssignRoleToUserAsync(int id, [FromBody] AssignRoleToUserCommand command)
    {
        command.UserId = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}/UnassignRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = UtilRoles.Administrator)]
    public async Task<IActionResult> UnassignRoleFromUserAsync(int id, [FromBody] UnassignRoleFromUserCommand command)
    {
        command.UserId = id;
        await mediator.Send(command);
        return NoContent();
    }
}