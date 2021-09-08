using Newtonsoft.Json;
using System;

namespace CompanyManagement.Api.Models
{
    public class RequestBase
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
    }
    public class RequestCompany
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
    public class RequestCompanyUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }
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
    public class NewCompanyDetails
    {
        public Int64 CompanyId { get; set; }
        public Int64 NewCompanyId { get; set; }
        public Guid UserId { get; set; }


    }
    public class BusinessType
    {
        public string Type { get; set; }
        public Int64 CompanyId { get; set; }
    }

    public class RequestMail
    {
        public string To { get; set; }
        public string From { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool EnableSsl { get; set; }
        public string DisplayName { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class RequestSendMail
    {
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
        public string EmailFrom { get; set; }
        public string EmailCC { get; set; }
        public string EmailTo { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
    public class RequestCompanyTemplate
    {
        [JsonProperty("companyid")]
        public long CompanyId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

    }

}
