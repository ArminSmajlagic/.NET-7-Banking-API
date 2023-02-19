using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace src.Filters
{
    public class NotFoundFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.HttpContext.Request.Method == "GET")
            {
                if (executedContext.Result is ObjectResult result)
                {
                    if (result.Value == null)
                    {
                        var contentResult = new ContentResult()
                        {
                            Content = "Nothing was found",
                            ContentType = "text/plain",
                            StatusCode = 404
                        };

                        executedContext.Result = contentResult;
                        return;
                    }
                }
            }

        }
    }
}
