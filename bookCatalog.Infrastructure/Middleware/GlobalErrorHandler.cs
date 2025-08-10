// public class GlobalErrorHandlerMiddleware
// {
//     private readonly RequestDelegate _next;

//     public GlobalErrorHandlerMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task InvokeAsync(HttpContext context)
//     {
//         try
//         {
//             await _next(context);
//         }
//         catch (Exception e)
//         {
//             await context.Response.WriteAsJsonAsync($"Error message: {e.Message}");
//         }
//     }
// }
// public static class RequestCultureMiddlewareExtensions
// {
//     public static IApplicationBuilder UseGlobalErrorHandler(
//         this IApplicationBuilder builder)
//     {
//         return builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
//     }
// }