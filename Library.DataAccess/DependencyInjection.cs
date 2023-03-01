using Library.DataAccess.Repository;
using Library.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataAccess;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseInMemoryDatabase("LibraryInMemoryDatabase");
        });
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        return services;
    }
}
