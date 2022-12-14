using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models.Response
{
    public class ResponseCompanyDtlByIdFrontend
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Pin { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public string AdminEmail { get; set; }
        public string ServiceEmail { get; set; }
        public string CurrencyCode { get; set; }
        public string ImageFilePath { get; set; }
        public string LogoFileName { get; set; }
        public bool? PINRequired { get; set; }
        public string CompCustServiceTel { get; set; }
        public string CompanySiteUrl { get; set; }
        public string About { get; set; }
        public string CompTermsConditionOrd { get; set; }
        public string CompanyTermsConditionPayment { get; set; }
        public ThemeData ThemeData { get; set; }
        public List<FooterData> FooterList { get; set; }
    }

    public class ThemeData
    {
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        public string ImageRatio { get; set; }
        public string Colour { get; set; }
        public string DesktopHeight { get; set; }
        public string MobileHeight { get; set; }

    }
    public class FooterData
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string HTMLData { get; set; }
    }
}
