﻿using CompanyManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface ICompanyService
    {
        Task<CompanyInfo> GetCompany(RequestBase request);
        Task<List<CompanyInfo>> GetCompanyList();
        Task<CompanyMailServer> GetCompanySmtp(RequestBase request);
        Task<CompanyTheme> GetCompanyTheme(RequestBase request);
        Task<List<BranchInfo>> GetCompanyBranch(RequestBase request);
        Task<List<CompanySettingInfo>> GetCompanySetting(RequestCompanySetting request);
        Task<Response<CompanyInfo>> AddEditCompany(CompanyInfo request);
    }
}
