using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public List<LookUpInfo> LookUps { get; set; }
        public LookUpInfo SelectedLookUp { get; set; }
        public Int64 SuggestedCompanyId { get; set; }
        public string CompanySiteUrl { get; set; }
        public Currency Currency { get; set; }
        public string AdminPhoneCode { get; set; }
        public string ServicePhoneCode { get; set; }
        public string AdminPhoneCountryCode { get; set; }
        public string ServicePhoneCountryCode { get; set; }

    }

    public class CompanyMailServer
    {
        public long MailServerId { get; set; }
        public long CompanyId { get; set; }
        public string SMTPServer { get; set; }
        public int? SMTPPort { get; set; }
        public bool? EnableSSL { get; set; }
        public string FromEmailDisplayName { get; set; }
        public string FromEmailId { get; set; }
        public string FromEmailPwd { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class CompanyTheme
    {
        public long ThemeId { get; set; }
        public long CompanyId { get; set; }
        public string ThemeName { get; set; }
        public string ExtThemeName { get; set; }
        public decimal? ImageRatio { get; set; }
        public int? NoOfHomePanels { get; set; }
        public string Colour { get; set; }
        public int? MobileHeight { get; set; }
        public int? DesktopHeight { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class CompanySettingInfo
    {
        [Key]
        public long CompanySettingId { get; set; }
        public long CompanyId { get; set; }
        public string SettingType { get; set; }
        public string DataText { get; set; }
        public string DataValue { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }

    }

    public class BranchInfo
    {
        [Key]
        public long BranchId { get; set; }
        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class ResponseCompanyId
    {
        public long CompanyId { get; set; }
    }

    public class LookUpInfo
    {
        public string LookUpValue { get; set; }
        public string LookUpText { get; set; }


    }


    public class OnBoardProcessinfo
    {
        public OnBoardCompany OnBoardCompanyInfo { get; set; }
        public List<OnBoardSubscriptions> OnBoardSubscriptionInfo { get; set; }
        public List<OnBoardAddOns> OnBoardAddOn { get; set; }
        public OnBoardConfiguration OnBoardConfiguration { get; set; }
        public List<SelectedSubscription> Subscriptions { get; set; }
        public List<SelectedAddOn> AddOns { get; set; }
    }
    public class OnBoardCompany
    {
        public CompanyInfo CompanyInfo { get; set; }
        public List<BranchInfo> BranchInfo { get; set; }
        public CompanyMailServer MailServerInfo { get; set; }
        public List<CompanySettingInfo> CompanySettingInfo { get; set; }
        public List<GetCompanyTemplate> CompanyTemplate { get; set; }
        public List<CompanyTheme> CompanyTheme { get; set; }
    }
    public class OnBoardSubscriptions
    {
        public long SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Metric { get; set; }
        public string Inclusion { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class OnBoardAddOns
    {
        public long AddOnId { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Metric { get; set; }
        public string Inclusion { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class SelectedSubscription
    {
        public Int64 SubscriptionId { get; set; }
    }
    public class SelectedAddOn
    {
        public Int64 AddOnId { get; set; }
    }
    public class OnBoardConfiguration
    {

    }
    public class CompanyAllDetails
    {
        public CompanyInfo CompanyDtl { get; set; }
        public List<OnBoardSubscriptions> SubscriptionDtl { get; set; }
        public List<OnBoardAddOns> AddOnDtl { get; set; }
    }
    public class ResponseMail
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class FrontEndTemplate
    {
        [JsonProperty("templateid")]
        public long TemplateId { get; set; }

        [JsonProperty("templatename")]
        public string TemplateName { get; set; }

        [JsonProperty("webviewname")]
        public string WebViewName { get; set; }

        [JsonProperty("mobileviewname")]
        public string MobileViewName { get; set; }

        [JsonProperty("primarycolor")]
        public string PrimaryColor { get; set; }

        [JsonProperty("secondarycolor")]
        public string SecondaryColor { get; set; }

        [JsonProperty("tertiarycolor")]
        public string TertiaryColor { get; set; }

        [JsonProperty("companyname")]
        public string CompanyName { get; set; }

        [JsonProperty("companylogo")]
        public string CompanyLogo { get; set; }

        [JsonProperty("sections")]
        public List<Section> Sections { get; set; }
    }

    public class Section
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("apiurl")]
        public string ApiUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("primarytext")]
        public string PrimaryText { get; set; }

        [JsonProperty("secondarytext")]
        public string SecondaryText { get; set; }

        [JsonProperty("tertiarytext")]
        public string TertiaryText { get; set; }

        [JsonProperty("nextpage")]
        public string NextPage { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }
        [JsonProperty("displayorder")]
        public int DisplayOrder { get; set; }
    }

    public class Image
    {
        [JsonProperty("imagepath")]
        public string ImagePath { get; set; }

        [JsonProperty("displayorder")]
        public long DisplayOrder { get; set; }
    }

    public class ItemIdBySection
    {
        public long VariantId { get; set; }
        public long ItemId { get; set; }
    }
    public class Currency
    {
        public int CurrencyMasterId { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }

    }
    public class ResponseSaveTwillioNotificationService
    {
        [Key]
        public int Id { get; set; }
    }
    public class ResponseGetNotificationServiceDetails
    {
        [Key]
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
        public bool IsSSLEnabled { get; set; } = false;

    }
}
