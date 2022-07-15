using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models.Response
{
    public class ResponseAdminTemplate
    {
        public int CompanyTemplateId { get; set; }
        public long CompanyId { get; set; }
        public long TemplateId { get; set; }
        public string TemplateName { get; set; }
        public bool IsDefault { get; set; }
        public string Url { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
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
        //public int? FontFamilyId { get; set; }
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
        public ResponseFrontEndTemplateFontFamilyMaster ResponseFontFamily { get; set; }
        public string LargeBrushName { get; set; }
        public string MediumBrushName { get; set; }
        public string SmallBrushName { get; set; }
        public List<ResponseAdminCompanyTemplateSection> ResponseCompanyTemplateSections { get; set; } = new List<ResponseAdminCompanyTemplateSection>();
    }
    public class ResponseAdminCompanyTemplateSection
    {
        public int CompanyId { get; set; }
        public int CompanyTemplateSectionId { get; set; }//pk
        public int CompanyTemplateId { get; set; }
        public int SectionType { get; set; }
        public string SectionTypeName { get; set; }
        public string SectionName { get; set; }
        public string? SectionBackgrounColor { get; set; }
        public bool? IsActive { get; set; }
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public int DisplayOrder { get; set; }
        public int SectionFor { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public ResponseAdminSectionItemAndImage ResponseSectionItemAndImage { get; set; }
        public List<TemplateSectionForMetaData> SectionForList { get; set; }
        public List<TemplateSectionTypemaster> TemplateSectionTypemaster { get; set; }
    }
    public class ResponseAdminSectionItemAndImage
    {
        public List<ResponseAdminCompanyTemplateSectionItem> SectionItems { get; set; }
        public List<ResponseAdminCompanyTemplateSectionImage> SectionImages { get; set; }
        public CompanyTemplateSectionItemMappingData SectionItemDetails { get; set; }
    }

    public class ResponseCompany
    {
        public long CompanyId { get; set; }
        public List<ResponseAdminCompanyTemplateSectionItem> ResponseAdminCompanyTemplateSectionItem { get; set; }
        public DemoAdminCompanyTemplateSectionItem DemoAdminCompanyTemplateSectionItem { get; set; }
    }
        public class ResponseAdminCompanyTemplateSectionItem
    {
        public long CompanyId { get; set; }
        public long CompanyTemplateSectionItemMappingId { get; set; }//pk
        public int ItemId { get; set; }
        public int VariantId { get; set; }
        public bool? IsActive { get; set; }
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal MRP { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal MembrPrice { get; set; }
        public List<ReturnImages> Images { get; set; }
      
    }
    public class DemoAdminCompanyTemplateSectionItem
    {
        public long CompanyId { get; set; }
        public long CompanyTemplateSectionItemMappingId { get; set; }//pk
        public int ItemId { get; set; }
        public int VariantId { get; set; }
        public bool? IsActive { get; set; }
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal MRP { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal MembrPrice { get; set; }
        public List<ReturnImages> Images { get; set; }

    }
    public class ResponseAdminCompanyTemplateSectionImage
    {
        public long CompanyTemplateSectionImageMappingId { get; set; }//pk
        public int CompanyTemplateSectionId { get; set; }
        public string ImagePath { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long ItemId { get; set; }
        public List<ResponseAdminCompanyTemplateSectionItem> VariantListWithinThisItem { get; set; }
    }
}
