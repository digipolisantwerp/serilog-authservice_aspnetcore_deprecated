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

            var userProp = new LogEventProperty(AuthServiceLoggingProperties.MessageUser, new ScalarValue(authService.User.Identity?.Name ?? AuthServiceLoggingProperties.NullValue));
            var isAuthenticatedProp = new LogEventProperty(AuthServiceLoggingProperties.MessageUserIsAuthenticated, new ScalarValue(authService.User.Identity?.IsAuthenticated ?? false));
            logEvent.AddOrUpdateProperty(userProp);
            logEvent.AddOrUpdateProperty(isAuthenticatedProp);
        }
    }
}
