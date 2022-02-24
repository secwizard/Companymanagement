using CompanyManagement.Api.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models.Response
{
    public class ResponseCompanyTemplate
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
        public int? FontFamilyId { get; set; }
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
        public List<ResponseCompanyTemplateSection> ResponseCompanyTemplateSections { get; set; } = new List<ResponseCompanyTemplateSection>();
    }    
}