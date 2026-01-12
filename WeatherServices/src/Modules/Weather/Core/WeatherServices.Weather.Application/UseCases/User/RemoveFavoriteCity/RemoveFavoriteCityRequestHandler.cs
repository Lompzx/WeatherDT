
using WeatherServices.DatabaseManagement.EntityFramework.Abstractions;
using WeatherServices.Weather.Domain.User;
using WeatherServices.Weather.SqlServer.EntityFramework.Weather;

namespace WeatherServices.Weather.Application.UseCases.User.RemoveFavoriteCity;

internal sealed class RemoveFavoriteCityRequestHandler : IRequestHandler<RemoveFavoriteCityRequest, Result>
{
    private readonly IDbFacade _dbFacade;
    public RemoveFavoriteCityRequestHandler(IDbFacade dbFacade) => _dbFacade = dbFacade;
    
    public async Task<Result> Handle(RemoveFavoriteCityRequest request, CancellationToken cancellationToken)
    {
        Users? existingUser = await _dbFacade.User()
            .WithFavorites()
            .FindOneAsync(user => user.Uuid == request.Uuid, cancellationToken);

        if(existingUser is null)
            return Result.NotFound("Unable to find user");

        UserFavoriteCity? userFavoriteCity = existingUser.FavoriteCities.FirstOrDefault(city => city.Uuid == request.CityUuid);

        if(userFavoriteCity is null)
            return Result.NotFound("Unable to find favorite city for user");

        existingUser.RemoveFavoriteCity(userFavoriteCity);

        _dbFacade.Update(existingUser);
        
        return Result.Ok();
    }
}
