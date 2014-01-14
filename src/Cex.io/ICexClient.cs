using System;
using System.Linq;

namespace Nextmethod.Cex
{
    /// <summary>
    /// Marker interface for Extension Methods
    /// </summary>
    internal interface ICexClient
    {

        ApiCredentials Credentials { get; }

        Func<Uri> BasePathFactory { get; }
        
        TimeSpan? Timeout { get; set; }

    }
}
