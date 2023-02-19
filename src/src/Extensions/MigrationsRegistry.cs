using FluentMigrator.Runner;
using System.Reflection;

namespace src.Extensions
{
    public static class MigrationsRegistry
    {
        public static IServiceCollection AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddPostgres11_0()
                .WithGlobalConnectionString(configuration.GetConnectionString("bankingdb"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

            return services;
        }

        public static void RunMigrations(this IServiceScope scope,IMigrationRunner migrationRunner) {
            try
            {
                migrationRunner.ListMigrations();
                migrationRunner.MigrateUp();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
