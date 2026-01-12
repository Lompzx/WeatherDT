using AutoMapper;
using WeatherServices.DatabaseManagement.EntityFramework.Abstractions;
using WeatherServices.Weather.Domain.User;
using WeatherServices.Weather.SqlServer.EntityFramework.Weather;


namespace WeatherServices.Weather.Application.UseCases.User.ListFavoriteCities;

internal sealed class ListFavoriteCitiesRequestHandler : IRequestHandler<ListFavoriteCitiesRequest, Result<ListFavoriteCitiesResponse>>
{
    private readonly IDbFacade _dbFacade;
    private readonly IMapper _mapper;
    public ListFavoriteCitiesRequestHandler(IDbFacade dbFacade, IMapper mapper)
    {
        _dbFacade = dbFacade;
        _mapper = mapper;
    }

    public async Task<Result<ListFavoriteCitiesResponse>> Handle(ListFavoriteCitiesRequest request, CancellationToken cancellationToken)
    {
        Users? existingUser = await _dbFacade.User(disableChangeTracker: true)
            .WithFavorites()
            .FindOneAsync(user => user.Uuid == request.Uuid, cancellationToken);

        if (existingUser is null)
            return Result.NotFound("Unable to find user");

        ListFavoriteCitiesResponse response = _mapper.Map<ListFavoriteCitiesResponse>(existingUser);

        return Result.Ok(response);
    }
}
