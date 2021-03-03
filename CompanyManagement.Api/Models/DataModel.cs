using System;
using System.ComponentModel.DataAnnotations;

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

}
