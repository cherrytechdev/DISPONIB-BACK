using Autofac;

namespace ESInfrastructure.Database
{

    public class DataAccessModule : Module
    {
        private readonly string databaseConnectionString;
        public DataAccessModule(string databaseConnectionString)
        {
            this.databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
            .As<ISqlConnectionFactory>()
            .WithParameter("connectionString", databaseConnectionString)
            .InstancePerLifetimeScope();
        }
    }
}
