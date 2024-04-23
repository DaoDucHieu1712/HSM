using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Persistance.Constants
{
    internal static class TableNames
    {
        // For Oubox pattern
        internal const string OutboxMessages = nameof(OutboxMessages);
        // Singular Nouns
        internal const string Department = nameof(Department);
    }
}
