using FluentMigrator;

using Infrastructure.Common;

namespace Infrastructure.Migrations;

[Migration(6)]
public class CreateUserRefreshTokenTable : Migration
{
    public override void Up()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.UserRefreshTokenTableName).Exists())
        {
            return;
        }

        Create.Table(DatabaseConstants.UserRefreshTokenTableName)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("token").AsGuid().PrimaryKey()
            .WithColumn("expiration_date").AsDateTime()
            .WithColumn("user_id").AsGuid();

        Create.ForeignKey("fk_refresh_token_user_id")
            .FromTable(DatabaseConstants.UserRefreshTokenTableName)
            .InSchema(DatabaseConstants.Schema)
            .ForeignColumn("user_id")
            .ToTable(DatabaseConstants.UserTableName)
            .InSchema(DatabaseConstants.Schema)
            .PrimaryColumn("id");

        Execute.Sql($@"
            CREATE UNIQUE INDEX {DatabaseConstants.UserRefreshTokenTableName}_unique_user_id
            ON {DatabaseConstants.Schema}.{DatabaseConstants.UserRefreshTokenTableName} (user_id)"
        );
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.UserRefreshTokenTableName).Exists())
        {
            Delete.Table(DatabaseConstants.UserRefreshTokenTableName);
        }
    }
}