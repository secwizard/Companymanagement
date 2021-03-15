using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models.Response
{
    public class ResponseGetCompanyDetailsForSentMail
    {
        public string CompImageFilePath { get; set; }
        public string CompLogoName { get; set; }
        public string CompName { get; set; }
        public string CompAdminEmail { get; set; }
        public string CompCustServiceTel { get; set; }
        public string SMTPServer { get; set; }
        public string FromEmailDisplayName { get; set; }
        public string FromEmailId { get; set; }
        public string FromEmailPwd { get; set; }
        public string CompTermsConditionOrd { get; set; }
        public string CompanyTermsConditionPayment { get; set; }
        public bool? Ssl { get; set; }
        public int? Port { get; set; }
        public string Template { get; set; }
    }
}
