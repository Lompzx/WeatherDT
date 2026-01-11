using AutoMapper;
using WeatherServices.DatabaseManagement.EntityFramework.Abstractions;
using WeatherServices.Weather.Domain.User;
using WeatherServices.Weather.SqlServer.EntityFramework.Weather;
using WeatherServices.WeatherAPI.Factories;

namespace WeatherServices.Weather.Application.UseCases.User.AddOrEditFavoriteCity;

internal sealed class AddFavoriteCityRequestHandler : IRequestHandler<AddFavoriteCityRequest, Result>
{
    private readonly IDbFacade _dbFacade;
    private readonly IMapper _mapper;

    public AddFavoriteCityRequestHandler(IDbFacade dbFacade, IMapper mapper, IWeatherAPIFactory weatherAPIFactory)
    {
        _dbFacade = dbFacade;
        _mapper = mapper;
    }


    public async Task<Result> Handle(AddFavoriteCityRequest request, CancellationToken cancellationToken)
    {
        Users? existingUser = await _dbFacade.User()
            .WithFavorites(city => city.Name.ToLower() == request.City.Name.ToLower())
            .FindOneAsync(user => user.Uuid == request.Uuid, cancellationToken);

        if (existingUser is null)
            return Result.NotFound($"Unable to find user");

        if (existingUser.FavoriteCities.Count > 0)
            return Result.Conflict($"City '{request.City.Name}' is already in favorite cities");

        UserFavoriteCity favoriteCity = _mapper.Map<UserFavoriteCity>(request.City);

        existingUser.AddFavoriteCity(favoriteCity);

        _dbFacade.Update(existingUser);

        return Result.Ok();
    }
}
