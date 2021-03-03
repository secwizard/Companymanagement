using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Response
{
    public class RequestCompanyInfo
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
    }
    public class LookUpInfo
    {
        public string LookUpValue { get; set; }
        public string LookUpText { get; set; }


    }
}
