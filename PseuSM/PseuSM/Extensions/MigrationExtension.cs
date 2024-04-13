using DAL;
using Microsoft.EntityFrameworkCore;

namespace PseuSM.Extensions
{
    public static class MigrationExtension
    {
        public static WebApplication UseMigration(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();

            MainContext context = scope.ServiceProvider.GetRequiredService<MainContext>();

            context.Database.Migrate();

            return app;
        }
    }
}
