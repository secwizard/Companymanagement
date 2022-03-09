using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagement.Api.Models
{
    public class Company
    {
        [Key]
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
        public string CompanySiteUrl { get; set; }
        [Column("CurrencyId")]
        public int CurrencyMasterId { get; set; }
        public CurrencyMaster CurrencyMaster { get; set; }
        public string AdminPhoneCode { get; set; }
        public string ServicePhoneCode { get; set; }
        public string AdminPhoneCountryCode { get; set; }
        public string ServicePhoneCountryCode { get; set; }
    }

    public class Branch
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

    public class CompanySetting
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
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsAllProductInclusiveOfTax { get; set; }
    }

    public class MailServer
    {
        [Key]
        public long MailServerId { get; set; }
        public long CompanyId { get; set; }
        public string SMTPServer { get; set; }
        public int? SMTPPort { get; set; }
        public bool? EnableSSL { get; set; }
        public string FromEmailDisplayName { get; set; }
        public string FromEmailId { get; set; }
        public string FromEmailPwd { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }

    public class Template
    {
        [Key]
        public long TemplateId { get; set; }
        public long CompanyId { get; set; }
        public string TemplateType { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string HTMLData { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }

    public class Theme
    {
        [Key]
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
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class GetCompanyTemplate
    {
        [Key]
        public long TemplateId { get; set; }
        public long CompanyId { get; set; }
        public string TemplateType { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string HTMLData { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class GetLookUpType
    {
        [Key]
        public long LookUpId { get; set; }
        public string LookUpType { get; set; }
        public string LookUpValue { get; set; }
        public string LookUpDescription { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class GetCompanyTheme
    {
        [Key]
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
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class GetSuggestedCompanyId
    {
        [Key]
        public Int64 CompanyId { get; set; }
    }
    public class AddOnMaster
    {
        [Key]
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

    public class SubscriptionMaster
    {
        [Key]
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


    public class AddOns
    {
        [Key]
        public long CompanyAddOnId { get; set; }
        public long CompanyId { get; set; }
        public long AddOnId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class Subscriptions
    {
        [Key]
        public long CompanySubscriptionId { get; set; }
        public long CompanyId { get; set; }
        public long SubscriptionId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class FronEndTemplate
    {
        [Key]
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateView { get; set; }
        public string ViewName { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public bool IsActive { get; set; }
        public string MobileViewName { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; }
        public bool? OnlyForMobile { get; set; }
        public bool? IsEditable { get; set; }
        public string TopBarBackgroundColor { get; set; }
        public string TopLogoUrl { get; set; }
        public string TopCartIconUrl { get; set; }
        public string TopcartIconBackgroundColor { get; set; }
        public string TopProfileIconUrl { get; set; }
        public string TopProfileIconBackgroundColor { get; set; }
        public string TopMenuIconUrl { get; set; }
        public string PageBackgroundColor { get; set; }
        public string FontBackgroundBrushColor { get; set; }
        public string GeneralFontColor { get; set; }
        public string SeeAllArrowIconUrl { get; set; }
        public string ShopNowFontColor { get; set; }
        public string ShopNowBackgroundColor { get; set; }
        public decimal? ShopNowBorderRadius { get; set; }
        public string ShopNowBorderColor { get; set; }
        public decimal? SectionBorderRadius { get; set; }
        public decimal? SubSectionBorderRadius { get; set; }
        public bool? IsSubSectionTransparent { get; set; }
        public string SubSectionGradientPrimaryColor { get; set; }
        public string SubSectionGradientSecondaryColor { get; set; }
        public int? FontFamilyId { get; set; }

        [ForeignKey(nameof(FontFamilyId))]
        public FrontEndTemplateFontFamilyMaster FontFamilyMaster { get; set; }

        public string LargeBrushName { get; set; }
        public string MediumBrushName { get; set; }
        public string SmallBrushName { get; set; }
        public List<TemplateDefaultSection> TemplateDefaultSections { get; set; } = new List<TemplateDefaultSection>();
    }
    public class TemplateDefaultSection
    {
        [Key]
        public int TemplateDefaultSectionId { get; set; }
        public int TemplateId { get; set; }
        public int SectionType { get; set; }
        public string SectionName { get; set; }
        public string SectionBackgrounColor { get; set; }
        public FronEndTemplate Template { get; set; }
    }
    public class CompanyTemplate
    {
        [Key]
        public int CompanyTemplateId { get; set; }
        public long CompanyId { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public bool IsDefault { get; set; }
        public string Url { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Type { get; set; }
        public bool IsForB2C { get; set; }
        public string TemplateView { get; set; }
        public string ViewName { get; set; }
        public string MobileViewName { get; set; }
        public string ImagePath { get; set; }
        public bool? OnlyForMobile { get; set; }
        public bool? IsEditable { get; set; }
        public string TopBarBackgroundColor { get; set; }
        public string TopLogoUrl { get; set; }
        public string TopCartIconUrl { get; set; }
        public string TopcartIconBackgroundColor { get; set; }
        public string TopProfileIconUrl { get; set; }
        public string TopProfileIconBackgroundColor { get; set; }
        public string TopMenuIconUrl { get; set; }
        public string PageBackgroundColor { get; set; }
        public string FontBackgroundBrushColor { get; set; }
        public string GeneralFontColor { get; set; }
        public string SeeAllArrowIconUrl { get; set; }
        public string ShopNowFontColor { get; set; }
        public string ShopNowBackgroundColor { get; set; }
        public decimal? ShopNowBorderRadius { get; set; }
        public string ShopNowBorderColor { get; set; }
        public decimal? SectionBorderRadius { get; set; }
        public decimal? SubSectionBorderRadius { get; set; }
        public bool? IsSubSectionTransparent { get; set; }
        public string SubSectionGradientPrimaryColor { get; set; }
        public string SubSectionGradientSecondaryColor { get; set; }
        public int? FontFamilyId { get; set; }
        [ForeignKey(nameof(FontFamilyId))]
        public FrontEndTemplateFontFamilyMaster FontFamilyMaster { get; set; }

        public string LargeBrushName { get; set; }
        public string MediumBrushName { get; set; }
        public string SmallBrushName { get; set; }

        public List<CompanyTemplateSection> CompanyTemplateSections { get; set; } = new List<CompanyTemplateSection>();
    }
    public class CompanyTemplateSection
    {
        [Key]
        public int CompanyTemplateSectionId { get; set; }
        public int CompanyTemplateId { get; set; }
        public int SectionType { get; set; }
        public string SectionName { get; set; }
        public string SectionBackgrounColor { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public int DisplayOrder { get; set; }
        public CompanyTemplate CompanyTemplate { get; set; }
        public List<CompanyTemplateSectionItemMapping> CompanyTemplateSectionItemMappings { get; set; } = new List<CompanyTemplateSectionItemMapping>();
        public List<CompanyTemplateSectionImageMapping> CompanyTemplateSectionImageMappings { get; set; } = new List<CompanyTemplateSectionImageMapping>();

    }
    public class CompanyTemplateSectionImageMapping
    {
        [Key]
        public long CompanyTemplateSectionImageMappingId { get; set; }
        public int CompanyTemplateSectionId { get; set; }
        public string ImagePath { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long ItemId { get; set; }
        public CompanyTemplateSection CompanyTemplateSection { get; set; }

    }
    public class CompanyTemplateSectionItemMapping
    {
        [Key]
        public long CompanyTemplateSectionItemMappingId { get; set; }
        public int CompanyTemplateSectionId { get; set; }
        public long ItemId { get; set; }
        public long VariantId { get; set; }
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CompanyTemplateSection CompanyTemplateSection { get; set; }

    }
    public class GetTemplate
    {
        [Key]
        public int CompanyTemplateId { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string WebViewName { get; set; }
        public string MobileViewName { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public string Name { get; set; }
        public string CompanyLogo { get; set; }
    }

    public class GetTemplateBySectionId
    {
        [Key]
        public long VariantId { get; set; }
        public long ItemId { get; set; }

    }
    public class TaxName
    {
        [Key]
        public int TaxNameId { get; set; }
        public string Tax1Name { get; set; }
        public string Tax2Name { get; set; }
        public string Tax3Name { get; set; }
        public string Tax4Name { get; set; }
        public string Tax5Name { get; set; }
        public int CompanyId { get; set; }
        public DateTime? CreatedOnUTC { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOnUTC { get; set; }
        public Guid? UpdatedBy { get; set; }
    }

    public class TaxDetails
    {
        [Key]
        public int TaxDetailsId { get; set; }
        public string TaxName { get; set; }
        public decimal Tax1Percentage { get; set; }
        public decimal Tax2Percentage { get; set; }
        public decimal Tax3Percentage { get; set; }
        public decimal Tax4Percentage { get; set; }
        public decimal Tax5Percentage { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? CreatedOnUTC { get; set; }
        public DateTime? UpdatedOnUTC { get; set; }
        public int CompanyId { get; set; }
        public bool IsDefault { get; set; }
        public decimal Total { get; set; }
    }
    public class GetTaxDetails
    {
        [Key]
        public int TaxDetailsId { get; set; }
        public string TaxName { get; set; }
        public string Tax1Name { get; set; }
        public decimal Tax1Percentage { get; set; }
        public string Tax2Name { get; set; }
        public decimal Tax2Percentage { get; set; }
        public string Tax3Name { get; set; }
        public decimal Tax3Percentage { get; set; }
        public string Tax4Name { get; set; }
        public decimal Tax4Percentage { get; set; }
        public string Tax5Name { get; set; }
        public decimal Tax5Percentage { get; set; }
        public bool IsDefault { get; set; }
        public decimal Total { get; set; }
    }
    public class CurrencyMaster
    {
        [Key]
        [Column("CurrencyId")]
        public int CurrencyMasterId { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }

    }

    public class FrontEndTemplateFontFamilyMaster
    {
        [Key]
        [Column("Id")]
        public int FontFamilyId { get; set; }
        public string FontName { get; set; }
        public string FontDemoUrl { get; set; }
    }
}
