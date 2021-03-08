using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Response
{
    public class OnBoardProcessinfo
    {
        public OnBoardCompany OnBoardCompanyInfo { get; set; }
        public OnBoardSubscriptions OnBoardSubscriptionInfo { get; set; }
        public OnBoardAddOns OnBoardAddOn { get; set; }
        public OnBoardConfiguration OnBoardConfiguration { get; set; }
}
    public class OnBoardCompany
    {
        public RequestCompanyInfo CompanyInfo { get; set; }
        public List<ResponseBranchDetails> BranchInfo { get; set; }
        public ResponseMailServerDetails MailServerInfo { get; set; }
        public List<ResponseCompanySetting> CompanySettingInfo { get; set; }
        public List<ResponseCompanyTemplate> CompanyTemplate { get; set; }
        public List<ResponseThemeDetails> CompanyTheme { get; set; }
    }
    public class OnBoardSubscriptions
    {

    }
    public class OnBoardAddOns
    {

    }
    public class OnBoardConfiguration
    {

    }
}
