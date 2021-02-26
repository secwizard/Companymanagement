using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizard.ImageManagement.Models.Response
{
    public class ResponseCompanyDtl
    {
        public string ImageFilePath { get; set; }
        public string shortName { get; set; }
        public string LogoFileName { get; set; }
        public string BusinessType { get; set; }
        public string favIconFileName { get; set; }
    }

    public class CompanyInfo
    {
        public long CompanyId { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
    }
    public class ResponseCompanyList
    {
        public List<CompanyInfo> CompanyList { get; set; }
    }


}
