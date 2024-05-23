namespace Erm.PresentationLayer.WebApi.Authentication;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var providedKeyApiKey = context.Request.Headers[AuthConfig.AuthKeyHeader].FirstOrDefault();
        var isValid = IsValidApiKey(providedKeyApiKey);

        if (!isValid)
        {
            await GenerateResponse(context, 401, "Invalid Authentication");
            return;
        }

        await _next(context);
    }

    private bool IsValidApiKey(string providedApiKey)
    {
        if (string.IsNullOrEmpty(providedApiKey))
        {
            return false;
        }

        var validApiKey = _configuration.GetValue<string>(AuthConfig.AuthSection);

        return string.Equals(validApiKey, providedApiKey, StringComparison.Ordinal);
    }

    public static async Task GenerateResponse(HttpContext context, int httpStatusCode, string msg)
    {
        context.Response.StatusCode = httpStatusCode;
        await context.Response.WriteAsync(msg);
    }
}