using System;
using Digipolis.Serilog.Enrichers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog.Core;

namespace Digipolis.Serilog
{
    public static class AddAuthServiceEnricherExt
    {
        public static SerilogExtensionsOptions AddAuthServiceEnricher(this SerilogExtensionsOptions options)
        {
            options.ApplicationServices.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            options.ApplicationServices.TryAddSingleton<ILogEventEnricher, AuthServiceEnricher>();
            return options;
        }
    }
}
