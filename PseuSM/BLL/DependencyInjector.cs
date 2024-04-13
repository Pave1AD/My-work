using BLL.Abstractions.Services;
using BLL.Abstractions.Services.Externals;
using BLL.Services;
using BLL.Services.Externals;
using BLL.Services.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection serviceCollection, IConfiguration configurations, bool isDevelopment)
        {
            serviceCollection.AddScoped<IJwtService, JwtService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IImageService, ImageService>();
            serviceCollection.AddScoped<IDALImageService, DALImageService>();
            serviceCollection.AddScoped<IImageMetadataService<CloudinaryImageParameters>, CloudinaryImageService>();
            serviceCollection.AddScoped<IImagePlaceholderService, ImagePlaceholderService>();
            serviceCollection.AddScoped<ICloudinaryService, CloudinaryService>();

            serviceCollection.AddAutoMapper(typeof(DependencyInjector));

            DAL.DependencyInjector.Inject(serviceCollection, configurations, isDevelopment);
        }
    }
}
