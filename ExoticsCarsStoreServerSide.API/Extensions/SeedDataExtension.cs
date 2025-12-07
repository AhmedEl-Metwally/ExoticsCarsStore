using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ExoticsCarsStoreServerSide.API.Extensions
{
    public static class SeedDataExtension
    {
        public static WebApplication MigrateSeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ExoticsCarsStoreDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
                dbContext.Database.Migrate();
            return app;
        }

        public static WebApplication SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            DataInitializerService.Initialize();
            return app; 
        }

    }
}
