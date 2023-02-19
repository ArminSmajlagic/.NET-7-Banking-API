namespace src.Middleware.APIVersioning
{
    public static class VersioningExtension
    {
        public static IApplicationBuilder UseAPIVersion(this IApplicationBuilder app)
        {
            return app.UseMiddleware<VersioningMiddleware>();
        }
    }
}
