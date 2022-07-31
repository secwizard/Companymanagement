﻿using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface ITemplateService
    {
        Task<List<ResponseAdminCompanyTemplateSectionItem>> MakeVariantWiseVariantDataForSection(List<ResponseAdminCompanyTemplateSectionItem> sectionItems, long companyid);
        Task<FrontEndTemplate> GetTemplate(RequestCompanyTemplate request);
        Task<List<ItemIdBySection>> GetTemplateBySectionID(RequestItemBySectionId request);
        Task<List<ResponseCompanyTemplate>> GetCompanyTemplates(RequestBase request);
        Task<List<ResponseFrontendTemplate>> GetFrontendTemplate();
        Task<ResponseCompanyTemplate> AddCompanyTemplate(RequestAddCompanyTemplate request);
        Task<ResponseCompanyTemplate> EditCompanyTemplate(RequestEditCompanyTemplate request);
        Task<ResponseCompanyTemplate> GetCompnayTemplateById(RequestGetCompanyTemplateById request);
        Task<ResponseCompanyTemplate> GetDefaultTemplateByCompany(RequestCompanyTemplate request);
        Task<ResponseCompanyTemplate> GetDefaultTemplateByCompanyV2(RequestCompanyTemplate request);
        Task<ResponseCompanyTemplateSection> EditCompanyTemplateSection(RequestEditCompanyTemplateSection request);
        Task<bool> EditCompanyTemplateSectionOrder(RequestEditCompanyTemplateSectionOrder request);
        Task<ResponseAdminSectionItemAndImage> AddSectionItem(RequestAddSectionItem request);
        Task<bool> EditSectionItemOrder(RequestEditSectionItemOrder request);
        Task<bool> EditSectionImageOrder(RequestEditSectionImageOrder request);
        Task<bool> ChangeCompanyTemplateDefault(RequestChangeCompanyTemplateDefault request);
        Task<bool> ChangeCompanyTemplateB2C(RequestChangeCompanyTemplateB2C request);
        Task<bool> DeleteCompanyTemplateSectionItem(RequestDeleteCompanyTemplateSectionItem request);
        Task<bool> DeleteCompanyTemplateSectionImage(RequestDeleteCompanyTemplateSectionImage request);
        Task<List<ResponseFrontEndTemplateFontFamilyMaster>> GetAllFrontEndTemplateFonts();
        Task<ResponseCompanyTemplateSection> GetTemplateSectionForMetaData();
        Task<ResponseCompanyTemplateSection> SaveUpdateCompanyTemplateSectionData(ResponseCompanyTemplateSection request);
        Task<List<long>> GetSelectedCustomGroup(RequestCompanyTempalteSectionMappingById request);
        Task<ResponseAdminSectionItemAndImage> SaveUpdateCompanyTemplateSectionItemMapping(RequestSectionCustomGroups request);
        Task<ResponseSectionItemAndImage> AddSectionItemVariantList(RequestAddSectionItem request);
        Task<ResponseAdminTemplate> GetCompnayAdminTemplateById(RequestGetCompanyTemplateById request);
        Task<ResponseAdminTemplate> GetCompnayTemplate(RequestGetCompanyTemplateById request);
        Task<ResponseAdminSectionItemAndImage> GetTemplateSectionById(int companyTemplateSectionId);

        Task<ResponseAdminTemplate> GetFrontEndCompanyTemplate(RequestCompanyTemplate request);
    }
}