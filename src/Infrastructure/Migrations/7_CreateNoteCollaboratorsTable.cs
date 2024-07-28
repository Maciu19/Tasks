using FluentMigrator;

using Infrastructure.Common;

namespace Infrastructure.Migrations;

[Migration(7)]
public class CreateNoteCollaboratorsTable : Migration
{
    public override void Up()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.NoteCollaboratorsTableName).Exists())
        {
            return;
        }

        Create.Table(DatabaseConstants.NoteCollaboratorsTableName)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("note_id").AsGuid()
            .WithColumn("user_id").AsGuid();   
        
        Create.PrimaryKey("PK_note_collaborators")
            .OnTable(DatabaseConstants.NoteCollaboratorsTableName)
            .WithSchema(DatabaseConstants.Schema)
            .Columns("note_id", "user_id");

        Create.ForeignKey("FK_note_collaborators_note")
            .FromTable(DatabaseConstants.NoteCollaboratorsTableName).ForeignColumn("note_id")
            .ToTable(DatabaseConstants.NoteTableName).PrimaryColumn("id");

        Create.ForeignKey("FK_note_collaborators_user")
            .FromTable(DatabaseConstants.NoteCollaboratorsTableName).ForeignColumn("user_id")
            .ToTable(DatabaseConstants.UserTableName).PrimaryColumn("id");
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.NoteCollaboratorsTableName).Exists())
        {
            Delete.Table(DatabaseConstants.NoteCollaboratorsTableName);
        }
    }
}
