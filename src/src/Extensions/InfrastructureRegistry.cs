namespace src.Extensions
{
    public static class InfrastructureRegistry
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

            //Adding my custom extension methodes

            return services
                .AddFluentMigrator(configuration)
                .AddRedis(configuration)
                .RegisterRepositoryDependencies();
        }
    }
}
