using Newtonsoft.Json;

namespace CompanyManagement.Api.Models
{
    public class RequestBase
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
    }

    public class RequestCompanySetting
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
        [JsonProperty("settingType")]
        public string SettingType { get; set; }
        [JsonProperty("dataText")]
        public string DataText { get; set; }
    }
    public class RequestLookUp
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
        [JsonProperty("lookuptype")]
        public string LookUpType { get; set; }
    }



}
