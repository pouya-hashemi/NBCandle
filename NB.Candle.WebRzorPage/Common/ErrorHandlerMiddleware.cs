using Microsoft.AspNetCore.Http;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace NB.Candle.WebRzorPage.Common
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private  ApplicationDbContext _applicationDbContext;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                
            }
            catch (Exception error)
            {
                this._applicationDbContext = context.RequestServices.GetService<ApplicationDbContext>(); ;
                var log = new Log()
                {
                    ID = Guid.NewGuid(),
                    DateTime=DateTime.Now,
                    Query=context.Request.QueryString.Value,
                    Method=context.Request.Method,
                    Request=context.Request.Path,
                    Message=error.Message,
                    InnerMessage=error.InnerException?.Message
                };
                _applicationDbContext.Logs.Add(log);
                await _applicationDbContext.SaveChangesAsync();

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                //await context.Response.WriteAsync(result);
                 context.Response.Redirect("/Error");

            }
        }
    }
}
