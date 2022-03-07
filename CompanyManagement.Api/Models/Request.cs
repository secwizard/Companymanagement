using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
    public class RequestItemBySectionId
    {
        public int SectionId { get; set; }
    }
    public class RequestGetCompanyTemplateByTypeAndName
    {
        public long CompanyId { get; set; }
        public string TemplateType { get; set; }
        public string Name { get; set; }
    }

    public class RequestAddCompanyTemplate
    {
        public int TemplateId { get; set; }
        public long CompanyId { get; set; }
        public bool IsForB2C { get; set; }
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; }
    }
    public class RequestEditCompanyTemplate
    {
        public int CompanyTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public bool IsForB2C { get; set; }
        public bool IsActive { get; set; }
        public int? FontFamilyId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class RequestGetCompanyTemplateById
    {
        public int CompanyTemplateId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class RequestEditCompanyTemplateSection
    {
        public int CompanyTemplateSectionId { get; set; }
        public long CompanyId { get; set; }
        public string SectionName { get; set; }
        public string SectionBackgroundColor { get; set; }

        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
    }
    public class RequestEditCompanyTemplateSectionOrder
    {
        public int[] CompanyTemplateSectionIds { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class RequestProductInclusiveOfTax
    {
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAllProductInclusiveOfTax { get; set; }
    }

    public class RequestAddSectionItem
    {
        public int CompanyTemplateSectionId { get; set; }
        public long CompanyId { get; set; }
        public List<RequestCompanyTemplateSectionItem> RequestCompanyTemplateSectionItems { get; set; }
        public Guid UserId { get; set; }
    }

    public class RequestCompanyTemplateSectionItem
    {
        public long ItemId { get; set; }
        public long VariantId { get; set; }
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public string ItemImage { get; set; }
        public string VariantImage { get; set; }
    }

    public class RequestEditSectionItemOrder
    {
        public int CompanyTemplateSectionId { get; set; }
        public long[] CompanyTemplateSectionItemMappingId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class RequestEditSectionImageOrder
    {
        public int CompanyTemplateSectionId { get; set; }
        public long[] CompanyTemplateSectionImageMappingId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class RequestChangeCompanyTemplateDefault
    {
        public int CompanyTemplateId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; }
    }

    public class RequestChangeCompanyTemplateB2C
    {
        public int CompanyTemplateId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        public bool IsForB2C { get; set; }
    }

    public class RequestDeleteCompanyTemplateSectionItem
    {
        public int CompanyTemplateSectionItemMappingId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class RequestDeleteCompanyTemplateSectionImage
    {
        public int CompanyTemplateSectionImageMappingId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
    public class RequestSaveNotificationServiceDetails
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string ServiceName { get; set; }
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }
        public string FromNumber { get; set; }
        public string SortCode { get; set; }
        public string APIKey { get; set; }
        public string SenderId { get; set; }
        public string URLLink { get; set; }
        public string SMTPServerAddress { get; set; }
        public string MailSendPort { get; set; }
        public string FromEmailId { get; set; }
        public string SMTPUserId { get; set; }
        public string SMTPPassword { get; set; }
        public bool IsSSLEnabled { get; set; }
        public string RoboCallFromNumber { get; set; }
        public string MessagingServiceSid { get; set; }

    }
    public class RequestGetNotificationSettingsServiceDetails
    {
        public long CompanyId { get; set; }
        public string ServiceName { get; set; }
    }
    public class RequestZoneSetting
    {
        public int ZoneId { get; set; }
        public long CompanyId { get; set; }
        public string ZoneName { get; set; }
        public string PatternValue { get; set; }
        public byte PatternType { get; set; }
        public bool Isdefault { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? CreatedOnUTC { get; set; }
        public DateTime? UpdatedOnUTC { get; set; }

    }
}