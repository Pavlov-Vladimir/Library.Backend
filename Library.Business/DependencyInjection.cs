using Library.Business.DataProviders;
using Library.Business.Services;
using Library.Domain.Contracts.DataProviders;
using Library.Domain.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Business;
public static class DependencyInjection
{
    public static IServiceCollection AddDataProvidersAndServices(this IServiceCollection services)
    {
        services.AddTransient<ILibraryDataProvider, LibraryDataProvider>();
        services.AddTransient<ILibraryService, LibraryService>();

        return services;
    }
}
