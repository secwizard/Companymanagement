using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models
{
    public class ResponseList<T>
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class Response<T>
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class CompanyInfo
    {
        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PIN { get; set; }
        public string DistrictCode { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public string AdminPhone { get; set; }
        public string ServicePhone { get; set; }
        public string AdminEmail { get; set; }
        public string ServiceEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string GSTNumber { get; set; }
        public string PanNumber { get; set; }
        public string BusinessType { get; set; }
        public string CurrencyCode { get; set; }
        public string ImageFilePath { get; set; }
        public string LogoFileName { get; set; }
        public string FavIconFileName { get; set; }
        public string LoginImageFileName { get; set; }
        public string Website { get; set; }
        public bool? PINRequired { get; set; }
    }


}
