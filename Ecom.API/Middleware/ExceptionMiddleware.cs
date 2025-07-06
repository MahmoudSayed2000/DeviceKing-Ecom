using System.Net;
using System.Text.Json;
using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;

namespace Ecom.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
    public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
    {
        _next = next;
        _environment = environment;
        _memoryCache = memoryCache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (IsRequestAllowed(context) == false)
            {
                context.Response.StatusCode =  (int)HttpStatusCode.TooManyRequests;
                context.Response.ContentType = "application/json";
                var response = 
                    new ApiExceptions((int)HttpStatusCode.TooManyRequests,"TooManyRequests Please Try Again Later");

                await context.Response.WriteAsJsonAsync(response);

            }
            _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode =  (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = _environment.IsDevelopment() ?
                new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
    private bool IsRequestAllowed(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress.ToString();
        var cachekey = $"Rate:{ipAddress}";
        var dateNow = DateTime.Now;

        var (timeStamp, count) = _memoryCache.GetOrCreate(cachekey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
            return (timeStamp: dateNow, count: 0);
        });
        
        if (dateNow - timeStamp < _rateLimitWindow)
        {
            if(count >= 8)
            {
                return false;
            }
            _memoryCache.Set(cachekey, (timeStamp, count + 1), _rateLimitWindow);
        }
        else
        {
            _memoryCache.Set(cachekey, (timeStamp, count), _rateLimitWindow);
        }
        return true;
    }

    private void ApplySecurity(HttpContext context)
    {
        context.Response.Headers["X-Content-Type-Option" ]= "nosniff";
        context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        context.Response.Headers["X-Frame-Options"] = "DENY";
    }
}