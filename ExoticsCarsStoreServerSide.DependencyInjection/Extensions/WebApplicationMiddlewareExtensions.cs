using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ExoticsCarsStoreServerSide.DependencyInjection.Extensions
{
    public static class WebApplicationMiddlewareExtensions
    {
        public static WebApplication UseWebApplicationMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(option =>
                {
                    option.SwaggerEndpoint("/openapi/v1.json", "API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
