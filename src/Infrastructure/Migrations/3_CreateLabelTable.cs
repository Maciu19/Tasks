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
           .WithColumn("id").AsInt32().PrimaryKey()
           .WithColumn("userId").AsGuid()
           .WithColumn("name").AsString().Unique();

        Create.ForeignKey("FK_label_user")
            .FromTable(DatabaseConstants.NoteTableName).ForeignColumn("userId")
            .ToTable(DatabaseConstants.UserTableName).PrimaryColumn("id");
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.LabelTableName).Exists())
        {
            Delete.Table(DatabaseConstants.LabelTableName);
        }
    }
}
