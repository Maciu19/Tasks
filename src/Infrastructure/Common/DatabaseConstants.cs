﻿namespace Infrastructure.Common;

public static class DatabaseConstants
{
    public const string Schema = "public";

    public const string NoteTableName = "note";
    public const string UserTableName = "user";
    public const string LabelTableName = "label";
    public const string LabelNoteTableName = "label_note";
    public const string NoteHistoryTableName = "note_history";
    public const string UserRefreshTokenTableName = "user_refresh_token";
    public const string NoteCollaboratorsTableName = "note_collaborators";
}
