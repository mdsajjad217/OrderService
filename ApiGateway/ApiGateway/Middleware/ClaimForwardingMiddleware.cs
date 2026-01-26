using System.Security.Claims;

namespace ApiGateway.Middleware;

public class ClaimForwardingMiddleware
{
    private readonly RequestDelegate _next;

    public ClaimForwardingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? context.User.FindFirst("sub")?.Value;

            var username = context.User.FindFirst("preferred_username")?.Value;

            var roles = context.User.FindAll("roles")
                .Select(r => r.Value)
                .ToArray();

            context.Request.Headers.TryAdd("X-User-Id", userId);
            context.Request.Headers.TryAdd("X-Username", username);
            context.Request.Headers.TryAdd("X-Roles", string.Join(",", roles));
        }

        await _next(context);
    }
}
