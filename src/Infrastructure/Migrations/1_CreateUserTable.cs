using FluentMigrator;

using Infrastructure.Common;

namespace Infrastructure.Migrations;

[Migration(1)]
public class CreateUserTable : Migration
{
    public override void Up()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.UserTableName).Exists())
        {
            return;
        }

        Create.Table(DatabaseConstants.UserTableName)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("email").AsString().Unique()
            .WithColumn("password").AsString()
            .WithColumn("display_name").AsString().Unique();
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.UserTableName).Exists())
        {
            Delete.Table(DatabaseConstants.UserTableName);
        }
    }
}
