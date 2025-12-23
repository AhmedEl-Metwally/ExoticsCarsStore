using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace ExoticsCarsStoreServerSide.Presentation.Attributes
{
    public class CacheAttribute: ActionFilterAttribute
    {
        private readonly int _durationInMinutes;

        public CacheAttribute(int DurationInMinutes = 80)
        {
            _durationInMinutes = DurationInMinutes;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = CreateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheValue = await cacheService.GetCachedKeyAsync(cacheKey);
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult result)
                await cacheService.SetCacheAsync(cacheKey,result.Value!,TimeSpan.FromMinutes(_durationInMinutes));
        }

        private string CreateCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(K => K.Key))
                keyBuilder.Append($"|{item.Key}-{item.Value}");
          return keyBuilder.ToString();
        }
    }
}


