namespace src.Middleware.APIVersioning
{
    public class VersioningMiddleware 
    {
        private readonly RequestDelegate next;

        public VersioningMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-API-VERSION", "1.0");

            await next.Invoke(context);
        }
    }
}
