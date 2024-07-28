using System.Net;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Presentation.Api;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        switch (exception)
        {
            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                await response.WriteAsync(exception.Message);
                LogInformation(exception, (int)HttpStatusCode.NotFound, exception.Message);
                break;
            case ForbidenException:
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                await response.WriteAsync(exception.Message);
                LogInformation(exception, (int)HttpStatusCode.Forbidden, exception.Message);
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(exception.Message);
                LogInformation(exception, (int)HttpStatusCode.InternalServerError, exception.Message);
                break;
        }
    }

    /// <summary>
    ///     Cette méthode permet logguer les informations
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="statusCode"></param>
    /// <param name="responseModel"></param>
    /// <param name="isWarning"></param>
    private void LogInformation(Exception exception, int statusCode, string message, bool isWarning = true)
    {
        if (isWarning)
            logger.LogWarning(exception, "Alerte Application : [{StatutCode} - {Message}]", statusCode, message);
        else
            logger.LogError(exception, "Erreur Application : [{StatutCode} - {Message}]", statusCode, message);
    }
}