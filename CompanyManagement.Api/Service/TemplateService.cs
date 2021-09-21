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
    }
}
