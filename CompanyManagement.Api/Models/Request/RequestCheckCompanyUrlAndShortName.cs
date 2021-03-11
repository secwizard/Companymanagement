using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models.Request
{
    public class RequestCheckCompanyUrlAndShortName
    {
        public string CompShortName { get; set; }
        public string CompUrl { get; set; }
    }
}
