using AutoMapper;
using CompanyManagement.Api.Data;
using CompanyManagement.Api.Mapper;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Request;
using CompanyManagement.Api.Models.Response;
using log4net;
using MailKit.Net.Smtp;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IMapper _mapper;

        static TemplateService()
        {
            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            }).CreateMapper();
        }

        public TemplateService(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<FrontEndTemplate> GetTemplate(RequestCompanyTemplate request)
        {
            FrontEndTemplate template = null;
            try
            {
                var parms = new SqlParameter[]
                 {
                    new SqlParameter("@Url", String.IsNullOrEmpty(request.Url)?"": request.Url),
                    new SqlParameter("@CompanyId", request.CompanyId),
                    new SqlParameter("@Type", String.IsNullOrEmpty(request.Type)?"": request.Type)
                 };

                string sqlText = $"EXECUTE dbo.[GetTemplate] @Url, @CompanyId, @Type";
                var dataTemplte = await _context.GetTemplate.FromSqlRaw(sqlText, parms).ToListAsync();
                if (dataTemplte != null && dataTemplte.Count > 0)
                    template = await BindTemplateData (template, dataTemplte.FirstOrDefault());
                return template;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        private async Task<FrontEndTemplate> BindTemplateData(FrontEndTemplate template, GetTemplate dataTemplte)
        {
            template = new FrontEndTemplate();
            template.CompanyLogo = dataTemplte.CompanyLogo;
            template.CompanyName = dataTemplte.Name;
            template.MobileViewName = dataTemplte.MobileViewName;
            template.PrimaryColor = dataTemplte.PrimaryColor;
            template.SecondaryColor = dataTemplte.SecondaryColor;
            template.TemplateId = dataTemplte.TemplateId;
            template.TemplateName = dataTemplte.TemplateName;
            template.TertiaryColor = dataTemplte.TertiaryColor;
            template.WebViewName = dataTemplte.WebViewName;
            template.MobileViewName = dataTemplte.MobileViewName;
            template = await GetTemplateSection(template, dataTemplte.TemplateId);
            return template;
        }
        private async Task<FrontEndTemplate> GetTemplateSection(FrontEndTemplate template, int templateId)
        {
            var sections = await _context.CompanyTemplateSection.Where(x => x.CompanyTemplateId == templateId && x.IsActive == true).ToListAsync();
            if (sections != null && sections.Count > 0)
            {
                template = await  BindSections(template, sections);
            }
            return template;
        }
        private async Task<FrontEndTemplate> BindSections(FrontEndTemplate template, List<CompanyTemplateSection> sections)
        {
            List<Section> listsection = new List<Section>();
            foreach (var item in sections)
            {
                Section section = new Section();
                section.Id = item.CompanyTemplateSectionId;
                section.Name = item.SectionName;
                section.PrimaryText = item.PrimaryText;
                section.SecondaryText = item.SecondaryText;
                section.TertiaryText = item.TertiaryText;
                section.Type = item.SectionType.ToString();
                section.DisplayOrder = item.DisplayOrder;
                section = await BindSectionImages(section, item.CompanyTemplateSectionId);
                listsection.Add(section);
            }
            template.Sections = new List<Section>();
            template.Sections = listsection;
            return template;
        }
        private async Task<Section> BindSectionImages(Section section, int companyTemplateSectionId)
        {
            List<Image> listImage = new List<Image>();
            var images = await _context.CompanyTemplateSectionImageMapping.Where(x => x.CompanyTemplateSectionId == companyTemplateSectionId && x.IsActive == true).ToListAsync();
            if (images != null && images.Count > 0)
            {
                foreach (var item in images)
                {
                    Image image = new Image();
                    image.DisplayOrder = item.DisplayOrder;
                    image.ImagePath = item.ImagePath;
                    listImage.Add(image);
                }
                section.Images = new List<Image>();
                section.Images = listImage;
            }
            return section;
        }



        public async Task<List<ItemIdBySection>> GetTemplateBySectionID(RequestItemBySectionId request)
        {
            try
            {
                List<ItemIdBySection> listData = new List<ItemIdBySection>();
                var parms = new SqlParameter[]
                 {

                    new SqlParameter("@SectionId", Convert.ToInt64(request.SectionId))

                 };

                string sqlText = $"EXECUTE dbo.[GetTemplateBySectionId] @SectionId";
                var data = await _context.GetTemplateBySectionId.FromSqlRaw(sqlText, parms).ToListAsync();
                BindItemData(listData, data);
                return listData;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        private void BindItemData(List<ItemIdBySection> listData, List<GetTemplateBySectionId> data)
        {
            if(data != null && data.Count > 0)
            {
                foreach(var item in data)
                {
                    ItemIdBySection id = new ItemIdBySection();
                    id.VariantId = item.VariantId;
                    id.ItemId = item.ItemId;
                    listData.Add(id);
                }
            }
        }

        public async Task<List<ResponseCompanyTemplate>> GetCompanyTemplate(RequestBase request)
        {
            try
            {
                var dataTemplte = await _context.CompanyTemplate
                    .Where(t => t.CompanyId == request.CompanyId)
                    .ToListAsync();
                var returnDataTemplte = _mapper.Map<List<ResponseCompanyTemplate>>(dataTemplte);
                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<List<ResponseFrontendTemplate>> GetFrontendTemplate()
        {
            try
            {
                var dataTemplte = await _context.FronEndTemplate
                    .ToListAsync();
                var returnDataTemplte = _mapper.Map<List<ResponseFrontendTemplate>>(dataTemplte);
                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseCompanyTemplate> AddCompanyTemplate(RequestAddCompanyTemplate request)
        {
            try
            {
                var dataTemplte = await _context.FronEndTemplate
                    .Include(t => t.TemplateDefaultSections)
                    .Where(t => t.TemplateId == request.TemplateId)
                    .FirstOrDefaultAsync();
                var companyTemplate = await CreateMapForComapnyTemplate(request, dataTemplte);
                var returnDataTemplte = _mapper.Map<ResponseCompanyTemplate>(companyTemplate);
                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        private async Task<CompanyTemplate> CreateMapForComapnyTemplate(RequestAddCompanyTemplate request, FronEndTemplate dataTemplte)
        {
            var companyDataTemp = _mapper.Map<FronEndTemplate, CompanyTemplate>(dataTemplte);
            companyDataTemp.CompanyId = request.CompanyId;
            companyDataTemp.Url = "";
            companyDataTemp.Type = "1";
            companyDataTemp.IsForB2C = request.IsForB2C;
            companyDataTemp.CreatedBy = request.UserId.ToString();
            companyDataTemp.CreatedAt = DateTime.UtcNow;
            companyDataTemp.UpdatedBy = request.UserId.ToString();
            companyDataTemp.UpdatedAt = DateTime.UtcNow;
            var companyTempSectionsLst = companyDataTemp.CompanyTemplateSections;
            if (companyTempSectionsLst.Count > 0)
            {
                int i = 1;
                foreach (var companyTempSection in companyTempSectionsLst)
                {
                    companyTempSection.CreatedBy = request.UserId.ToString();
                    companyTempSection.CreatedAt = DateTime.UtcNow;
                    companyTempSection.UpdatedBy = request.UserId.ToString();
                    companyTempSection.UpdatedAt = DateTime.UtcNow;
                    companyTempSection.DisplayOrder = i;
                    i++;
                }
            }
            await _context.CompanyTemplate.AddAsync(companyDataTemp);
            await _context.SaveChangesAsync();
            return companyDataTemp;
        }

        public async Task<ResponseCompanyTemplate> GetCompnayTemplateById(RequestAddCompanyTemplate request)
        {
            try
            {
                var dataTemplte = await _context.CompanyTemplate
                    .Include(t => t.CompanyTemplateSections)
                    .Where(t => t.TemplateId == request.TemplateId && t.CompanyId == request.CompanyId)
                    .FirstOrDefaultAsync();
                var returnDataTemplte = _mapper.Map<ResponseCompanyTemplate>(dataTemplte);
                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<ResponseCompanyTemplateSection> EditCompanyTemplateSection(RequestEditCompanyTemplateSection request)
        {
            try
            {
                var companyTemplateSection = await GetCompnayTemplateSectionById(request.CompanyTemplateSectionId);
                companyTemplateSection.PrimaryText = request.PrimaryText;
                companyTemplateSection.SecondaryText = request.SecondaryText;
                companyTemplateSection.TertiaryText = request.TertiaryText;
                companyTemplateSection.UpdatedBy = request.UserId.ToString();
                companyTemplateSection.UpdatedAt = DateTime.UtcNow;

                //_context.CompanyTemplateSection.Attach(companyTemplateSection).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var returnDataTemplte = _mapper.Map<ResponseCompanyTemplateSection>(companyTemplateSection);
                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        private async Task<CompanyTemplateSection> GetCompnayTemplateSectionById(int companyTemplateSectionId)
        {
            try
            {
                var dataTemplte = await _context.CompanyTemplateSection
                    .Where(t => t.CompanyTemplateSectionId == companyTemplateSectionId)
                    .FirstOrDefaultAsync();
                return dataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<bool> EditCompanyTemplateSectionOrder(RequestEditCompanyTemplateSectionOrder request)
        {
            try
            {
                var resultList = new List<int>();
                var Ids = Array.ConvertAll(request.CompanyTemplateSectionIds, s => int.Parse(s));
                var commandText = "UPDATE [dbo].[CompanyTemplateSection] SET DisplayOrder = @DisplayOrder WHERE [CompanyTemplateSectionId] = @Id";
                foreach (var id in Ids)
                {
                    var parms = new SqlParameter[]
                    {
                        new SqlParameter("@DisplayOrder", (Array.FindIndex(Ids, i => i == id) + 1)),
                        new SqlParameter("@Id", id)
                    };
                    var result = await _context.Database.ExecuteSqlRawAsync(commandText, parms);
                    resultList.Add(result);
                }
                if (resultList.Any(i => i < 0)) return false;
                return true;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
    }
}
