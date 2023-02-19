namespace src.Middleware.Migrations
{
    //Custom middleware that runs migration on every Http request
    //Migrations are being done on app startup in Program.cs so this is not used, but it is one of the options to ensure that migrations were used
    public static class MigrationExtension
    {
        public static IApplicationBuilder AddMigrations(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MigrationMiddleware>();
        }
    }
}
