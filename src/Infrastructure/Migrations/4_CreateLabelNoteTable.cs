using FluentMigrator;

using Infrastructure.Common;

namespace Infrastructure.Migrations;

[Migration(4)]
public class CreateLabelNoteTable : Migration
{
    public override void Up()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.LabelNoteTableName).Exists())
        {
            return;
        }

        Create.Table(DatabaseConstants.LabelNoteTableName)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("label_id").AsInt32()
            .WithColumn("note_id").AsGuid()
            .WithColumn("fixed").AsBoolean();

        Create.PrimaryKey("PK_label_note")
            .OnTable(DatabaseConstants.LabelNoteTableName)
            .WithSchema(DatabaseConstants.Schema)
            .Columns(["label_id", "note_id"]);

        Create.ForeignKey("FK_label_note_label")
            .FromTable(DatabaseConstants.LabelNoteTableName).ForeignColumn("label_id")
            .ToTable(DatabaseConstants.LabelTableName).PrimaryColumn("id");

        Create.ForeignKey("FK_label_note_note")
            .FromTable(DatabaseConstants.LabelNoteTableName).ForeignColumn("note_id")
            .ToTable(DatabaseConstants.NoteTableName).PrimaryColumn("id");
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.LabelNoteTableName).Exists())
        {
            Delete.Table(DatabaseConstants.LabelNoteTableName);
        }
    }
}
