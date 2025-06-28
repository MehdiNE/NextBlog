﻿namespace NextBlog.Api.DTOs.Auth
{
    public class RegisterRequest
    {
        public required string Email { get; init; }
        public required string Name { get; init; }
        public required string Password { get; init; }
        public required string ConfirmPassword { get; init; }
    }
}
