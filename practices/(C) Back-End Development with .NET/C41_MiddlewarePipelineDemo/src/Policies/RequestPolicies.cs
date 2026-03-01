using Microsoft.Extensions.Primitives;

namespace MiddlewarePipelineDemo.Policies;

public sealed class RequestPolicies
{
    public bool RequiresSecureQuery(HttpContext context)
        => context.Request.Query.TryGetValue("secure", out var value) && value == "true";

    public bool IsUnauthorizedPath(HttpContext context)
        => context.Request.Path == "/security-demo/unauthorized";

    public bool IsAuthenticated(HttpContext context)
        => context.Request.Query.TryGetValue("authenticated", out var value) && value == "true";

    public bool IsValidInput(HttpContext context)
    {
        context.Request.Query.TryGetValue("input", out StringValues input);

        var value = input.ToString();

        if (string.IsNullOrEmpty(value))
            return true;

        return value.All(char.IsLetterOrDigit)
               && !value.Contains("<script>", StringComparison.OrdinalIgnoreCase);
    }
}