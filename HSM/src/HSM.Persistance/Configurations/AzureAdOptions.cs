using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Persistance.Configurations
{
    public class AzureAdOptions
    {
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string Tenant { get; set; }
        public string Authority => Instance + Tenant;
    }
}
