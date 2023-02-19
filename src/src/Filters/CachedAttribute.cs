using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using src.Services;
using System.Text;

namespace src.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CachedAttribute : Attribute, IAsyncResourceFilter
    {
        //In seconds
        private readonly int _timeToLive;
        private readonly bool dataChanged;

        public CachedAttribute(int timeToLive = 60, bool dataChanged = false)
        {
            _timeToLive = timeToLive;
            this.dataChanged = dataChanged;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var cachingService = context.HttpContext.RequestServices.GetRequiredService<ICachingService>();

            if (!dataChanged)
            {

                string key = GenerateKey(context.HttpContext);

                var respons = await cachingService.GetCachedRespons(key);

                if (respons != null)
                {
                    var contentResult = new ContentResult()
                    {
                        Content = respons,
                        ContentType = "application/json",
                        StatusCode = 200
                    };

                    context.Result = contentResult;
                    return;
                }

                var executedContext = await next();

                if (executedContext.Result is OkObjectResult okObjectResult)
                {
                    await cachingService.CacheRespons(key, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLive));

                }
            }
            else
            {
                var (getAllKey, getByIdKey) = FindKeyFromRoute(context.HttpContext);

                await cachingService.Remove(getAllKey, getByIdKey);

                await next();
            }
        }
        public string GenerateKey(HttpContext context)
        {
            var cacheKey = new StringBuilder();

            cacheKey.Append($"{context.Request.Path}");

            foreach (var (key, value) in context.Request.Query.OrderBy(x => x.Key))
            {
                cacheKey.Append($"|{key}-{value}");
            }

            return cacheKey.ToString();
        }

        private (string, string) FindKeyFromRoute(HttpContext context)
        {
            var getAllKey = $"{context.Request.Path}";
            var cacheKey = new StringBuilder();

            cacheKey.Append($"{context.Request.Path}");

            foreach (var (key, value) in context.Request.Query.OrderBy(x => x.Key))
            {
                if (key == "id")
                    cacheKey.Append($"|{key}-{value}");
            }
            return (getAllKey, cacheKey.ToString());
        }
    }
}
