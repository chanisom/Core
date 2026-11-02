namespace Middleware;

public class StudyTasksMiddleware
{
    private readonly RequestDelegate next;

    public StudyTasksMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context, IConfiguration config)
    {
    if (config.GetValue<bool>("StudyTasksDisabled") &&
    context.Request.Path.StartsWithSegments("/StudyTasks"))
    {
        context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Study Tasks is temporarily unavailable.");
        return;
    }

    await next.Invoke(context);
    }
}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseStudyTasks(this IApplicationBuilder builder)
        => builder.UseMiddleware<StudyTasksMiddleware>();
}