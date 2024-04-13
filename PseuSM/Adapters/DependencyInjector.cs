using Adapters.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection serviceCollection, IConfiguration configuration, bool isDevelopment)
        {
            serviceCollection.AddScoped<IUserAuthAdapter, UserAuthAdapter>();
            serviceCollection.AddScoped<IUserAdapter, UserAdapter>();

            serviceCollection.AddAutoMapper(typeof(DependencyInjector));

            BLL.DependencyInjector.Inject(serviceCollection, configuration, isDevelopment);
        }
    }
}
