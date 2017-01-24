using System;
using System.Linq;
using Digipolis.Serilog.Enrichers;

namespace Digipolis.Serilog
{
    public static class AddAuthServiceEnricherExt
    {
        public static SerilogExtensionsOptions AddAuthServiceEnricher(this SerilogExtensionsOptions options)
        {
            if ( !options.EnricherTypes.Contains(typeof(AuthServiceEnricher)) )
                options.AddEnricher<AuthServiceEnricher>();
            return options;
        }
    }
}
