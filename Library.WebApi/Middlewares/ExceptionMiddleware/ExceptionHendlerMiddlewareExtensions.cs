namespace Library.WebApi.Middlewares.ExceptionMiddleware;

public static class ExceptionHendlerMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHendlerMiddleware>();
    }
}
