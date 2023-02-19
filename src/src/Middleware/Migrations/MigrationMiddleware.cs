using FluentMigrator.Runner;
using src.Extensions;

namespace src.Middleware.Migrations
{
    //Custom middleware that runs migration on every Http request
    //Migrations are being done on app startup in Program.cs so this is not used, but is one of options to ensure that migrations were used
    public class MigrationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            context.Response.Headers.Add("", "");
            using var scope = context.RequestServices.CreateScope();

            var migrationRunner = scope.ServiceProvider.GetService<IMigrationRunner>();

            if (migrationRunner != null)
                scope.RunMigrations(migrationRunner);
            else
                throw new Exception("IMigrationRunner could not be resolved at Program.cs");

            await next.Invoke(context);
        }
    }
}
