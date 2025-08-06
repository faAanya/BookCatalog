using System.Text.Json;

public class BookValidationMiddleware
{
    private readonly RequestDelegate _next;

    public BookValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/books") &&
            (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put))
        {

            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var bodyString = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(bodyString))
            {
                try
                {
                    var jsonDoc = JsonDocument.Parse(bodyString);
                    var root = jsonDoc.RootElement;

                    if (root.TryGetProperty("publicationYear", out var yearProp) &&
                        yearProp.GetInt32() < 0)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new { error = "PublicationYear must be non-negative." });
                        return;
                    }

                    if (root.TryGetProperty("pageCount", out var pagesProp) &&
                        pagesProp.GetInt32() < 0)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new { error = "PageCount must be non-negative." });
                        return;
                    }
                }
                catch (JsonException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new { error = "Invalid JSON format." });
                    return;
                }
            }
        }

        await _next(context);
    }
}
public static class BookValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseBookValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BookValidationMiddleware>();
    }
}