using BO.TestTask.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BO.TestTask.Api.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException)
        {
            return false;
        }

        ProblemDetails model = new()
        {
            Status = (int)HttpStatusCode.NotFound,
            Type = exception.GetType().Name,
            Title = "An error occurred",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };
        httpContext.Response.StatusCode = model.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(model, cancellationToken);

        return true;
    }
}
