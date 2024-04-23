using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Persistance.DependencyInjection.Options
{
    public record SqlServerRetryOptions
    {
        [Required, Range(5, 20)] public int MaxRetryCount { get; init; }
        [Required, Timestamp] public TimeSpan MaxRetryDelay { get; init; }
        public int[]? ErrorNumbersToAdd { get; init; }
    }
}
