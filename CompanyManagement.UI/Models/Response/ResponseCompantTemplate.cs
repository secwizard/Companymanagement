using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Response
{
    public class ResponseCompanyTemplate
    {
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
    }
}
