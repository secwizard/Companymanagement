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
                    BindTemplateData(template, dataTemplte.FirstOrDefault());
                return template;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        private void BindTemplateData(FrontEndTemplate template, GetTemplate dataTemplte)
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
            GetTemplateSection(template, dataTemplte.TemplateId);
        }
        private async void GetTemplateSection(FrontEndTemplate template, int templateId)
        {
            var sections = await _context.CompanyTemplateSection.Where(x => x.CompanyTemplateId == templateId && x.IsActive == true).ToListAsync();
            if (sections != null && sections.Count > 0)
            {
                BindSections(template, sections);
            }
        }
        private void BindSections(FrontEndTemplate template, List<CompanyTemplateSection> sections)
        {
            List<Section> listsection = new List<Section>();
            foreach (var item in sections)
            {
                Section section = new Section();
                section.Name = item.SectionName;
                section.PrimaryText = item.PrimaryText;
                section.SecondaryText = item.SecondaryText;
                section.TertiaryText = item.TertiaryText;
                section.Type = item.SectionType.ToString();
                BindSectionImages(section, item.CompanyTemplateSectionId);
                listsection.Add(section);
            }
            template.Sections = new List<Section>();
        }
        private async void BindSectionImages(Section section, int companyTemplateSectionId)
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
        }
    }
}
