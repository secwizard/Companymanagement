using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Response
{
    public class ResponseUserDtl
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long CompanyId { get; set; }
    }
}
