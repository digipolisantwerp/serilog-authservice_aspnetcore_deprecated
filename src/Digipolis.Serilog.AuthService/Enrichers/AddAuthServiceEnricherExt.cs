using System;
using System.Linq;

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
