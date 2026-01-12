using ExoticsCarsStoreServerSide.Shared.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExoticsCarsStoreServerSide.DependencyInjection.Extensions
{
    public static class WebApplicationExtensions
    {

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddOpenApi();

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateAPiValidationResponse;
            });
            return ConfigureJWT(Services, Configuration);
        }

        private static IServiceCollection ConfigureJWT(IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidIssuer = Configuration["JwtOptions:Issuer"],
                      ValidAudience = Configuration["JwtOptions:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtOptions:SecretKey"]))
                  };
              });

            return Services;
        }

    }
}
