using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Tax;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface ITaxService
    {
        Task<TaxNameResponse> GetTaxName(RequestBase request);
        Task<TaxDetailsGet> SaveTaxDetails(TaxDetailsSet request);
        Task<List<TaxDetailsGet>> GetTaxDetails(RequestBase request);
        Task<TaxDetailsGet> GetSpecificTaxDetails(SingleTaxDetailsRequest request);
        Task<bool> CreateTaxName(RequestBase requestBase);
        Task<bool> UpdateTaxName(TaxNameRequest taxNameRequest);

        #region Item
        Task<CompanyTaxDetailsResponse> GetAllTax(CompanyTaxDetailsRequest taxNameRequest);
        #endregion
    }
}