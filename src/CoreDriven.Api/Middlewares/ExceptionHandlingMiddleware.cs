using CoreDriven.Utils.Response;

namespace CoreDriven.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
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
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            UnauthorizedAccessException unauthorizedException => new
            {
                statusCode = StatusCodes.Status401Unauthorized,
                apiResponse = Res<object>.Fail(
                    unauthorizedException.Message,
                    "UNAUTHORIZED"
                )
            },
            KeyNotFoundException notFoundException => new
            {
                statusCode = StatusCodes.Status404NotFound,
                apiResponse = Res<object>.Fail(
                    notFoundException.Message,
                    "NOT_FOUND"
                )
            },
            ArgumentException argumentException => new
            {
                statusCode = StatusCodes.Status400BadRequest,
                apiResponse = Res<object>.Fail(
                    argumentException.Message,
                    "BAD_REQUEST"
                )
            },
            _ => new
            {
                statusCode = StatusCodes.Status500InternalServerError,
                apiResponse = Res<object>.Fail(
                    "An internal server error occurred","INTERNAL_ERROR"
                )
            }
        };

        context.Response.StatusCode = response.statusCode;
        await context.Response.WriteAsJsonAsync(response.apiResponse);
    }
}