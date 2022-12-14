using CompanyManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CompanyManagement.Api.Service
{
    public interface IOnBoardService
    {
        Task<CompanyAllDetails> GetCompanyDetails(NewCompanyDetails request);
        Task<List<LookUpInfo>> GetRequiredDetails(RequestBase request);
        Task<Response<CompanyInfo>> AddCompany(CompanyInfo request);
        Task<ResponseCompanyId> GetSuggestedCompanyId(string type);
        Task<ResponseCompanyId> SaveOnBoardProcess(OnBoardProcessinfo process, UserInfo user);
    }
}
