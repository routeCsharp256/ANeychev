using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogHeaders(context);
            await LogRequest(context);
            await _next(context);
            await LogResponse(context);
        }

        private async Task LogHeaders(HttpContext context)
        {
            _logger.LogInformation($"{context.Request.Method ?? "no method"} {context.Request.Path.Value}");
            var headers = context.Request.Headers;
            string result = await Task.Run(() => context.Request.Headers.ToArray()
                .Aggregate(string.Empty,
                    (current, header) =>
                        current + ($"{header.Key} : {header.Value.ToString()}" + Environment.NewLine)));
            _logger.LogInformation(result);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();

                    var buffer = new byte[context.Request.ContentLength.Value];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    _logger.LogInformation("Request body:" + Environment.NewLine + bodyAsText);
                    _logger.LogInformation(bodyAsText);

                    context.Request.Body.Position = 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request body");
            }
        }

        private async Task LogResponse(HttpContext context)
        {
            try
            {
                if (context.Response.ContentLength > 0)
                {
                    var buffer = new byte[context.Response.ContentLength.Value];
                    await context.Response.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    _logger.LogInformation("Response body:" + Environment.NewLine + bodyAsText);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response body");
            }
        }
    }
}