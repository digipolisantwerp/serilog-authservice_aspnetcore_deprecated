using System;
using System.Linq;
using Digipolis.Serilog.Enrichers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Xunit;

namespace Digipolis.Serilog.AuthService.UnitTests.Enrichers
{
    public class AddAuthServiceEnricherExtTests
    {
        [Fact]
        void AuthServiceEnricherIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();
            services.AddSerilogExtensions(options => {
                options.AddAuthServiceEnricher();
            });

            var registrations = services.Where(sd => sd.ServiceType == typeof(ILogEventEnricher) &&
                                                     sd.ImplementationType == typeof(AuthServiceEnricher))
                                                     .ToArray();

            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }

        [Fact]
        void IHttpContextAccessorIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();
            services.AddSerilogExtensions(options => {
                options.AddAuthServiceEnricher();
            });

            var registrations = services.Where(sd => sd.ServiceType == typeof(IHttpContextAccessor))
                                                     .ToArray();

            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }
    }
}
