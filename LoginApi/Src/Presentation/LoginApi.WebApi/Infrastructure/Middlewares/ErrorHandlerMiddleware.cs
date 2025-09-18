using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using HyBrForex.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HyBrForex.WebApi.Infrastructure.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<Program> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = BaseResult.Failure();

            switch (error)
            {
                case ValidationException e:
                    // validation error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    foreach (var validationFailure in e.Errors)
                    {
                        logger.LogInformation(
                            $"validationFailure:-StatusCode:-{response.StatusCode},Message:-{validationFailure.ErrorMessage},Field:-{validationFailure.PropertyName}");
                        responseModel.AddError(new Error(ErrorCode.ModelStateNotValid, validationFailure.ErrorMessage,
                            validationFailure.PropertyName));
                    }

                    break;

                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.LogInformation(
                        $"NotFound Exception:-StatusCode:-{response.StatusCode},Message:-{e.Message}");
                    responseModel.AddError(new Error(ErrorCode.NotFound, e.Message));
                    break;

                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    logger.LogInformation($"Exception:-StatusCode:-{response.StatusCode},Message:-{error.Message}");
                    responseModel.AddError(new Error(ErrorCode.Exception, error.Message));
                    break;
            }

            var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(result);
        }
    }
}