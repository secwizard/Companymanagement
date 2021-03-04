using Newtonsoft.Json;
using System;

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
    public class RequestLookUp
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
        [JsonProperty("lookuptype")]
        public string LookUpType { get; set; }
    }

    public class DeleteCompanyTemplate
    {
        public long TemplateId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
    public class DeleteCompanyTheme
    {
        public long ThemeId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
    public class DeleteCompanySettings
    {
        public long CompanySettingsId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
    public class DeleteCompanyBranch
    {
        public long BranchId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}
