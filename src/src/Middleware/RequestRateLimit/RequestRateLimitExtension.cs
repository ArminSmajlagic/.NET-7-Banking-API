namespace src.Middleware.RequestRateLimit
{
    public static class RequestRateLimitExtension
    {
        public static IApplicationBuilder UseRequestLimiting(this IApplicationBuilder app, int limit, int interval)
        {
            return app.UseMiddleware<RequestRateLimitMiddleware>(limit, interval);
        }
    }
}
