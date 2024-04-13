using DAL.Abstractions.UnitOfWork;
using DAL.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection serviceCollection, IConfiguration configuration, bool isDevelopment)
        {
            serviceCollection.AddDbContext<MainContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(isDevelopment ? configuration.GetConnectionString("MainConnectionString") : configuration["MAIN_CONNECTION_STRING"]);
            });

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
