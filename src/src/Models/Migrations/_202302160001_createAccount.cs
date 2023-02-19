using FluentMigrator;

namespace src.Models.Migrations
{
    [Migration(202302160001)]
    public class _202302160001_createAccount : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();

        }

        public override void Up()
        {
            Create.Table("account")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("username").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("created").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated").AsDateTime().Nullable()
                .WithColumn("deleted").AsBoolean().NotNullable();
        }
    }
}
