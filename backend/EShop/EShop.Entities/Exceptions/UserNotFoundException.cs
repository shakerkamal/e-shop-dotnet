﻿namespace EShop.Entities.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string? message) : base(message)
    {
    }
}
