using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjector
    {
        public static void Inject(IServiceCollection serviceCollection, IConfiguration configuration, bool isDevelopment)
        {
            serviceCollection.Configure<JwtProperties>(properties =>
            {
                properties.Issuer = isDevelopment ? configuration["JwtOptions:Issuer"]! : configuration["JWT_ISSUER"]!;
                properties.Audience = isDevelopment ? configuration["JwtOptions:Audience"]! : configuration["JWT_AUDIENCE"]!;
                properties.SecretKey = isDevelopment ? configuration["JwtOptions:SecretKey"]! : configuration["JWT_SEWCRET_KEY"]!;
            });

            serviceCollection.Configure<CloudinaryAccountSettings>(settings =>
            {
                settings.CloudName = isDevelopment ? configuration["CloudinaryAccountSettings:CloudName"]! : configuration["CLOUDINARY_CLOUD_NAME"]!;
                settings.ApiKey = isDevelopment ? configuration["CloudinaryAccountSettings:ApiKey"]! : configuration["CLOUDINARY_API_KEY"]!;
                settings.ApiSecret = isDevelopment ? configuration["CloudinaryAccountSettings:ApiSecret"]! : configuration["CLOUDINARY_API_SECRET"]!;
            });

            serviceCollection.Configure<CloudinaryParameters>(parameters =>
            {
                parameters.DefaultImageUrl = isDevelopment ? configuration["CloudinaryParameters:DefaultImageUrl"]! : configuration["CLOUDINARY_DEFAULT_IMAGE_URL"]!;
            });
        }
    }
}
