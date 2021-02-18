using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models
{
    public class RequestBase
    {
        [JsonProperty("companyid")]
        public int CompanyId { get; set; }
    }

    
}
