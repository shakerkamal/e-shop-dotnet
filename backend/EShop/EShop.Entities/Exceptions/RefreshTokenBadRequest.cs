﻿namespace EShop.Entities.Exceptions;

public class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest() : base("Invalid request. The token object has invalid values.")
    {
    }
}
