using WeatherServices.Identity.Domain.Entities;
using WeatherServices.Identity.Domain.Exceptions;
using WeatherServices.Identity.Domain.Interfaces;
using WeatherServices.Identity.Ports.In;
using WeatherServices.Identity.Ports.Out;

namespace WeatherServices.Identity.Application.UseCases.AuthenticateUser;

public sealed class AuthenticateUserHandler : IAuthenticateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthenticateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    public async Task<AuthenticateUserResult> ExecuteAsync(AuthenticateUser command, CancellationToken cancellationToken)
    {
        Users? existingUsers = await _userRepository
            .GetByUsernameAsync(command.Username, cancellationToken);

        if (existingUsers is null)
            throw new ArgumentNullException("Unable to find user");

        string hash = _passwordHasher.Hash(command.Password);

        if (!_passwordHasher.Verify(command.Password, existingUsers.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        string token = _tokenService.GenerateAccessToken(existingUsers);

        return new AuthenticateUserResult(existingUsers.Uuid, existingUsers.Email, token);        
    }
}
