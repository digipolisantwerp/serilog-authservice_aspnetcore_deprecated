using System;
using Digipolis.Auth.Services;
using Digipolis.Serilog.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;

namespace Digipolis.Serilog.Enrichers
{
    public class AuthServiceEnricher : ILogEventEnricher
    {
        public AuthServiceEnricher(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private readonly IHttpContextAccessor _accessor;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = _accessor.HttpContext;
            var authService = httpContext?.RequestServices?.GetService<IAuthService>();
            if ( authService == null ) return;

            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(AuthServiceLoggingProperties.MessageUser, authService.User.Identity?.Name ?? AuthServiceLoggingProperties.NullValue));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(AuthServiceLoggingProperties.MessageUserIsAuthenticated, authService.User.Identity?.IsAuthenticated ?? false));
        }
    }
}
