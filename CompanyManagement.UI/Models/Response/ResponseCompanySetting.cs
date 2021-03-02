using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Response
{
    public class ResponseCompanySetting
    {
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
}
