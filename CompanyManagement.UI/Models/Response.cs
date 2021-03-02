using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wizard.ImageManagement.Models.Response;

namespace CompanyManagement.UI.Models
{
    public class Response<T>
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
    public class ResponseList<T>
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
    public class ResponseStatus
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
