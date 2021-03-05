using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Request
{
    public class RequestCompanyDtl
    {
        public Int64 CompanyId { get; set; }
    }
    public class NewCompanyDetails
    {
        public Int64 CompanyId { get; set; }
        public Int64 NewCompanyId { get; set; }
        public Guid UserId { get; set; }


    }
}
