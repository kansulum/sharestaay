using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment evn;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment evn)
        {
            this.evn = evn;
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "applicaton/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = evn.IsDevelopment()
                 ? new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                 : new ApiException((int)HttpStatusCode.InternalServerError);
                 
                var options = new JsonSerializerOptions{PropertyNamingPolicy= JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}