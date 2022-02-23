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
        Task<List<ResponseCompanyTemplate>> GetCompanyTemplates(RequestBase request);
        Task<List<ResponseFrontendTemplate>> GetFrontendTemplate();
        Task<ResponseCompanyTemplate> AddCompanyTemplate(RequestAddCompanyTemplate request);
        Task<ResponseCompanyTemplate> EditCompanyTemplate(RequestEditCompanyTemplate request);
        Task<ResponseCompanyTemplate> GetCompnayTemplateById(RequestGetCompanyTemplateById request);
        Task<ResponseCompanyTemplateSection> EditCompanyTemplateSection(RequestEditCompanyTemplateSection request);
        Task<bool> EditCompanyTemplateSectionOrder(RequestEditCompanyTemplateSectionOrder request);
        Task<ResponseSectionItemAndImage> AddSectionItem(RequestAddSectionItem request);
        Task<bool> EditSectionItemOrder(RequestEditSectionItemOrder request);
        Task<bool> EditSectionImageOrder(RequestEditSectionImageOrder request);
        Task<bool> ChangeCompanyTemplateDefault(RequestChangeCompanyTemplateDefault request);
        Task<bool> ChangeCompanyTemplateB2C(RequestChangeCompanyTemplateB2C request);
        Task<bool> DeleteCompanyTemplateSectionItem(RequestDeleteCompanyTemplateSectionItem request);
        Task<bool> DeleteCompanyTemplateSectionImage(RequestDeleteCompanyTemplateSectionImage request);
        Task<List<ResponseFrontEndTemplateFontFamilyMaster>> GetAllFrontEndTemplateFonts();
    }
}