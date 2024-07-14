using FluentMigrator;

using Infrastructure.Common;

namespace Infrastructure.Migrations;

[Migration(5)]
public class CreateNoteHistoryTable : Migration
{
    public override void Up()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.NoteHistoryTableName).Exists())
        {
            return;
        }

        Create.Table(DatabaseConstants.NoteHistoryTableName)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("id").AsInt32().PrimaryKey()
            .WithColumn("note_id").AsGuid()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("content").AsString()
            .WithColumn("timestamp").AsDateTime();

        Create.Index()
            .OnTable(DatabaseConstants.NoteHistoryTableName)
            .OnColumn("id").Ascending()
            .OnColumn("timestamp").Ascending();

        Create.ForeignKey("FK_note_history_note")
            .FromTable(DatabaseConstants.NoteHistoryTableName).ForeignColumn("note_id")
            .ToTable(DatabaseConstants.NoteTableName).PrimaryColumn("id");
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.NoteHistoryTableName).Exists())
        {
            Delete.Table(DatabaseConstants.NoteHistoryTableName);
        }
    }
}
