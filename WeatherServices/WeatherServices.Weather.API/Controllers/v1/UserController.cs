using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using WeatherServices.Identity.Application.UseCases.AuthenticateUser;
using WeatherServices.Identity.Ports.In;
using WeatherServices.Weather.API.Constants;
using WeatherServices.Weather.Application.UseCases.User.AddOrEditFavoriteCity;
using WeatherServices.Weather.Application.UseCases.User.ListFavoriteCities;
using WeatherServices.Weather.Application.UseCases.User.RemoveFavoriteCity;

namespace WeatherServices.Weather.API.Controllers.v1;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.ProblemJson)]

public sealed class UserController : ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender) => _sender = sender;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, IAuthenticateUser authenticateUser, CancellationToken cancellationToken)
    {
        AuthenticateUserResult result = await authenticateUser.ExecuteAsync(
            new AuthenticateUser(
                request.Email,
                request.Password
            ), cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("{uuid:guid}/list")]
    public async Task<IActionResult> ListAsync(
       [FromRoute(Name = RouteConstants.Uuid)] Guid uuid,       
       CancellationToken cancellationToken)
    {       
        Result<ListFavoriteCitiesResponse> result = await _sender.Send(new ListFavoriteCitiesRequest(uuid), cancellationToken);

        return this.ProcessResponse(result);
    }

    [Authorize]
    [HttpPost("{uuid:guid}/favorite-city")]
    public async Task<IActionResult> AddFavoriteCityAsync(
        [FromRoute(Name = RouteConstants.Uuid)] Guid uuid,
        [FromBody] AddFavoriteCityRequest request, 
        CancellationToken cancellationToken)
    {
        request.AttatchUuid(uuid);

        Result result = await _sender.Send(request, cancellationToken);

        return this.ProcessResponse(result);
    }

    [Authorize]
    [HttpDelete("{uuid:guid}/favorite-city/{city-uuid:guid}")]
    public async Task<IActionResult> RemoveFavoriteCityAsync(
       [FromRoute(Name = RouteConstants.Uuid)] Guid uuid,
       [FromRoute(Name = RouteConstants.CityUuid)] Guid cityUuid,
       CancellationToken cancellationToken)
    {
        Result result = await _sender.Send(new RemoveFavoriteCityRequest(uuid, cityUuid), cancellationToken);

        return this.ProcessResponse(result);        
    }  

}
