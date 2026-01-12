using WeatherServices.SharedKernel.Core.Domain;

namespace WeatherServices.Identity.Domain.Entities;

public sealed class Users : IAuditable
{
    public int Id { get;  init; }
    public Guid Uuid { get;  init; }
    public DateTime CreatedAt { get;  init; }
    public DateTime? UpdatedAt { get;  set; }
    public required string Name { get; init; }
    public required string Email { get; init; }    
    public required string PasswordHash { get; init; }
}
