﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Response
{
    public class OnBoardProcessinfo
    {
        public OnBoardCompany OnBoardCompanyInfo { get; set; }
        public List<OnBoardSubscriptions> OnBoardSubscriptionInfo { get; set; }
        public List<OnBoardAddOns> OnBoardAddOn { get; set; }
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
        public long SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Metric { get; set; }
        public string Inclusion { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class OnBoardAddOns
    {
        public long AddOnId { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Metric { get; set; }
        public string Inclusion { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
    public class OnBoardConfiguration
    {

    }
    public class CompanyAllDetails
    {
        public RequestCompanyInfo CompanyDtl { get; set; }
        public List<OnBoardSubscriptions> SubscriptionDtl { get; set; }
        public List<OnBoardAddOns> AddOnDtl { get; set; }
    }
}
