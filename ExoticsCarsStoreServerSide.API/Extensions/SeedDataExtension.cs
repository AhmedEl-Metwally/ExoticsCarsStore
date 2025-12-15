using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExoticsCarsStoreServerSide.API.Extensions
{
    public static class SeedDataExtension
    {
        public static async Task<WebApplication> MigrateSeedDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ExoticsCarsStoreDbContext>();
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();   
            if (PendingMigrations.Any())
                await dbContext.Database.MigrateAsync();
            return app;
        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            await DataInitializerService.InitializeAsync();
            return app; 
        }

    }
}
