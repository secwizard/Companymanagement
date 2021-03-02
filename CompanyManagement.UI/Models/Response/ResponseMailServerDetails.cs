using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Response
{
    public class ResponseMailServerDetails
    {
        public long MailServerId { get; set; }
        public long CompanyId { get; set; }
        public string SMTPServer { get; set; }
        public int? SMTPPort { get; set; }
        public bool? EnableSSL { get; set; }
        public string FromEmailDisplayName { get; set; }
        public string FromEmailId { get; set; }
        public string FromEmailPwd { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
