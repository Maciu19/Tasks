using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(1)]
public class CreateUserTable : Migration
{
    private const string TableName = "user";

    public override void Up()
    {
        if (Schema.Schema("public").Table(TableName).Exists())
        {
            return;
        }

        Create.Table(TableName)
            .InSchema("public")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("email").AsString().Unique()
            .WithColumn("password").AsString()
            .WithColumn("display_name").AsString().Unique();
    }

    public override void Down()
    {
        if (Schema.Schema("public").Table(TableName).Exists())
        {
            Delete.Table(TableName);
        }
    }
}
