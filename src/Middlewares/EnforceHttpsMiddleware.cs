﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Middlewares
{
    public class EnforceHttpsMiddleware
    {
        private readonly RequestDelegate _next;

        public EnforceHttpsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=15552001; includeSubDomains; preload");
            if (!context.Request.IsHttps)
            {
                await HandleNonHttpsRequest(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
        protected virtual async Task HandleNonHttpsRequest(HttpContext context)
        {
            if (!string.Equals(context.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else
            {
                string newUrl = string.Empty;
                await Task.Run(() =>
                {
                    var optionsAccessor = context.RequestServices.GetRequiredService<IOptions<MvcOptions>>();
                    var request = context.Request;
                    var host = request.Host;
                    if (optionsAccessor.Value.SslPort.HasValue && optionsAccessor.Value.SslPort > 0)
                    {
                        host = new HostString(host.Host, optionsAccessor.Value.SslPort.Value);
                    }
                    else
                    {
                        host = new HostString(host.Host);
                    }
                    newUrl = string.Concat(
                        "https://",
                        host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent());
                });
                context.Response.Redirect(newUrl, permanent: true);
            }
        }
    }
}
