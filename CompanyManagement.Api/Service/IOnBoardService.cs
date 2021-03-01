using CompanyManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CompanyManagement.Api.Service
{
    public interface IOnBoardService
    {
        Task<CompanyInfo> GetCompanyDetails(RequestBase request);

        Task<Response<CompanyInfo>> AddCompany(CompanyInfo request);
    }
}
