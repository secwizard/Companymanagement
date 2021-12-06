using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface ITemplateService
    {
        Task<FrontEndTemplate> GetTemplate(RequestCompanyTemplate request);
        Task<List<ItemIdBySection>> GetTemplateBySectionID(RequestItemBySectionId request);
        Task<List<ResponseCompanyTemplate>> GetCompanyTemplate(RequestBase request);
        Task<List<ResponseFrontendTemplate>> GetFrontendTemplate();
        Task<ResponseCompanyTemplate> AddCompanyTemplate(RequestAddCompanyTemplate request);
        Task<ResponseCompanyTemplate> GetCompnayTemplateById(RequestAddCompanyTemplate request);
        Task<ResponseCompanyTemplateSection> EditCompanyTemplateSection(RequestEditCompanyTemplateSection request);
        Task<bool> EditCompanyTemplateSectionOrder(RequestEditCompanyTemplateSectionOrder request);
    }
}
