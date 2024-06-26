﻿using FluentMigrator;

using Infrastructure.Common;

namespace Infrastructure.Migrations;

[Migration(2)]
public class CreateNoteTable : Migration
{
    public override void Up()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.NoteTableName).Exists())
        {
            return;
        }

        Create.Table(DatabaseConstants.NoteTableName)
            .InSchema(DatabaseConstants.Schema)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("userId").AsGuid()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("text").AsString()
            .WithColumn("deleted").AsBoolean()
            .WithColumn("fixed").AsBoolean()
            .WithColumn("due_date").AsDateTime().Nullable();

        Create.ForeignKey("FK_note_user")
            .FromTable(DatabaseConstants.NoteTableName).ForeignColumn("userId")
            .ToTable(DatabaseConstants.UserTableName).PrimaryColumn("id");
    }

    public override void Down()
    {
        if (Schema.Schema(DatabaseConstants.Schema).Table(DatabaseConstants.NoteTableName).Exists())
        {
            Delete.Table(DatabaseConstants.NoteTableName);
        }
    }
}
