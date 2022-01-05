using System;
using System.Collections.Generic;

namespace CompanyManagement.Api.Models.Response
{
    public class ResponseFrontendTemplate
    {
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
    }

    public class ResponseCompanyTemplateSection
    {
        public int CompanyTemplateSectionId { get; set; }//pk
        public int SectionType { get; set; }
        public string SectionName { get; set; }
        public string SectionBackgrounColor { get; set; }
        public bool? IsActive { get; set; }
        public string PrimaryText { get; set; }
        public string SecondaryText { get; set; }
        public string TertiaryText { get; set; }
        public int DisplayOrder { get; set; }
        public ResponseSectionItemAndImage ResponseSectionItemAndImage { get; set; }
    }

    public class ResponseCompanyTemplateSectionItem
    {
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
    }
    public class ResponseCompanyTemplateSectionImage
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
        public List<ResponseCompanyTemplateSectionItem> VariantListWithinThisItem { get; set; }
    }
    public class ResponseSectionItemAndImage
    {
        public List<ResponseCompanyTemplateSectionItem> SectionItems { get; set; }
        public List<ResponseCompanyTemplateSectionImage> SectionImages { get; set; }
    }
}