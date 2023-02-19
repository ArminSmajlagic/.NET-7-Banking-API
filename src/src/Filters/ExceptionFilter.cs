using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace src.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            logger.LogError(context.Exception.Message);

            var result = new ContentResult()
            {
                StatusCode = 500,
                Content = context.Exception.Message,
                ContentType = "text/plain"
            };

            context.Result = result;
            
            
            return Task.CompletedTask;
        }
    }
}
