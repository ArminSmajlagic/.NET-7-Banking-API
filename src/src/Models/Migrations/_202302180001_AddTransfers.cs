using FluentMigrator;

namespace src.Models.Migrations
{
    [Migration(202302180001)]
    public class _202302180001_AddTransfers : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("transfer")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("accounttoid").AsInt32().NotNullable()
                .WithColumn("accountfromid").AsInt32().NotNullable()
                .WithColumn("ammount").AsDouble().NotNullable()
                .WithColumn("created").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("updated").AsDateTime().Nullable()
                .WithColumn("deleted").AsBoolean().NotNullable();

            Create.ForeignKey()
                .FromTable("transfer").ForeignColumn("accounttoid")
                .ToTable("account").PrimaryColumn("id");

            Create.ForeignKey()
                .FromTable("transfer").ForeignColumn("accountfromid")
                .ToTable("account").PrimaryColumn("id");

            Alter.Table("account")
                .AddColumn("balance")
                .AsDouble()
                .WithDefaultValue(2000.0)
                .NotNullable();       

        }
    }
}
