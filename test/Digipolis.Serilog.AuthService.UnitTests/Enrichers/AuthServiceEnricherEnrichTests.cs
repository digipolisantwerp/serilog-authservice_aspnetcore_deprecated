using System;
using System.Collections.Generic;
using System.Security.Claims;
using Digipolis.Auth.Services;
using Digipolis.Serilog.Enrichers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Serilog.Events;
using Serilog.Parsing;
using Xunit;

namespace Digipolis.Serilog.AuthService.UnitTests.Enrichers
{
    public class AuthServiceEnricherEnrichTests
    {
        [Fact]
        void MessageUserIsAdded()
        {
            var accessor = CreateHttpContextAccessor();
            var enricher = new AuthServiceEnricher(accessor);
            var logEvent = CreateLogEvent();

            enricher.Enrich(logEvent, null);

            Assert.Contains(AuthServiceLoggingProperties.MessageUser, logEvent.Properties.Keys);
        }

        [Fact]
        void MessageUserIsAuthenticatedIsAdded()
        {
            var accessor = CreateHttpContextAccessor();
            var enricher = new AuthServiceEnricher(accessor);
            var logEvent = CreateLogEvent();

            enricher.Enrich(logEvent, null);

            Assert.Contains(AuthServiceLoggingProperties.MessageUserIsAuthenticated, logEvent.Properties.Keys);
        }

        private IHttpContextAccessor CreateHttpContextAccessor()
        {
            var authService = new Mock<IAuthService>();
            authService.SetupGet(x => x.User).Returns(new ClaimsPrincipal());

            var services = new ServiceCollection();
            services.AddOptions();
            services.AddSingleton<IAuthService>(authService.Object);

            var accessor = new HttpContextAccessor();
            accessor.HttpContext = new DefaultHttpContext();
            accessor.HttpContext.RequestServices = services.BuildServiceProvider();

            return accessor;
        }

        private LogEvent CreateLogEvent()
        {
            var tokens = new List<MessageTemplateToken>();
            var properties = new List<LogEventProperty>();
            var logEvent = new LogEvent(DateTime.Now, LogEventLevel.Information, null, new MessageTemplate(tokens), properties);
            return logEvent;
        }
    }
}
