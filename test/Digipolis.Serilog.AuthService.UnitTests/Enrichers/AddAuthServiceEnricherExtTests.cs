using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Xunit;

namespace Digipolis.Serilog.AuthService.UnitTests.Enrichers
{
    public class AddAuthServiceEnricherExtTests
    {
        [Fact]
        void AuthServiceEnricherIsAdded()
        {
            var options = new SerilogExtensionsOptions();
            options.AddAuthServiceEnricher();
            Assert.Collection(options.EnricherTypes, item => Assert.Equal(typeof(AuthServiceEnricher), item));
        }

        [Fact]
        void AuthServiceEnricherIsAddedOnlyOnce()
        {
            var options = new SerilogExtensionsOptions();
            options.AddAuthServiceEnricher();
            options.AddAuthServiceEnricher();
            Assert.Collection(options.EnricherTypes, item => Assert.Equal(typeof(AuthServiceEnricher), item));
        }

        [Fact]
        void AuthServiceEnricherIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();
            services.AddSerilogExtensions(options => {
                options.MessageVersion = "1";
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
                options.MessageVersion = "1";
                options.AddAuthServiceEnricher();
            });

            var registrations = services.Where(sd => sd.ServiceType == typeof(IHttpContextAccessor))
                                                     .ToArray();

            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }
    }
}
