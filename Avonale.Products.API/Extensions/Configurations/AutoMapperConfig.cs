using Avonale.Products.Application.AutoMapper;

namespace Avonale.Products.API.Extensions.Configurations;

public static class AutoMapperConfiguration
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(AutoMapperProfile));
    }
}