using Newtonsoft.Json;

namespace CompanyManagement.Api.Models
{
    public class RequestBase
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
    }
    public class RequestCompanyUrl
    {
        [JsonProperty("companyUrl")]
        public string CompanyUrl { get; set; }
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



}
