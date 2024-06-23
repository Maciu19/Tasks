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
            .WithColumn("username").AsString()
            .WithColumn("email").AsString()
            .WithColumn("password").AsString()
            .WithColumn("display_name").AsString()
            .WithColumn("deleted").AsBoolean();

        Execute.Sql($@"
            CREATE UNIQUE INDEX {DatabaseConstants.UserTableName}_unique_username
            ON {DatabaseConstants.Schema}.{DatabaseConstants.UserTableName} (username)
            WHERE deleted = false;
        ");

        Execute.Sql($@"
            CREATE UNIQUE INDEX {DatabaseConstants.UserTableName}_unique_email
            ON {DatabaseConstants.Schema}.{DatabaseConstants.UserTableName} (email)
            WHERE deleted = false;
        ");

        Execute.Sql($@"
            CREATE UNIQUE INDEX {DatabaseConstants.UserTableName}_unique_display_name
            ON {DatabaseConstants.Schema}.{DatabaseConstants.UserTableName} (display_name)
            WHERE deleted = false;
        ");
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.UserTableName).Exists())
        {
            Delete.Table(DatabaseConstants.UserTableName);
        }
    }
}
