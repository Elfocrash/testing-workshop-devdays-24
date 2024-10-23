using Microsoft.AspNetCore.Mvc;

namespace ForeignExchange.Api.Validation;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _request;

    public ValidationExceptionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = 400;
            
            var error = new ValidationProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Status = 400,
                Extensions =
                {
                    ["traceId"] = context.TraceIdentifier
                }
            };
            
            error.Errors.Add(new KeyValuePair<string, string[]>(
                exception.PropertyName, 
                new[] { exception.Message }));
            
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
