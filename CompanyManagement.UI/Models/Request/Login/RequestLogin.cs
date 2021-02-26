using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Request.Login
{
    public class RequestLogin
    {
        public long CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BusinessType { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ConLogRequest
    {
        [JsonProperty("stack")]
        public string Stack { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
