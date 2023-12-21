﻿using EShop.Entities.ErrorModels;
using EShop.Entities.Exceptions;
using EShop.LoggerService;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace EShop.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error
                        switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            ValidationAppException => StatusCodes.Status422UnprocessableEntity,
                            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        if (contextFeature.Error is ValidationAppException exception)
                        {
                            await context.Response
                            .WriteAsync(JsonSerializer.Serialize(new { exception.Errors }));
                        }
                        else
                        {
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message,
                            }.ToString());
                        }
                    }
                });
            });
        }
    }
}
