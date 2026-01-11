using AutoMapper;

namespace WeatherServices.SharedKernel.Configurations.AutoMapper;

public abstract class ApplicationMappingProfile : Profile
{
    protected ApplicationMappingProfile()
    {
        RegisterMappings();
    }
    protected abstract void RegisterMappings();
}