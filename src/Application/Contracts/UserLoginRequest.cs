﻿namespace Application.Contracts;

public record UserLoginRequest (
    string Email,
    string Password
);