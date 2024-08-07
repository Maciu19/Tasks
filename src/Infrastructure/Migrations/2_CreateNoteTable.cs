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
            .WithColumn("user_id").AsGuid()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("content").AsString()
            .WithColumn("last_edited").AsDateTime()
            .WithColumn("due_date").AsDateTime().Nullable()
            .WithColumn("fixed").AsBoolean()
            .WithColumn("background").AsString().Nullable()
            .WithColumn("deleted").AsBoolean();

        Create.ForeignKey("FK_note_user")
            .FromTable(DatabaseConstants.NoteTableName).ForeignColumn("user_id")
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
