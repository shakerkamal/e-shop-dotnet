﻿namespace EShop.Shared.DataTransferObjects.AuthenticationDtos;

public record TokenDto(string AccessToken, string RefreshToken);
