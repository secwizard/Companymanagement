using AutoMapper;
using CompanyManagement.Api.Data;
using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Mapper;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Request;
using CompanyManagement.Api.Models.Response;
using log4net;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IMapper _mapper;
        private readonly AppSettings appSettings;
        private readonly IServiceAPI _serviceAPI;


        static TemplateService()
        {
            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            }).CreateMapper();
        }

        public TemplateService(CompanyDBContext context, IOptions<AppSettings> appSettings, IServiceAPI serviceAPI)
        {
            _context = context;
            this.appSettings = appSettings.Value;
            _serviceAPI = serviceAPI;
        }

        public async Task<List<ResponseFrontendTemplate>> GetFrontendTemplate()
        {
            try
            {
                var dataTemplte = await _context.FronEndTemplate
                    .Where(k => k.IsActive)
                    .ToListAsync();

                var returnDataTemplte = _mapper.Map<List<ResponseFrontendTemplate>>(dataTemplte);
                returnDataTemplte.ForEach(k => k.ImagePath = k.ImagePath.StartsWith("http") ? k.ImagePath : appSettings.CommonImagePath + k.ImagePath);
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
                CompanyTemplate companyTemplate = null;
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //Getting front end template//
                        var dataTemplte = await _context.FronEndTemplate
                            .Include(t => t.TemplateDefaultSections)
                            .Where(t => t.TemplateId == request.TemplateId)
                            .FirstOrDefaultAsync();

                        //Saving company template//
                        companyTemplate = await CreateMapForComapnyTemplate(request, dataTemplte);
                        if (companyTemplate != null)
                            await transaction.CommitAsync();
                        else
                            await transaction.RollbackAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        companyTemplate = null;
                    }
                }
                var returnDataTemplte = companyTemplate != null ?
                    _mapper.Map<ResponseCompanyTemplate>(companyTemplate)
                    : null;

                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseCompanyTemplate> EditCompanyTemplate(RequestEditCompanyTemplate request)
        {
            try
            {
                var companyTemplate = new CompanyTemplate
                {
                    CompanyTemplateId = request.CompanyTemplateId,
                    TemplateName = request.TemplateName,
                    PrimaryColor = request.PrimaryColor,
                    SecondaryColor = request.SecondaryColor,
                    TertiaryColor = request.TertiaryColor,
                    IsActive = request.IsActive,
                    IsForB2C = request.IsForB2C,
                    FontFamilyId = request.FontFamilyId,
                    UpdatedBy = request.UserId.ToString(),
                    UpdatedAt = DateTime.UtcNow
                };
                _context.CompanyTemplate.Attach(companyTemplate);
                _context.Entry(companyTemplate).Property(x => x.TemplateName).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.PrimaryColor).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.SecondaryColor).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.TertiaryColor).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.IsForB2C).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.IsActive).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.FontFamilyId).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.UpdatedBy).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.UpdatedAt).IsModified = true;
                await _context.SaveChangesAsync();

                return new ResponseCompanyTemplate
                {
                    CompanyId = request.CompanyId,
                    CompanyTemplateId = request.CompanyTemplateId
                };
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseCompanyTemplate> GetCompnayTemplateById(RequestGetCompanyTemplateById request)
        {
            try
            {
                log.Info("***GetCompnayTemplateById api Method*** Call at Date : " + DateTime.UtcNow);

                var dataTemplte = await _context.CompanyTemplate
                        .Include(ct => ct.CompanyTemplateSections.OrderBy(cts => cts.DisplayOrder).Where(cts => cts.IsActive))
                            .ThenInclude(cts => cts.CompanyTemplateSectionItemMappings.OrderBy(ctsItem => ctsItem.DisplayOrder).Where(ctsItem => ctsItem.IsActive))
                        .Include(ct => ct.CompanyTemplateSections.OrderBy(cts => cts.DisplayOrder).Where(cts => cts.IsActive))
                            .ThenInclude(cts => cts.CompanyTemplateSectionImageMappings.OrderBy(ctsImg => ctsImg.DisplayOrder).Where(ctsImg => ctsImg.IsActive))
                        .Where(ct => ct.CompanyTemplateId == request.CompanyTemplateId)
                        .FirstOrDefaultAsync();

                var fontMaster = await _context.FrontEndTemplateFontFamilyMaster.FirstOrDefaultAsync(f => f.FontFamilyId == dataTemplte.FontFamilyId);
                dataTemplte.FontFamilyMaster = fontMaster;

                var companyImagePath = (await _context.Company.FirstOrDefaultAsync(k => k.CompanyId == request.CompanyId)).ImageFilePath;

                var returnDataTemplte = _mapper.Map<ResponseCompanyTemplate>(dataTemplte);
                returnDataTemplte.TopLogoUrl = companyImagePath + returnDataTemplte.TopLogoUrl;
                returnDataTemplte.ImagePath = returnDataTemplte.ImagePath.Contains("http") ? returnDataTemplte.ImagePath : appSettings.CommonImagePath + returnDataTemplte.ImagePath;
                returnDataTemplte.TopCartIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopCartIconUrl;
                returnDataTemplte.TopProfileIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopProfileIconUrl;
                returnDataTemplte.TopMenuIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopMenuIconUrl;
                returnDataTemplte.SeeAllArrowIconUrl = appSettings.CommonImagePath + returnDataTemplte.SeeAllArrowIconUrl;
                returnDataTemplte.LargeBrushName = appSettings.CommonImagePath + returnDataTemplte.LargeBrushName;
                returnDataTemplte.MediumBrushName = appSettings.CommonImagePath + returnDataTemplte.MediumBrushName;
                returnDataTemplte.SmallBrushName = appSettings.CommonImagePath + returnDataTemplte.SmallBrushName;

                string sqlTextdata = $"EXECUTE dbo.SP_GetTemplateSectionForMetaDataById";
                var dataListValue = await _context.TemplateSectionForMetaData.FromSqlRaw(sqlTextdata).ToListAsync();

                foreach (var section in returnDataTemplte.ResponseCompanyTemplateSections)
                {
                    MakeItemWiseVariantDataForSection(section.ResponseSectionItemAndImage.SectionImages,
                        section.ResponseSectionItemAndImage.SectionItems);
                    section.SectionForList = dataListValue;
                }

                log.Info("***GetCompnayTemplateById api Method*** Call end Date : " + DateTime.UtcNow);

                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<ResponseCompanyTemplate> GetCompnayTemplateByIdV2(RequestGetCompanyTemplateById request)
        {
            try
            {
                //request.CompanyTemplateId = 124;
                var dataTemplte = await _context.CompanyTemplate
                        .Include(ct => ct.CompanyTemplateSections.OrderBy(cts => cts.DisplayOrder).Where(cts => cts.IsActive))
                            .ThenInclude(cts => cts.CompanyTemplateSectionItemMappings.OrderBy(ctsItem => ctsItem.DisplayOrder).Where(ctsItem => ctsItem.IsActive))
                        .Include(ct => ct.CompanyTemplateSections.OrderBy(cts => cts.DisplayOrder).Where(cts => cts.IsActive))
                        //.ThenInclude(cts => cts.CompanyTemplateSectionImageMappings.OrderBy(ctsImg => ctsImg.DisplayOrder).Where(ctsImg => ctsImg.IsActive))
                        .Where(ct => ct.CompanyTemplateId == request.CompanyTemplateId)
                        .FirstOrDefaultAsync();

                var fontMaster = await _context.FrontEndTemplateFontFamilyMaster.FirstOrDefaultAsync(f => f.FontFamilyId == dataTemplte.FontFamilyId);
                dataTemplte.FontFamilyMaster = fontMaster;

                var companyImagePath = (await _context.Company.FirstOrDefaultAsync(k => k.CompanyId == request.CompanyId)).ImageFilePath;

                var returnDataTemplte = _mapper.Map<ResponseCompanyTemplate>(dataTemplte);
                returnDataTemplte.TopLogoUrl = companyImagePath + returnDataTemplte.TopLogoUrl;
                returnDataTemplte.ImagePath = returnDataTemplte.ImagePath.Contains("http") ? returnDataTemplte.ImagePath : appSettings.CommonImagePath + returnDataTemplte.ImagePath;
                returnDataTemplte.TopCartIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopCartIconUrl;
                returnDataTemplte.TopProfileIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopProfileIconUrl;
                returnDataTemplte.TopMenuIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopMenuIconUrl;
                returnDataTemplte.SeeAllArrowIconUrl = appSettings.CommonImagePath + returnDataTemplte.SeeAllArrowIconUrl;
                returnDataTemplte.LargeBrushName = appSettings.CommonImagePath + returnDataTemplte.LargeBrushName;
                returnDataTemplte.MediumBrushName = appSettings.CommonImagePath + returnDataTemplte.MediumBrushName;
                returnDataTemplte.SmallBrushName = appSettings.CommonImagePath + returnDataTemplte.SmallBrushName;

                return returnDataTemplte;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }


        //proc for company template

        public async Task<ResponseAdminTemplate> GetCompnayAdminTemplateById(RequestGetCompanyTemplateById request)
        {
            try
            {
                var companyTemplate = await GetCompanyTemplate(request.CompanyId, request.CompanyTemplateId);
                var companyTemplateSections = companyTemplate != null ? await GetCompanyTemplateSections(request.CompanyId, request.CompanyTemplateId) : null;
                if (companyTemplateSections != null && companyTemplateSections.Count > 0)
                {
                    await GetCompanyTemplateSectionImages(companyTemplateSections);
                    await GetCompanyTemplateSectionItems(companyTemplateSections);
                   
                    var fontMaster = companyTemplateSections != null ? await _context.FrontEndTemplateFontFamilyMaster.FirstOrDefaultAsync(f => f.FontFamilyId == companyTemplate.FontFamilyId) : null;
                    companyTemplate.FontFamilyMaster = fontMaster;

                    if (companyTemplate.IsEditable == false)
                    {
                        companyTemplate.IsEditable = true;

                    }
                }
                return await CreateTemplateReturnObject(request.CompanyId, companyTemplate);
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        private async Task<CompanyTemplateAdmin> GetCompanyTemplate(long companyId, int companyTemplateId)
        {
            try
            {
                var companyTemplates = await _context.CompanyTemplateAdmin
                    .FromSqlRaw($"dbo.GetCompanyTemplateById @CompanyId, @CompanyTemplateId"
                    , new SqlParameter[]
                     {
                       new SqlParameter("@CompanyId", companyId),
                       new SqlParameter("@CompanyTemplateId", companyTemplateId)
                     })
                    .ToListAsync();

                return companyTemplates != null && companyTemplates.Count > 0 ? companyTemplates.FirstOrDefault() : null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return null;
        }

        private async Task<List<CompanyTemplateSectionAdmin>> GetCompanyTemplateSections(long companyId, int companyTemplateId)
        {
            try
            {
                return await _context.CompanyTemplateSectionAdmin
                    .FromSqlRaw($"dbo.GetCompanyTemplateSectionByTemplateId @CompanyId, @CompanyTemplateId"
                    , new SqlParameter[]
                     {
                       new SqlParameter("@CompanyId", companyId),
                       new SqlParameter("@CompanyTemplateId", companyTemplateId)
                     })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return null;
        }

        //private async Task<List<CompanyTemplateSectionAdmin>> BindCompanyTemplateSectionImages(List<CompanyTemplateSectionAdmin> companyTemplateSections)
        //{
        //    var companyTemplateSectionImageMappings = await GetCompanyTemplateSectionImages(companyTemplateSections);
        //    foreach (var item in companyTemplateSections)
        //    {
        //        item.CompanyTemplateSectionImageMappings.AddRange(companyTemplateSectionImageMappings.Where(x => x.CompanyTemplateSectionId == item.CompanyTemplateSectionId).ToList()); ;
        //    }
        //    return companyTemplateSections;
        //}

        //private async Task<List<CompanyTemplateSectionAdmin>> BindCompanyTemplateSectionItems(List<CompanyTemplateSectionAdmin> companyTemplateSections)
        //{
        //    var companyTemplateSectionImageMappings = await GetCompanyTemplateSectionItems(companyTemplateSections);
        //    foreach (var item in companyTemplateSections)
        //    {
        //        item.CompanyTemplateSectionItemMappings.AddRange(companyTemplateSectionImageMappings.Where(x => x.CompanyTemplateSectionId == item.CompanyTemplateSectionId).ToList()); ;
        //    }
        //    return companyTemplateSections;
        //}

        private async Task<List<CompanyTemplateSectionImageMappingAdmin>> GetCompanyTemplateSectionImages(List<CompanyTemplateSectionAdmin> sections)
        {
            try
            {
                return await _context.CompanyTemplateSectionImageMappingAdmin
                    .FromSqlRaw($"dbo.GetImageMappingFromSectionIds @CompanyTemplateSectionIds"
                    , new SqlParameter[]
                     {
                       new SqlParameter("@CompanyTemplateSectionIds", String.Join(',',sections.Select(x=>x.CompanyTemplateSectionId).ToList()))
                     })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return null;
        }

        private async Task<List<CompanyTemplateSectionItemMappingAdmin>> GetCompanyTemplateSectionItems(List<CompanyTemplateSectionAdmin> sections)
        {
            try
            {
                return await _context.CompanyTemplateSectionItemMappingAdmin
                    .FromSqlRaw($"dbo.GetItemMappingFromSectionIds @CompanyTemplateSectionIds"
                    , new SqlParameter[]
                     {
                       new SqlParameter("@CompanyTemplateSectionIds", String.Join(',',sections.Select(x=>x.CompanyTemplateSectionId).ToList()))
                     })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return null;
        }

        private async Task<List<TemplateSectionForMetaData>> GetCustomGroups(long companyId)
        {
            string sqlTextdata = $"EXECUTE dbo.SP_GetTemplateSectionForMetaDataById";
            var dataListValue = await _context.TemplateSectionForMetaData.FromSqlRaw(sqlTextdata).ToListAsync();
            return dataListValue;
        }

     



        private async Task<ResponseAdminTemplate> CreateTemplateReturnObject(long companyId, CompanyTemplateAdmin dataTemplte)
        {
            var companyImagePath = (await _context.Company.FirstOrDefaultAsync(k => k.CompanyId == companyId)).ImageFilePath;
            var returnDataTemplte = _mapper.Map<ResponseAdminTemplate>(dataTemplte);
            var companyid = returnDataTemplte.CompanyId;
            returnDataTemplte.TopLogoUrl = companyImagePath + returnDataTemplte.TopLogoUrl;
            returnDataTemplte.ImagePath = returnDataTemplte.ImagePath.Contains("http") ? returnDataTemplte.ImagePath : appSettings.CommonImagePath + returnDataTemplte.ImagePath;
            returnDataTemplte.TopCartIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopCartIconUrl;
            returnDataTemplte.TopProfileIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopProfileIconUrl;
            returnDataTemplte.TopMenuIconUrl = appSettings.CommonImagePath + returnDataTemplte.TopMenuIconUrl;
            returnDataTemplte.SeeAllArrowIconUrl = appSettings.CommonImagePath + returnDataTemplte.SeeAllArrowIconUrl;
            returnDataTemplte.LargeBrushName = appSettings.CommonImagePath + returnDataTemplte.LargeBrushName;
            returnDataTemplte.MediumBrushName = appSettings.CommonImagePath + returnDataTemplte.MediumBrushName;
            returnDataTemplte.SmallBrushName = appSettings.CommonImagePath + returnDataTemplte.SmallBrushName;
          
            var customGroups = await GetCustomGroups(companyId);

  

            foreach (var section in returnDataTemplte.ResponseCompanyTemplateSections)
            {
                MakeItemWiseVariantDataForSectionAdmin(section.ResponseSectionItemAndImage.SectionImages, section.ResponseSectionItemAndImage.SectionItems);
                var variant = MakeVariantWiseVariantDataForSection(section.ResponseSectionItemAndImage.SectionItems, companyid);
        
                section.SectionForList = customGroups;
                section.ResponseSectionItemAndImage.SectionItems = await variant;
            }
            return returnDataTemplte;
        }

      

        public async Task<List<ResponseAdminCompanyTemplateSectionItem>> MakeVariantWiseVariantDataForSection(List<ResponseAdminCompanyTemplateSectionItem> sectionItems, long cid)
        {

            try
            {



                foreach (var item in sectionItems)
                {
                    item.CompanyId = cid;
                }

                var content = JsonConvert.SerializeObject(sectionItems);
                var result = await _serviceAPI.ProcessPostRequest($"{appSettings.ProductManagementAPI}Product/GetItemVariantsByItemandVariantIds", content);
                var data = JsonConvert.DeserializeObject<ResponseList<ResponseAdminCompanyTemplateSectionItem>>(result);
                if (data != null && data.Status)
                {
                    return data.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Info("***MakeVariantWiseVariantDataForSection*** Date : " + DateTime.UtcNow + " Error : " + ex.Message + "StackTrace : " + ex.StackTrace.ToString());
                throw;
            }
        }






        private void MakeItemWiseVariantDataForSectionAdmin(List<ResponseAdminCompanyTemplateSectionImage> itemImages, List<ResponseAdminCompanyTemplateSectionItem> itemVariants)
        {
            foreach (var item in itemImages)
            {
                item.VariantListWithinThisItem = itemVariants.Where(k => k.ItemId == item.ItemId).OrderBy(k => k.DisplayOrder).ToList();
            }
        }

        //end For Company Template 


        public async Task<ResponseCompanyTemplate> GetDefaultTemplateByCompany(RequestCompanyTemplate request)
        {
            try
            {
                var templateId = (await _context.CompanyTemplate.FirstOrDefaultAsync(k =>
                  k.CompanyId == request.CompanyId
                  && k.IsActive == true
                  && k.Type == request.Type
                  && (string.IsNullOrWhiteSpace(request.Url) ? k.IsDefault == true : k.Url == request.Url)))
                  .CompanyTemplateId;

                return
                    await GetCompnayTemplateById(new RequestGetCompanyTemplateById
                    {
                        CompanyId = request.CompanyId,
                        CompanyTemplateId = templateId
                    });
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<ResponseCompanyTemplate> GetDefaultTemplateByCompanyV2(RequestCompanyTemplate request)
        {
            log.Info("***GetDefaultTemplateByCompanyV2 Method*** Call at Date : " + DateTime.UtcNow);

            try
            {
                var templateId = (await _context.CompanyTemplate.FirstOrDefaultAsync(k =>
                  k.CompanyId == request.CompanyId
                  && k.IsActive == true
                  && k.Type == request.Type
                  && (string.IsNullOrWhiteSpace(request.Url) ? k.IsDefault == true : k.Url == request.Url)))
                  .CompanyTemplateId;

                log.Info("***GetDefaultTemplateByCompanyV2 Method*** Call end Date : " + DateTime.UtcNow);
                return
                    await GetCompnayTemplateByIdV2(new RequestGetCompanyTemplateById
                    {
                        CompanyId = request.CompanyId,
                        CompanyTemplateId = templateId
                    });
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }



        public async Task<List<ResponseCompanyTemplate>> GetCompanyTemplates(RequestBase request)
        {
            try
            {
                var dataTemplte = await _context.CompanyTemplate
                    .Where(t => t.CompanyId == request.CompanyId)
                    .ToListAsync();
                foreach (var item in dataTemplte)
                {

                    if (item.IsEditable == false)
                    {
                        item.IsEditable = true;
                    }
                }
                var returnDataTemplte = _mapper.Map<List<ResponseCompanyTemplate>>(dataTemplte);

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
                var section = new CompanyTemplateSection
                {
                    CompanyTemplateSectionId = request.CompanyTemplateSectionId,
                    SectionName = request.SectionName,
                    SectionBackgrounColor = request.SectionBackgroundColor,
                    PrimaryText = request.PrimaryText,
                    SecondaryText = request.SecondaryText,
                    TertiaryText = request.TertiaryText,
                    IsActive = request.IsActive,
                    UpdatedBy = request.UserId.ToString(),
                    UpdatedAt = DateTime.UtcNow
                };
                _context.CompanyTemplateSection.Attach(section);
                _context.Entry(section).Property(x => x.SectionName).IsModified = true;
                _context.Entry(section).Property(x => x.SectionBackgrounColor).IsModified = true;
                _context.Entry(section).Property(x => x.PrimaryText).IsModified = true;
                _context.Entry(section).Property(x => x.SecondaryText).IsModified = true;
                _context.Entry(section).Property(x => x.TertiaryText).IsModified = true;
                _context.Entry(section).Property(x => x.IsActive).IsModified = true;
                _context.Entry(section).Property(x => x.UpdatedBy).IsModified = true;
                _context.Entry(section).Property(x => x.UpdatedAt).IsModified = true;
                await _context.SaveChangesAsync();

                var returnDataTemplte = _mapper.Map<ResponseCompanyTemplateSection>(section);
                return returnDataTemplte;
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
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        StringBuilder query = new StringBuilder();
                        for (int i = 1; i <= request.CompanyTemplateSectionIds.Length; i++)
                        {
                            query.Append($"update CompanyTemplateSection set DisplayOrder={i},UpdatedBy=@UpdatedBy,UpdatedAt=@updatedAt " +
                                $"where CompanyTemplateSectionId={request.CompanyTemplateSectionIds[i - 1]};");
                        }

                        var ret = await _context.Database.ExecuteSqlRawAsync(query.ToString(), new SqlParameter[]
                            {
                                new SqlParameter("@UpdatedBy",request.UserId.ToString()),
                                new SqlParameter("@updatedAt",DateTime.UtcNow)
                            });
                        if (ret > 0)
                            await transaction.CommitAsync();
                        else
                            await transaction.RollbackAsync();

                        return ret > 0;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseSectionItemAndImage> AddSectionItem(RequestAddSectionItem request)
        {
            try
            {
                var companyTemplateSectionItemList = new List<CompanyTemplateSectionItemMapping>();
                var companyTemplateSectionImageList = new List<CompanyTemplateSectionImageMapping>();

                await RemoveDuplicateCompanyTemplateSectionItem(request);

                //Add operation will be done if there is items//
                if (request.RequestCompanyTemplateSectionItems.Count > 0)
                {
                    for (int i = 1; i <= request.RequestCompanyTemplateSectionItems.Count; i++)
                    {
                        companyTemplateSectionItemList.Add(new CompanyTemplateSectionItemMapping
                        {
                            CompanyTemplateSectionId = request.CompanyTemplateSectionId,
                            ItemId = request.RequestCompanyTemplateSectionItems[i - 1].ItemId,
                            VariantId = request.RequestCompanyTemplateSectionItems[i - 1].VariantId,
                            PrimaryText = request.RequestCompanyTemplateSectionItems[i - 1].PrimaryText,
                            SecondaryText = request.RequestCompanyTemplateSectionItems[i - 1].SecondaryText,
                            TertiaryText = request.RequestCompanyTemplateSectionItems[i - 1].TertiaryText,
                            DisplayOrder = i,
                            IsActive = true,
                            CreatedBy = request.UserId.ToString(),
                            CreatedAt = DateTime.UtcNow,
                            UpdatedBy = request.UserId.ToString(),
                            UpdatedAt = DateTime.UtcNow
                        });
                    }

                    var distinctItemList = request.RequestCompanyTemplateSectionItems
                        .Select(k => new RequestCompanyTemplateSectionItem { ItemId = k.ItemId, ItemImage = k.ItemImage })
                        .GroupBy(i => i.ItemId).Select(i => i.FirstOrDefault()).ToList();

                    distinctItemList = await RemoveDuplicateCompanyTemplateSectionImage(request, distinctItemList);

                    if (distinctItemList.Count > 0)
                    {
                        for (int i = 0; i < distinctItemList.Count; i++)
                        {
                            companyTemplateSectionImageList.Add(new CompanyTemplateSectionImageMapping
                            {
                                CompanyTemplateSectionId = request.CompanyTemplateSectionId,
                                CreatedAt = DateTime.UtcNow,
                                CreatedBy = request.UserId.ToString(),
                                DisplayOrder = i + 1,
                                ImagePath = distinctItemList[i].ItemImage,
                                ItemId = distinctItemList[i].ItemId,
                                IsActive = true,
                                UpdatedAt = DateTime.UtcNow,
                                UpdatedBy = request.UserId.ToString()
                            });
                        }
                    }
                }

                if (companyTemplateSectionImageList.Any())
                    await _context.CompanyTemplateSectionImageMapping.AddRangeAsync(companyTemplateSectionImageList);
                if (companyTemplateSectionItemList.Any())
                    await _context.CompanyTemplateSectionItemMapping.AddRangeAsync(companyTemplateSectionItemList);

                if (companyTemplateSectionImageList.Any() || companyTemplateSectionItemList.Any())
                    await _context.SaveChangesAsync();

                var section = await GetSectionDataById(_context, request.CompanyTemplateSectionId);
                MakeItemWiseVariantDataForSection(section.ResponseSectionItemAndImage.SectionImages, section.ResponseSectionItemAndImage.SectionItems);
                return section.ResponseSectionItemAndImage;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<bool> EditSectionItemOrder(RequestEditSectionItemOrder request)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        StringBuilder query = new StringBuilder();
                        for (int i = 1; i <= request.CompanyTemplateSectionItemMappingId.Length; i++)
                        {
                            query.Append($"update [dbo].[CompanyTemplateSectionItemMapping] set DisplayOrder={i},UpdatedBy=@UpdatedBy,UpdatedAt=@updatedAt " +
                                $"where CompanyTemplateSectionItemMappingId={request.CompanyTemplateSectionItemMappingId[i - 1]};");
                        }

                        var ret = await _context.Database.ExecuteSqlRawAsync(query.ToString(), new SqlParameter[]
                            {
                                new SqlParameter("@UpdatedBy",request.UserId.ToString()),
                                new SqlParameter("@updatedAt",DateTime.UtcNow)
                            });
                        if (ret > 0)
                            await transaction.CommitAsync();
                        else
                            await transaction.RollbackAsync();

                        return ret > 0;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<bool> EditSectionImageOrder(RequestEditSectionImageOrder request)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        StringBuilder query = new StringBuilder();
                        for (int i = 1; i <= request.CompanyTemplateSectionImageMappingId.Length; i++)
                        {
                            query.Append($"update [dbo].[CompanyTemplateSectionImageMapping] set DisplayOrder={i},UpdatedBy=@UpdatedBy,UpdatedAt=@updatedAt " +
                                $"where CompanyTemplateSectionImageMappingId={request.CompanyTemplateSectionImageMappingId[i - 1]};");
                        }

                        var ret = await _context.Database.ExecuteSqlRawAsync(query.ToString(), new SqlParameter[]
                            {
                                new SqlParameter("@UpdatedBy",request.UserId.ToString()),
                                new SqlParameter("@updatedAt",DateTime.UtcNow)
                            });
                        if (ret > 0)
                            await transaction.CommitAsync();
                        else
                            await transaction.RollbackAsync();

                        return ret > 0;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<bool> ChangeCompanyTemplateDefault(RequestChangeCompanyTemplateDefault request)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var companyTemplate = new CompanyTemplate
                        {
                            CompanyTemplateId = request.CompanyTemplateId,
                            IsDefault = request.IsDefault,
                            UpdatedBy = request.UserId.ToString(),
                            UpdatedAt = DateTime.UtcNow
                        };
                        _context.CompanyTemplate.Attach(companyTemplate);
                        _context.Entry(companyTemplate).Property(x => x.IsDefault).IsModified = true;
                        _context.Entry(companyTemplate).Property(x => x.UpdatedBy).IsModified = true;
                        _context.Entry(companyTemplate).Property(x => x.UpdatedAt).IsModified = true;
                        var ret = await _context.SaveChangesAsync();
                        if (ret > 0)
                        {
                            await UpdateTemplateDefault(_context, (int)request.CompanyId);
                            await transaction.CommitAsync();
                        }
                        else
                            await transaction.RollbackAsync();
                        return ret > 0;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<bool> ChangeCompanyTemplateB2C(RequestChangeCompanyTemplateB2C request)
        {
            try
            {
                var companyTemplate = new CompanyTemplate
                {
                    CompanyTemplateId = request.CompanyTemplateId,
                    IsForB2C = request.IsForB2C,
                    UpdatedBy = request.UserId.ToString(),
                    UpdatedAt = DateTime.UtcNow
                };
                _context.CompanyTemplate.Attach(companyTemplate);
                _context.Entry(companyTemplate).Property(x => x.IsForB2C).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.UpdatedBy).IsModified = true;
                _context.Entry(companyTemplate).Property(x => x.UpdatedAt).IsModified = true;
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<bool> DeleteCompanyTemplateSectionItem(RequestDeleteCompanyTemplateSectionItem request)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync("[dbo].[DeleteCompanyTemplateSectionItem]  @CompanyTemplateSectionItemMappingId", new[] {
                                new SqlParameter("@CompanyTemplateSectionItemMappingId", request.CompanyTemplateSectionItemMappingId)
                            }) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<bool> DeleteCompanyTemplateSectionImage(RequestDeleteCompanyTemplateSectionImage request)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync("[dbo].[DeleteCompanyTemplateSectionImage]  @CompanyTemplateSectionImageMappingId", new[] {
                                new SqlParameter("@CompanyTemplateSectionImageMappingId", request.CompanyTemplateSectionImageMappingId)
                            }) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }


        //----------------------------------------//

        //Save company template with sections//
        private async Task<CompanyTemplate> CreateMapForComapnyTemplate(RequestAddCompanyTemplate request, FronEndTemplate dataTemplte)
        {
            var companyDataTemp = _mapper.Map<FronEndTemplate, CompanyTemplate>(dataTemplte);
            companyDataTemp.IsDefault = request.IsDefault;
            companyDataTemp.CompanyId = request.CompanyId;
            companyDataTemp.Url = "";//hardcoded for now
            companyDataTemp.Type = dataTemplte.Type; //modified
            companyDataTemp.IsForB2C = request.IsForB2C;
            companyDataTemp.CreatedBy = request.UserId.ToString();
            companyDataTemp.CreatedAt = DateTime.UtcNow;
            companyDataTemp.UpdatedBy = request.UserId.ToString();
            companyDataTemp.UpdatedAt = DateTime.UtcNow;
            companyDataTemp.IsActive = true;

            var companyTempSectionsLst = companyDataTemp.CompanyTemplateSections;
            if (companyTempSectionsLst != null && companyTempSectionsLst.Any())
            {
                int i = 1;
                foreach (var companyTempSection in companyTempSectionsLst)
                {
                    companyTempSection.CreatedBy = request.UserId.ToString();
                    companyTempSection.CreatedAt = DateTime.UtcNow;
                    companyTempSection.UpdatedBy = request.UserId.ToString();
                    companyTempSection.UpdatedAt = DateTime.UtcNow;
                    companyTempSection.DisplayOrder = i;
                    companyTempSection.IsActive = true;
                    i++;
                }
            }

            await _context.CompanyTemplate.AddAsync(companyDataTemp);
            var ret = await _context.SaveChangesAsync();

            if (ret > 0)
            {
                await UpdateTemplateDefault(_context, (int)request.CompanyId);
                return companyDataTemp;
            }
            return null;
        }

        //Procedure to manage the default company template//
        private async Task UpdateTemplateDefault(CompanyDBContext _context, int companyId)
        {
            await _context.Database.ExecuteSqlRawAsync("[dbo].[SetDefaultCompanyTemplateForCompany]  @companyId", new[] {
                                new SqlParameter("@companyId",companyId)
                            });
        }

        //Get specific section full data by section id//
        private async Task<ResponseCompanyTemplateSection> GetSectionDataById(CompanyDBContext _context, int companyTemplateSectionId)
        {
            var section =
                await _context.CompanyTemplateSection
                .Include(cts => cts.CompanyTemplateSectionItemMappings.OrderBy(mp => mp.DisplayOrder))
                .Include(cts => cts.CompanyTemplateSectionImageMappings.OrderBy(mp => mp.DisplayOrder))
                .Where(cts => cts.CompanyTemplateSectionId == companyTemplateSectionId)
                .FirstOrDefaultAsync();
            return _mapper.Map<ResponseCompanyTemplateSection>(section);
        }

        private void MakeItemWiseVariantDataForSection(List<ResponseCompanyTemplateSectionImage> itemImages, List<ResponseCompanyTemplateSectionItem> itemVariants)
        {
            foreach (var item in itemImages)
            {
                item.VariantListWithinThisItem = itemVariants.Where(k => k.ItemId == item.ItemId).OrderBy(k => k.DisplayOrder).ToList();
            }
        }

        private async Task RemoveDuplicateCompanyTemplateSectionItem(RequestAddSectionItem request)
        {
            var sectionItemListDB = await _context.CompanyTemplateSectionItemMapping
                                .Where(c => c.CompanyTemplateSectionId == request.CompanyTemplateSectionId)
                                .Select(c => new RequestCompanyTemplateSectionItem
                                {
                                    ItemId = c.ItemId,
                                    VariantId = c.VariantId
                                }).ToListAsync();

            request.RequestCompanyTemplateSectionItems = request.RequestCompanyTemplateSectionItems.Where(k => !sectionItemListDB.Any(c => k.ItemId == c.ItemId && k.VariantId == c.VariantId)).ToList();
        }

        private async Task<List<RequestCompanyTemplateSectionItem>> RemoveDuplicateCompanyTemplateSectionImage(RequestAddSectionItem request, List<RequestCompanyTemplateSectionItem> distinctItemList)
        {
            var sectionImageListDB = await _context.CompanyTemplateSectionImageMapping
                .Where(c => c.CompanyTemplateSectionId == request.CompanyTemplateSectionId)
                .Select(c => new RequestCompanyTemplateSectionItem { ItemId = c.ItemId })
                .ToListAsync();

            distinctItemList = distinctItemList.Where(k => !sectionImageListDB.Any(c => k.ItemId == c.ItemId)).ToList();
            return distinctItemList;
        }

        //--------------------------------------//

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
                    template = await BindTemplateData(template, dataTemplte.FirstOrDefault());
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
            template = await GetTemplateSection(template, dataTemplte.CompanyTemplateId);
            return template;
        }
        private async Task<FrontEndTemplate> GetTemplateSection(FrontEndTemplate template, int templateId)
        {
            var sections = await _context.CompanyTemplateSection.Where(x => x.CompanyTemplateId == templateId && x.IsActive == true).ToListAsync();
            if (sections != null && sections.Count > 0)
            {
                template = await BindSections(template, sections);
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
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    ItemIdBySection id = new ItemIdBySection();
                    id.VariantId = item.VariantId;
                    id.ItemId = item.ItemId;
                    listData.Add(id);
                }
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

        public async Task<List<ResponseFrontEndTemplateFontFamilyMaster>> GetAllFrontEndTemplateFonts()
        {
            try
            {
                var fontList = await _context.FrontEndTemplateFontFamilyMaster.ToListAsync();
                var returnFontList = _mapper.Map<List<ResponseFrontEndTemplateFontFamilyMaster>>(fontList);
                return returnFontList;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseCompanyTemplateSection> GetTemplateSectionForMetaData()
        {
            ResponseCompanyTemplateSection retVal = new ResponseCompanyTemplateSection();
            try
            {
                string sqlText = $"EXECUTE dbo.SP_GetTemplateSectionForMetaDataById";
                var dataList = await _context.TemplateSectionForMetaData.FromSqlRaw(sqlText).ToListAsync();
                if (dataList != null && dataList.Count > 0)
                {
                    retVal.SectionForList = dataList;
                }

                string sqlTextdata = $"EXECUTE dbo.SP_GetTemplateSectionTypeMaster";
                var dataListValue = await _context.TemplateSectionTypemaster.FromSqlRaw(sqlTextdata).ToListAsync();
                if (dataListValue != null && dataListValue.Count > 0)
                {
                    retVal.TemplateSectionTypemaster = dataListValue;
                }
            }
            catch (Exception ex)
            {
                log.Info($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
                throw;
            }
            return retVal;
        }

        public async Task<ResponseCompanyTemplateSection> SaveUpdateCompanyTemplateSectionData(ResponseCompanyTemplateSection request)
        {
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@CompanyTemplateSectionId", request.CompanyTemplateSectionId),
                    new SqlParameter("@CompanyTemplateId", request.CompanyTemplateId),
                    new SqlParameter("@SectionType", request.SectionType),
                    new SqlParameter("@SectionName", request.SectionName),
                    new SqlParameter("@SectionBackgrounColor", request.SectionBackgrounColor??""),
                    new SqlParameter("@IsActive",true),
                    new SqlParameter("@CreatedBy",request.CreatedBy),
                    new SqlParameter("@UpdatedBy", request.UpdatedBy??""),
                    new SqlParameter("@PrimaryText", request.PrimaryText??""),
                    new SqlParameter("@SecondaryText",request.SecondaryText??""),
                    new SqlParameter("@TertiaryText", request.TertiaryText??""),
                    new SqlParameter("@DisplayOrder", request.DisplayOrder),
                    new SqlParameter("@SectionFor",  request.SectionFor),
                };
                string sqlText = $"EXECUTE dbo.SP_SaveUpdateCompanyTemplateSection  @CompanyTemplateSectionId, @CompanyTemplateId, @SectionType, @SectionName, @SectionBackgrounColor, @IsActive, @CreatedBy, @UpdatedBy, @PrimaryText, @SecondaryText, @TertiaryText, @DisplayOrder, @SectionFor";

                var retval = await _context.CompanyTemplateSection.FromSqlRaw(sqlText, parms).ToListAsync();
                return request;
            }
            catch (Exception ex)
            {
                log.Info($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
                throw ex;
            }

        }

        public async Task<List<long>> GetSelectedCustomGroup(RequestCompanyTempalteSectionMappingById request)
        {
            try
            {
                List<long> listData = new List<long>();
                var parms = new SqlParameter[]
                 {

                    new SqlParameter("@CompanyTemplateSectionId",(request.CompanyTemplateSectionId))

                 };

                string sqlText = $"EXECUTE dbo.SP_CompanyTemplateSectionItemMappingById @CompanyTemplateSectionId";
                var data = await _context.CompanyTempalteSectionMappingById.FromSqlRaw(sqlText, parms).ToListAsync();
                if (data != null && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        listData.Add(item.ItemId);
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<List<long>> SaveUpdateCompanyTemplateSectionItemMapping(RequestSectionCustomGroups request)
        {
            List<long> response = new List<long>();
            try
            {
                if (request.RequestCustomSectionIds.Count > 0)
                {
                    var customIds = "";
                    var displayOrder = 0;
                    foreach (var x in request.RequestCustomSectionIds)
                    {
                        customIds += x.Id.ToString() + '#' + displayOrder + '#' + x.CustomGroupImageLink + ",";
                        displayOrder++;
                    }
                    customIds = customIds.Remove(customIds.Length - 1);
                    var parms = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyTemplateSectionId", request.CompanyTemplateSectionId),
                            new SqlParameter("@SectionCustomId", customIds),
                            new SqlParameter("@IsActive",true),
                            new SqlParameter("@CreatedBy", request.UserId.ToString()),
                            new SqlParameter("@UpdatedBy", request.UserId.ToString()),
                    };
                    string sqlText = $"EXECUTE dbo.SP_SaveUpdateCompanyTemplateSectionItemMapping @CompanyTemplateSectionId,@SectionCustomId,@IsActive,@CreatedBy,@UpdatedBy";
                    var retval = await _context.CustomIdList.FromSqlRaw(sqlText, parms).ToListAsync();

                    if (retval != null && retval.Count > 0)
                    {
                        foreach (var item in retval)
                        {
                            response.Add(item.ItemId);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Info($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
                //throw;
            }
            return response;
        }



        public async Task<ResponseSectionItemAndImage> AddSectionItemVariantList(RequestAddSectionItem request)
        {
            //List<long> response = new List<long>();
            try
            {
                var companyTemplateSectionItemList = new List<CompanyTemplateSectionItemMapping>();
                var companyTemplateSectionImageList = new List<CompanyTemplateSectionImageMapping>();

                await RemoveDuplicateCompanyTemplateSectionItem(request);

                //Add operation will be done if there is items//
                if (request.RequestCompanyTemplateSectionItems.Count > 0)
                {
                    for (int i = 1; i <= request.RequestCompanyTemplateSectionItems.Count; i++)
                    {
                        companyTemplateSectionItemList.Add(new CompanyTemplateSectionItemMapping
                        {
                            CompanyTemplateSectionId = request.CompanyTemplateSectionId,
                            ItemId = request.RequestCompanyTemplateSectionItems[i - 1].ItemId,
                            VariantId = request.RequestCompanyTemplateSectionItems[i - 1].VariantId,
                            PrimaryText = request.RequestCompanyTemplateSectionItems[i - 1].PrimaryText,
                            SecondaryText = request.RequestCompanyTemplateSectionItems[i - 1].SecondaryText,
                            TertiaryText = request.RequestCompanyTemplateSectionItems[i - 1].TertiaryText,
                            DisplayOrder = i,
                            IsActive = true,
                            CreatedBy = request.UserId.ToString(),
                            CreatedAt = DateTime.UtcNow,
                            UpdatedBy = request.UserId.ToString(),
                            UpdatedAt = DateTime.UtcNow
                        });
                    }

                    var distinctItemList = request.RequestCompanyTemplateSectionItems
                        .Select(k => new RequestCompanyTemplateSectionItem { ItemId = k.ItemId, VariantId = k.VariantId, ItemImage = k.ItemImage })
                        .GroupBy(i => i.ItemId).Select(i => i.FirstOrDefault()).ToList();

                    distinctItemList = await RemoveDuplicateCompanyTemplateSectionImage(request, distinctItemList);

                    if (distinctItemList.Count > 0)
                    {
                        for (int i = 0; i < distinctItemList.Count; i++)
                        {
                            companyTemplateSectionImageList.Add(new CompanyTemplateSectionImageMapping
                            {
                                CompanyTemplateSectionId = request.CompanyTemplateSectionId,
                                CreatedAt = DateTime.UtcNow,
                                CreatedBy = request.UserId.ToString(),
                                DisplayOrder = i + 1,
                                ImagePath = distinctItemList[i].ItemImage,
                                ItemId = distinctItemList[i].ItemId,
                                IsActive = true,
                                UpdatedAt = DateTime.UtcNow,
                                UpdatedBy = request.UserId.ToString()
                            });
                        }
                    }
                }

                if (companyTemplateSectionImageList.Any())
                    await _context.CompanyTemplateSectionImageMapping.AddRangeAsync(companyTemplateSectionImageList);
                await _context.SaveChangesAsync();
                if (companyTemplateSectionItemList.Any())
                    await _context.CompanyTemplateSectionItemMapping.AddRangeAsync(companyTemplateSectionItemList);
                await _context.SaveChangesAsync();
                //if (companyTemplateSectionImageList.Any() || companyTemplateSectionItemList.Any())
                //    await _context.SaveChangesAsync();
                //if (companyTemplateSectionItemList != null && companyTemplateSectionItemList.Count > 0)
                //{
                //    foreach (var item in companyTemplateSectionItemList)
                //    {
                //        response.Add(item.ItemId);
                //    }
                //}
                var section = await GetSectionDataById(_context, request.CompanyTemplateSectionId);
                MakeItemWiseVariantDataForSection(section.ResponseSectionItemAndImage.SectionImages, section.ResponseSectionItemAndImage.SectionItems);
                return section.ResponseSectionItemAndImage;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

     


    }

}