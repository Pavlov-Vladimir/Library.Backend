using Library.DataAccess.Repository;
using Library.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataAccess;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            //opt.UseInMemoryDatabase("LibraryInMemoryDatabase"); // in memory
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")); // default
        });
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        return services;
    }
}
