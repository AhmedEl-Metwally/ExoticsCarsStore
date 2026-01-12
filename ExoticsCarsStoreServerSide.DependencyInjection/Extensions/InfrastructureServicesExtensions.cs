using ExoticsCarsStoreServerSide.Domain.Models.IdentityModule;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using ExoticsCarsStoreServerSide.Persistence.IdentityData.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ExoticsCarsStoreServerSide.DependencyInjection.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<ExoticsCarsStoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });

            Services.AddDbContext<ExoticsCarsStoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnectionString"));
            });

            Services.AddSingleton<IConnectionMultiplexer>(SP =>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection")!);
            });

            Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ExoticsCarsStoreIdentityDbContext>();



            return Services;
        }
    }
}
