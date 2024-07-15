using FluentMigrator;

using Infrastructure.Common;

namespace Infrastructure.Migrations;

[Migration(3)]
public class CreateLabelTable : Migration
{
    public override void Up()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.LabelTableName).Exists())
        {
            return;
        }

        Create.Table(DatabaseConstants.LabelTableName)
           .InSchema(DatabaseConstants.Schema)
           .WithColumn("id").AsInt32().Identity().PrimaryKey()
           .WithColumn("user_id").AsGuid()
           .WithColumn("name").AsString();

        Create.ForeignKey("FK_label_user")
            .FromTable(DatabaseConstants.NoteTableName).ForeignColumn("user_id")
            .ToTable(DatabaseConstants.UserTableName).PrimaryColumn("id");

        Create.Index("IX_label_name_user_id")
            .OnTable(DatabaseConstants.LabelTableName)
            .InSchema(DatabaseConstants.Schema)
            .OnColumn("name").Ascending()
            .OnColumn("user_id").Ascending()
            .WithOptions().Unique();
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.LabelTableName).Exists())
        {
            Delete.Table(DatabaseConstants.LabelTableName);
        }
    }
}
