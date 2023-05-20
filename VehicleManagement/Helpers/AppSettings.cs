using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string MailHost { get; set; }
        public int MailPort { get; set; }
        public string MailAddress { get; set; }
        public string MailPassword { get; set; }
        public string StripeApiKey { get; set; }
        public string PriceId { get; set; }
        public string StripeSuccessUrl { get; set; }
        public string StripeCancelUrl { get; set; }
    }
}
