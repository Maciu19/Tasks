﻿namespace Domain.Access;

public record RefreshToken(
    Guid Token,
    DateTime ExpirationDate,
    Guid UserId
)
{
    private RefreshToken() : this(Guid.Empty, DateTime.MinValue, Guid.Empty) { }
}
