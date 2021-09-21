using CompanyManagement.Api.Models;
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
    }
}
