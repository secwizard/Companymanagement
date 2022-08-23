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
    public class CompanyService : ICompanyService
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IMapper _mapper;

        static CompanyService()
        {
            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            }).CreateMapper();
        }

        public CompanyService(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<ResponseCompanyDtlByIdFrontend> GetCompanyDtlByIdFrontend(RequestBase request)
        {
            try
            {
                var res = new ResponseCompanyDtlByIdFrontend();

                var data = await _context.Company
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    _mapper.Map(data, res);

                    var themeData = await _context.Theme
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (themeData != null)
                    {
                        var theme = new ThemeData();
                        _mapper.Map(themeData, theme);
                        res.ThemeData = theme;
                    }

                    var templateData = await _context.Template
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).ToListAsync();
                    if (templateData != null)
                    {
                        var footer = new List<FooterData>();
                        _mapper.Map(templateData, footer);
                        res.FooterList = footer;
                    }
                    res.CompTermsConditionOrd = _context.Template.Where(x => x.CompanyId == request.CompanyId
                     && x.TemplateType == "TermsCondition" && x.Name == "ORDER" && x.IsActive == true).FirstOrDefault()?.HTMLData;

                    res.CompanyTermsConditionPayment = _context.Template.Where(x => x.CompanyId == request.CompanyId
                     && x.TemplateType == "TermsCondition" && x.Name == "PAYMENT" && x.IsActive == true).FirstOrDefault()?.HTMLData;

                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<CompanyInfo> GetCompany(RequestBase request)
        {
            try
            {
                var res = new CompanyInfo();

                var data = await _context.Company
                    .Include(c => c.CurrencyMaster)
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    _mapper.Map(data, res);
                    var reqlookUp = new RequestLookUp { CompanyId = request.CompanyId, LookUpType = "BusinessType" };
                    var lookup = GetCompanyLookUp(reqlookUp).Result;

                    res.LookUps = (from lk in lookup select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).ToList();
                    res.SelectedLookUp = (from lk in lookup where lk.LookUpValue.ToLower() == data.BusinessType.ToLower() select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).FirstOrDefault();

                    var parms = new SqlParameter[]{
                    new SqlParameter("@CompanyId", request.CompanyId)};

                    string sqlText = $"EXECUTE dbo.GetCompanySocialLinks @CompanyId";
                    var dataList = await _context.CompanySocialLink.FromSqlRaw(sqlText, parms).ToListAsync();
                    if (dataList?.Count > 0)
                    {
                        res = BindSocialLinkDataMobile(dataList.FirstOrDefault(), res);
                    }
                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseCompanyDtlByIdFrontend> GetCompanyByUrl(RequestCompanyUrl request)
        {
            try
            {
                long companyId = CheckCompanyUrl(request.Url);
                return await GetCompanyDtlByIdFrontend(companyId);
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<Response<CompanyInfo>> EditCompany(CompanyInfo request)
        {
            var retVal = new Response<CompanyInfo>();
            try
            {
                var res = new CompanyInfo();
                var data = await _context.Company
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();

                if (data != null)
                {
                    data = MapCompany(data, request);
                    data.ModifiedBy = request.CreatedBy;
                    data.ModifiedDate = DateTime.Now;
                    _context.Entry(data).State = EntityState.Modified;
                    _context.SaveChanges();
                    var reqlookUp = new RequestLookUp { CompanyId = request.CompanyId, LookUpType = "BusinessType" };
                    var lookup = GetCompanyLookUp(reqlookUp).Result;
                    request.LookUps = (from lk in lookup select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).ToList();
                    request.SelectedLookUp = (from lk in lookup where lk.LookUpValue.ToLower() == data.BusinessType.ToLower() select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).FirstOrDefault();
                    request.CompanyId = data.CompanyId;
                    retVal.Data = request;
                    retVal.Message = "OK";
                    retVal.Status = true;
                }

            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        private Company MapCompany(Company preData, CompanyInfo postData)
        {
            if (preData == null)
                preData = new Company();
            preData.Address1 = postData.Address1;
            preData.Address2 = postData.Address2;
            preData.AdminEmail = postData.AdminEmail;
            preData.AdminPhone = postData.AdminPhone;
            preData.BusinessType = postData.BusinessType;
            preData.CountryCode = postData.CountryCode;
            preData.CurrencyCode = postData.CurrencyCode;
            preData.DistrictCode = postData.DistrictCode;
            preData.FavIconFileName = postData.FavIconFileName;
            preData.GSTNumber = postData.GSTNumber;
            preData.ImageFilePath = postData.ImageFilePath;
            preData.IsActive = postData.IsActive;
            preData.LoginImageFileName = postData.LoginImageFileName;
            preData.LogoFileName = postData.LogoFileName;
            preData.Name = postData.Name;
            preData.PanNumber = postData.PanNumber;
            preData.PIN = postData.PIN;
            preData.PINRequired = postData.PINRequired;
            preData.SecondaryEmail = postData.SecondaryEmail;
            preData.ServiceEmail = postData.ServiceEmail;
            preData.ServicePhone = postData.ServicePhone;
            preData.ShortName = postData.ShortName;
            preData.StateCode = postData.StateCode;
            preData.Website = postData.Website;
            preData.AdminPhoneCode = postData.AdminPhoneCode;
            preData.ServicePhoneCode = postData.ServicePhoneCode;
            preData.AdminPhoneCountryCode = postData.AdminPhoneCountryCode;
            preData.ServicePhoneCountryCode = postData.ServicePhoneCountryCode;
            //preData.CompanySiteUrl = postData.CompanySiteUrl;
            return preData;
        }

        public async Task<List<GetLookUpType>> GetCompanyLookUp(RequestLookUp request)
        {
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@LookUpType", request.LookUpType),
                };

                string sqlText = $"EXECUTE dbo.[GetLookUpType] @LookUpType";
                var dataList = await _context.GetLookUpType.FromSqlRaw(sqlText, parms).ToListAsync();

                if (dataList?.Count > 0)
                {
                    return dataList;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<List<CompanyInfo>> GetCompanyList()
        {
            try
            {
                var res = new List<CompanyInfo>();

                var data = await _context.Company
                    .Where(c => c.IsActive == true).ToListAsync();

                if (data != null)
                {
                    res = _mapper.Map<List<Company>, List<CompanyInfo>>(data);
                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<CompanyMailServer> GetCompanySmtp(RequestBase request)
        {
            try
            {
                var res = new CompanyMailServer();

                var data = await _context.MailServer
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();

                if (data != null)
                {
                    _mapper.Map(data, res);
                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<Response<CompanyMailServer>> EditSTMPServer(CompanyMailServer request)
        {
            var retVal = new Response<CompanyMailServer>();
            try
            {
                var data = await _context.MailServer
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.MailServerId == request.MailServerId
                    && c.IsActive == true).FirstOrDefaultAsync();

                if (data != null)
                {
                    data = MapMailServer(data, request);
                    data.ModifiedBy = request.CreatedBy;
                    data.ModifiedDate = DateTime.Now;
                    _context.Entry(data).State = EntityState.Modified;
                    _context.SaveChanges();
                    request.CompanyId = data.CompanyId;
                    retVal.Data = request.IsActive == true ? request : null;
                    retVal.Message = "OK";
                    retVal.Status = true;
                }
                else
                {
                    data = MapMailServer(new MailServer(), request);
                    data.CreatedBy = request.CreatedBy;
                    data.CreatedDate = DateTime.Now;
                    _context.MailServer.Add(data);
                    _context.SaveChanges();
                    request.CompanyId = data.CompanyId;
                    retVal.Data = request.IsActive == true ? request : null;
                    retVal.Message = "OK";
                    retVal.Status = true;
                }

            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        private MailServer MapMailServer(MailServer preData, CompanyMailServer postData)
        {
            preData.CompanyId = postData.CompanyId;
            preData.SMTPPort = postData.SMTPPort == null ? 0 : postData.SMTPPort;
            preData.SMTPServer = postData.SMTPServer == null ? "" : postData.SMTPServer;
            preData.FromEmailDisplayName = postData.FromEmailDisplayName == null ? "" : postData.FromEmailDisplayName;
            preData.FromEmailId = postData.FromEmailId == null ? "" : postData.FromEmailId;
            preData.FromEmailPwd = postData.FromEmailPwd == null ? "" : postData.FromEmailPwd;
            preData.IsActive = postData.FromEmailPwd == null ? false : postData.IsActive;
            preData.EnableSSL = postData.EnableSSL == null ? false : postData.EnableSSL;
            return preData;
        }

        public async Task<List<GetCompanyTheme>> GetCompanyTheme(RequestBase request)
        {
            try
            {
                var parms = new SqlParameter[]
                 {
                    new SqlParameter("@CompanyId", request.CompanyId),
                 };

                string sqlText = $"EXECUTE dbo.[GetCompanyTheme] @CompanyId";
                var dataList = await _context.GetCompanyTheme.FromSqlRaw(sqlText, parms).ToListAsync();
                return dataList;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseList<GetCompanyTheme>> EditTheme(GetCompanyTheme request)
        {
            var retVal = new ResponseList<GetCompanyTheme>();
            var retData = new List<GetCompanyTheme>();
            try
            {
                if (request != null && request.ThemeId > 0)
                {
                    var data = await _context.Theme
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.ThemeId == request.ThemeId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {

                        data = MapTheme(data, request);
                        data.ModifiedBy = request.CreatedBy;
                        data.ModifiedDate = DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;

                    }
                }
                else
                {
                    var data = MapTheme(new Theme(), request);
                    data.CreatedBy = request.CreatedBy;
                    data.CreatedDate = DateTime.Now;
                    _context.Theme.Add(data);
                }
                _context.SaveChanges();
                RequestBase req = new RequestBase();
                req.CompanyId = request.CompanyId;

                retVal.Data = GetCompanyTheme(req).Result;
                retVal.Message = "OK";
                retVal.Status = true;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        public async Task<ResponseList<GetCompanyTheme>> DeleteTheme(DeleteCompanyTheme request)
        {
            var retVal = new ResponseList<GetCompanyTheme>();
            try
            {
                if (request != null && request.ThemeId > 0)
                {
                    var data = await _context.Theme
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.ThemeId == request.ThemeId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.IsActive = false;
                        data.ModifiedBy = request.UserId;
                        data.ModifiedDate = DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;
                        _context.SaveChanges();
                        RequestBase req = new RequestBase();
                        req.CompanyId = request.CompanyId;

                        retVal.Data = GetCompanyTheme(req).Result;
                        retVal.Message = "OK";
                        retVal.Status = true;
                    }
                    else
                    {
                        retVal.Status = false;
                    }

                }
                else
                {
                    retVal.Status = false;
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        private Theme MapTheme(Theme preData, GetCompanyTheme postData)
        {
            preData.CompanyId = postData.CompanyId;
            preData.DesktopHeight = postData.DesktopHeight;
            preData.ExtThemeName = postData.ExtThemeName;
            preData.ImageRatio = postData.ImageRatio;
            preData.MobileHeight = postData.MobileHeight;
            preData.NoOfHomePanels = postData.NoOfHomePanels;
            preData.ThemeName = postData.ThemeName;
            preData.Colour = postData.Colour;
            preData.MobileHeight = postData.MobileHeight;
            preData.IsActive = postData.IsActive;
            preData.IsDefault = postData.IsDefault;
            return preData;
        }

        public async Task<List<BranchInfo>> GetCompanyBranch(RequestBase request)
        {
            try
            {
                var res = new List<BranchInfo>();

                var data = await _context.Branch
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).ToListAsync();
                res = _mapper.Map<List<Branch>, List<BranchInfo>>(data);
                return res;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<ResponseList<BranchInfo>> EditBranch(Branch request)
        {
            var retVal = new ResponseList<BranchInfo>();
            try
            {
                if (request != null && request.BranchId > 0)
                {
                    var data = await _context.Branch
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.BranchId == request.BranchId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data = MapBranch(data, request);
                        data.ModifiedBy = request.CreatedBy;
                        data.ModifiedDate = DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;

                    }
                }
                else
                {
                    var data = MapBranch(new Branch(), request);
                    data.CreatedDate = DateTime.Now;
                    data.CreatedBy = request.CreatedBy;
                    _context.Branch.Add(data);
                }
                _context.SaveChanges();
                RequestBase req = new RequestBase();
                req.CompanyId = request.CompanyId;

                retVal.Data = GetCompanyBranch(req).Result;
                retVal.Message = "OK";
                retVal.Status = true;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        public async Task<ResponseList<BranchInfo>> DeleteBranch(DeleteCompanyBranch request)
        {
            var retVal = new ResponseList<BranchInfo>();
            try
            {
                if (request != null && request.BranchId > 0)
                {
                    var data = await _context.Branch
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.BranchId == request.BranchId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.ModifiedBy = request.UserId;
                        data.ModifiedDate = DateTime.Now;
                        data.IsActive = false;
                        _context.Entry(data).State = EntityState.Modified;
                        _context.SaveChanges();
                        RequestBase req = new RequestBase();
                        req.CompanyId = request.CompanyId;

                        retVal.Data = GetCompanyBranch(req).Result;
                        retVal.Message = "OK";
                        retVal.Status = true;

                    }
                    else
                    {
                        retVal.Status = false;
                    }
                }
                else
                {
                    retVal.Status = false;
                }

            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        private Branch MapBranch(Branch preData, Branch postData)
        {
            preData.CompanyId = postData.CompanyId;
            preData.Address1 = postData.Address1;
            preData.Address2 = postData.Address2;
            preData.Code = postData.Code;
            preData.Country = postData.Country;
            preData.District = postData.District;
            preData.Email = postData.Email;
            preData.IsActive = postData.IsActive;
            preData.Name = postData.Name;
            preData.Phone = postData.Phone;
            preData.PostalCode = postData.PostalCode;
            preData.State = postData.State;
            return preData;
        }

        public async Task<List<CompanySettingInfo>> GetCompanySetting(RequestCompanySetting request)
        {
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@CompanyId", request.CompanyId),
                    new SqlParameter("@SettingType", request.SettingType??""),
                    new SqlParameter("@DataText", request.DataText??"")
                };

                string sqlText = $"EXECUTE dbo.[GetCompanySettings] @CompanyId, @SettingType, @DataText";
                var dataList = await _context.CompanySettingInfo.FromSqlRaw(sqlText, parms).ToListAsync();
                return dataList;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseList<CompanySettingInfo>> EditCompanySetting(CompanySettingInfo request)
        {
            var retVal = new ResponseList<CompanySettingInfo>();
            var retData = new List<CompanySettingInfo>();
            try
            {
                if (request != null && request.CompanySettingId > 0)
                {
                    var data = await _context.CompanySetting
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.CompanySettingId == request.CompanySettingId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data = MapCompanySetting(data, request);
                        data.ModifiedBy = request.CreatedBy;
                        data.ModifiedDate = DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;

                    }
                }
                else
                {
                    var data = MapCompanySetting(new CompanySetting(), request);
                    data.CreatedDate = DateTime.Now;
                    data.CreatedBy = request.CreatedBy;
                    _context.CompanySetting.Add(data);
                }
                _context.SaveChanges();
                RequestCompanySetting req = new RequestCompanySetting();
                req.CompanyId = request.CompanyId;
                req.DataText = "";
                req.SettingType = "";

                retVal.Data = GetCompanySetting(req).Result;
                retVal.Message = "OK";
                retVal.Status = true;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        public async Task<ResponseList<CompanySettingInfo>> DeleteCompanySetting(DeleteCompanySettings request)
        {
            var retVal = new ResponseList<CompanySettingInfo>();
            var retData = new List<CompanySettingInfo>();
            try
            {
                if (request != null && request.CompanySettingsId > 0)
                {
                    var data = await _context.CompanySetting
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.CompanySettingId == request.CompanySettingsId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.ModifiedBy = request.UserId;
                        data.ModifiedDate = DateTime.Now;
                        data.IsActive = false;
                        _context.Entry(data).State = EntityState.Modified;
                        _context.SaveChanges();
                        RequestCompanySetting req = new RequestCompanySetting();
                        req.CompanyId = request.CompanyId;
                        req.DataText = "";
                        req.SettingType = "";

                        retVal.Data = GetCompanySetting(req).Result;
                        retVal.Message = "OK";
                        retVal.Status = true;
                    }
                    else
                    {
                        retVal.Status = false;
                    }
                }
                else
                {
                    retVal.Status = false;
                }


            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        private CompanySetting MapCompanySetting(CompanySetting preData, CompanySettingInfo postData)
        {
            preData.CompanyId = postData.CompanyId;
            preData.DataText = postData.DataText;
            preData.DataValue = postData.DataValue;
            preData.Option1 = postData.Option1;
            preData.Option2 = postData.Option2;
            preData.Option3 = postData.Option3;
            preData.IsActive = postData.IsActive;
            preData.SettingType = postData.SettingType;
            return preData;
        }

        public async Task<List<GetCompanyTemplate>> GetCompanyTemplate(RequestBase request)
        {
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@CompanyId", request.CompanyId),
                };

                string sqlText = $"EXECUTE dbo.[GetCompanyTemplate] @CompanyId";
                var dataList = await _context.GetCompanyTemplate.FromSqlRaw(sqlText, parms).ToListAsync();
                return dataList;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<List<GetCompanyTemplate>> GetCompanyTemplateByTypeAndName(RequestGetCompanyTemplateByTypeAndName request)
        {
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@CompanyId", request.CompanyId),
                    new SqlParameter("@TemplateType", request.TemplateType),
                    new SqlParameter("@Name", request.Name),
                };

                string sqlText = $"EXECUTE dbo.[GetCompanyTemplateByTypeAndName] @CompanyId, @TemplateType, @Name";
                var dataList = await _context.GetCompanyTemplate.FromSqlRaw(sqlText, parms).ToListAsync();
                return dataList;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseList<GetCompanyTemplate>> EditTemplate(Template request)
        {
            var retVal = new ResponseList<GetCompanyTemplate>();
            var retData = new List<CompanySettingInfo>();
            try
            {
                if (request != null && request.TemplateId > 0)
                {
                    var data = await _context.Template
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.TemplateId == request.TemplateId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data = MapTemplate(data, request);
                        data.ModifiedBy = request.CreatedBy;
                        data.ModifiedDate = DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;

                    }
                }
                else
                {
                    var data = MapTemplate(new Template(), request);
                    data.CreatedDate = DateTime.Now;
                    data.CreatedBy = request.CreatedBy;
                    _context.Template.Add(data);
                }
                _context.SaveChanges();
                RequestBase req = new RequestBase();
                req.CompanyId = request.CompanyId;

                retVal.Data = GetCompanyTemplate(req).Result;
                retVal.Message = "OK";
                retVal.Status = true;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        public async Task<ResponseList<GetCompanyTemplate>> DeleteTemplate(DeleteCompanyTemplate request)
        {
            var retVal = new ResponseList<GetCompanyTemplate>();
            var retData = new List<CompanySettingInfo>();
            try
            {
                if (request != null && request.TemplateId > 0)
                {
                    var data = await _context.Template
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.TemplateId == request.TemplateId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.ModifiedBy = request.UserId;
                        data.ModifiedDate = DateTime.Now;
                        data.IsActive = false;
                        _context.Entry(data).State = EntityState.Modified;
                        _context.SaveChanges();
                        RequestBase req = new RequestBase();
                        req.CompanyId = request.CompanyId;

                        retVal.Data = GetCompanyTemplate(req).Result;
                        retVal.Message = "OK";
                        retVal.Status = true;

                    }
                    else
                    {
                        retVal.Status = false;
                    }
                }
                else
                {
                    retVal.Status = false;
                }

            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        private Template MapTemplate(Template preData, Template postData)
        {
            preData.CompanyId = postData.CompanyId;
            preData.HTMLData = postData.HTMLData;
            preData.Name = postData.Name;
            preData.Title = postData.Title;
            preData.TemplateType = postData.TemplateType;
            preData.IsActive = postData.IsActive;
            return preData;
        }

        public async Task<CompanyInfo> CheckCompanyUrlAndShortName(RequestCheckCompanyUrlAndShortName request)
        {
            try
            {
                var data = await _context.Company.Where(x => x.CompanySiteUrl.ToLower() == request.CompUrl.ToLower() && x.ShortName == request.CompShortName
                 && x.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    var ret = new CompanyInfo
                    {
                        CompanyId = data.CompanyId
                    };
                    return ret;
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
            return null;
        }

        public async Task<CompanyInfo> CheckCompanyUrlFrontend(RequestCheckCompanyUrlAndShortName request)
        {
            try
            {
                var data = await _context.Company.Where(x => x.CompanySiteUrl.ToLower() == request.CompUrl.ToLower() && x.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    var ret = new CompanyInfo
                    {
                        CompanyId = data.CompanyId
                    };
                    return ret;
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
            return null;
        }
        private long CheckCompanyUrl(string url)
        {
            String[] strlist = url.Split('?');
            long companyid = 0;
            try
            {
                if (strlist.Count() > 1)
                {
                    var urlpart = strlist[1].Split('=')[1];
                    var compUrl = strlist[0];
                    var data = _context.Company.Where(x => x.CompanySiteUrl.ToLower() == compUrl.ToLower() && x.ShortName.ToLower() == urlpart.ToLower() && x.IsActive == true).FirstOrDefault();
                    if (data != null)
                    {
                        return data.CompanyId;
                    }
                }
                else
                {
                    var compUrl = strlist[0];
                    var split = compUrl.IndexOf(".com/");
                    if (split > 0)
                    {
                        var x = compUrl.Substring(0, split + 5);
                        compUrl = x;
                    }
                    var data = _context.Company.Where(x => x.CompanySiteUrl.ToLower() == compUrl.ToLower() && x.IsActive == true).FirstOrDefault();
                    if (data != null)
                    {
                        return data.CompanyId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return companyid;
        }
        private ResponseCompanyDtlByIdFrontend BindSocialLinkDataWebsite(CompanySocialLink social, ResponseCompanyDtlByIdFrontend res)
        {
            res.Facebook = social.Facebook;
            res.ShowFacebookOnline = social.ShowFacebookOnline;
            res.Instagram = social.Instagram;
            res.ShowInstagramOnline = social.ShowInstagramOnline;
            res.Twitter = social.Twitter;
            res.ShowTwitterOnline = social.ShowTwitterOnline;
            res.ContactEmail = social.ContactEmail;
            res.ShowContactEmailOnline = social.ShowContactEmailOnline;
            res.ContactPhone = social.ContactPhone;
            res.ShowContactPhoneOnline = social.ShowContactPhoneOnline;
            return res;
        }

        private CompanyInfo BindSocialLinkDataMobile(CompanySocialLink social, CompanyInfo res)
        {
            res.Facebook = social.Facebook;
            res.ShowFacebookOnline = social.ShowFacebookOnline;
            res.Instagram = social.Instagram;
            res.ShowInstagramOnline = social.ShowInstagramOnline;
            res.Twitter = social.Twitter;
            res.ShowTwitterOnline = social.ShowTwitterOnline;
            res.ContactEmail = social.ContactEmail;
            res.ShowContactEmailOnline = social.ShowContactEmailOnline;
            res.ContactPhone = social.ContactPhone;
            res.ShowContactPhoneOnline = social.ShowContactPhoneOnline;
            return res;
        }
        private async Task<ResponseCompanyDtlByIdFrontend> GetCompanyDtlByIdFrontend(long CompanyId)
        {
            try
            {
                var res = new ResponseCompanyDtlByIdFrontend();

                var data = await _context.Company
                    .Include(c => c.CurrencyMaster)
                    .Where(c => c.CompanyId == CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    _mapper.Map(data, res);
                    res.IsPhonePeActive = data.IsPhonePeActive ?? false;
                    res.CompanyLogo = data.ImageFilePath + "" + data.LogoFileName;
                    res.GoogleClientId = data.GoogleClientId;
                    res.FaceBookApiId = data.FaceBookApiId;
                    res.GoogleClientSecret = data.GoogleClientSecret;
                    res.ShowCity = data.ShowCity??false;
                    res.ShowDistrict = data.ShowDistrict ?? false;
                    res.ShowState = data.ShowState ?? false;
                    res.AdminPhoneCode = data.AdminPhoneCode;
                    res.AdminPhoneCountryCode = data.AdminPhoneCountryCode;
                    res.ServicePhoneCode = data.ServicePhoneCode;
                    res.ServicePhoneCountryCode = data.ServicePhoneCountryCode;
                    res.FavIconFileName = data.FavIconFileName;

                    var themeData = await _context.Theme
                                .Where(c => c.CompanyId == CompanyId
                                && c.IsActive == true).FirstOrDefaultAsync();
                    if (themeData != null)
                    {
                        var theme = new ThemeData();
                        _mapper.Map(themeData, theme);
                        res.ThemeData = theme;
                    }

                    var templateData = await _context.Template
                    .Where(c => c.CompanyId == CompanyId
                    && c.IsActive == true).ToListAsync();
                    if (templateData != null)
                    {
                        var footer = new List<FooterData>();
                        _mapper.Map(templateData, footer);
                        res.FooterList = footer;
                    }
                    res.CompTermsConditionOrd = _context.Template.Where(x => x.CompanyId == CompanyId
                     && x.TemplateType == "TermsCondition" && x.Name == "ORDER" && x.IsActive == true).FirstOrDefault()?.HTMLData;

                    res.CompanyTermsConditionPayment = _context.Template.Where(x => x.CompanyId == CompanyId
                     && x.TemplateType == "TermsCondition" && x.Name == "PAYMENT" && x.IsActive == true).FirstOrDefault()?.HTMLData;



                    var parms = new SqlParameter[]{
                    new SqlParameter("@CompanyId", CompanyId)};

                    string sqlText = $"EXECUTE dbo.GetCompanySocialLinks @CompanyId";
                    var dataList = await _context.CompanySocialLink.FromSqlRaw(sqlText, parms).ToListAsync();
                    if (dataList?.Count > 0)
                    {
                        res = BindSocialLinkDataWebsite(dataList.FirstOrDefault(), res);
                    }


                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<bool> GetIsPINRequired(RequestBase request)
        {
            try
            {
                var data = await _context.Company
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    return Convert.ToBoolean(data.PINRequired);
                }
                return false;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<string> GetCompanyCurrencyCode(RequestBase request)
        {
            try
            {
                var data = await _context.Company
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    return data.CurrencyCode;
                }
                return "";
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<CompanyDetailsForSentMail> GetCompanyDetailsForSentMail(RequestBase request)
        {
            try
            {
                var parms = new SqlParameter[]
                 {
                       new SqlParameter("@CompanyId", request.CompanyId),
                 };

                string sqlText = $"dbo.GetCompanyMailInfo @CompanyId";
                var mailInfo = await _context.CompanyDetailsForSentMail
                    .FromSqlRaw(sqlText, parms)
                    .ToListAsync();

                if (mailInfo?.Count > 0) return mailInfo.FirstOrDefault();
                else return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<ResponseMail> SendMail(NotificationMetadata notificationMetadata, RequestSendMail requestSendMail)
        {
            var responce = new ResponseMail();
            responce.Status = false;
            try
            {
                var mimeMessage = CreateMimeMessage(requestSendMail);
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(notificationMetadata.SmtpServer,
                    notificationMetadata.Port, notificationMetadata.EnableSSL);
                    await smtpClient.AuthenticateAsync(notificationMetadata.UserName,
                    notificationMetadata.Password);
                    await smtpClient.SendAsync(mimeMessage);
                    await smtpClient.DisconnectAsync(true);
                }
                responce.Status = true;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                responce.Message = ex.Message;
            }
            return responce;
        }
        private MimeMessage CreateMimeMessage(RequestSendMail requestSendMail)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Admin", requestSendMail.EmailFrom));
            if (!string.IsNullOrEmpty(requestSendMail.EmailTo))
            {
                foreach (var address in requestSendMail.EmailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.To.Add(new MailboxAddress(address));
                }
            }
            if (!string.IsNullOrEmpty(requestSendMail.EmailCC))
            {
                foreach (var address in requestSendMail.EmailCC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.Cc.Add(new MailboxAddress(address));
                }
            }
            mimeMessage.Subject = requestSendMail.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            { Text = requestSendMail.Message };
            return mimeMessage;
        }
        public async Task<bool> EditProductInclusiveOfTax(RequestProductInclusiveOfTax request)
        {
            try
            {
                bool result = false;
                var commandText = "UPDATE [dbo].[CompanySetting] SET [IsAllProductInclusiveOfTax]=@IsAllProductInclusiveOfTax WHERE [CompanyId]=@CompanyId";
                var parms = new SqlParameter[]
                {
                   new SqlParameter("@IsAllProductInclusiveOfTax", request.IsAllProductInclusiveOfTax),
                   new SqlParameter("@CompanyId", request.CompanyId)
                };
                result = await _context.Database.ExecuteSqlRawAsync(commandText, parms) > 0 ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<bool> IsProductInclusiveOfTax(RequestBase request)
        {
            try
            {
                return
                    (await _context.CompanySetting
                    .FirstOrDefaultAsync(k => k.CompanyId == request.CompanyId))
                    .IsAllProductInclusiveOfTax;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseSaveTwillioNotificationService> SaveTwillioNotificationService(RequestSaveNotificationServiceDetails request)
        {
            try
            {
                var parms = new SqlParameter[]
                     {
                          new SqlParameter("@Id", request.Id),
                          new SqlParameter("@CompanyId", request.CompanyId),
                          new SqlParameter("@ServiceName", request.ServiceName),
                          new SqlParameter("@AccountSID", request.AccountSID??""),
                          new SqlParameter("@AuthToken", request.AuthToken??""),
                          new SqlParameter("@FromNumber", request.FromNumber??""),
                          new SqlParameter("@SortCode", request.SortCode??""),
                          new SqlParameter("@APIKey", request.APIKey??""),
                          new SqlParameter("@SenderId", request.SenderId??""),
                          new SqlParameter("@URLLink", request.URLLink??""),
                          new SqlParameter("@SMTPServerAddress", request.SMTPServerAddress??""),
                          new SqlParameter("@MailSendPort", request.MailSendPort??""),
                          new SqlParameter("@FromEmailId", request.FromEmailId??""),
                          new SqlParameter("@SMTPUserId", request.SMTPUserId??""),
                          new SqlParameter("@SMTPPassword", request.SMTPPassword??""),
                          new SqlParameter("@IsSSLEnabled", request.IsSSLEnabled),
                          new SqlParameter("@RoboCallFromNumber", request.RoboCallFromNumber??""),
                          new SqlParameter("@MessagingServiceSid", request.MessagingServiceSid??""),

                     };

                string sqlText = $"dbo.SP_SaveCompanySecretForTwillioNotificationService @Id, @CompanyId, @ServiceName,@AccountSID,@AuthToken,@FromNumber,@SortCode,@APIKey,@SenderId,@URLLink,@SMTPServerAddress,@MailSendPort,@FromEmailId,@SMTPUserId,@SMTPPassword,@IsSSLEnabled,@RoboCallFromNumber,@MessagingServiceSid";
                var Id = await _context.AddEditTwillioNotificationService.FromSqlRaw(sqlText, parms).ToListAsync();
                return Id.FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
                throw ex;
            }
        }
        public async Task<ResponseGetNotificationServiceDetails> GetNotificationServiceService(RequestGetNotificationSettingsServiceDetails request)
        {
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@CompanyId", request.CompanyId),
                    new SqlParameter("@ServiceName", request.ServiceName)

                };

                string sqlText = $"EXECUTE dbo.SP_GetCompanySecretForTwillioNotificationService @CompanyId,@ServiceName";
                var dataList = await _context.ResponseGetNotificationServiceDetails.FromSqlRaw(sqlText, parms).ToListAsync();
                if (dataList?.Count > 0)
                {
                    return dataList.FirstOrDefault();
                }
                ResponseGetNotificationServiceDetails resBlankObj = new ResponseGetNotificationServiceDetails();
                resBlankObj = await GetNotificationServiceDefaultObject();
                return resBlankObj;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseGetNotificationServiceDetails> GetNotificationServiceDefaultObject()
        {
            ResponseGetNotificationServiceDetails resBlankObj = new ResponseGetNotificationServiceDetails();
            try
            {
                resBlankObj.AccountSID = "";
                resBlankObj.AuthToken = "";
                resBlankObj.FromNumber = "";
                resBlankObj.ServiceName = "";
                resBlankObj.SortCode = "";
                resBlankObj.APIKey = "";
                resBlankObj.SenderId = "";
                resBlankObj.URLLink = "";
                resBlankObj.SMTPServerAddress = "";
                resBlankObj.MailSendPort = "";
                resBlankObj.FromEmailId = "";
                resBlankObj.SMTPUserId = "";
                resBlankObj.SMTPPassword = "";
                resBlankObj.IsSSLEnabled = false;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
            return resBlankObj;
        }

        public async Task<CompanySocialLinkResponse> GetAllSocialDetails(SocialReqById reqById)
        {
            CompanySocialLinkResponse retVal = new CompanySocialLinkResponse();
            try
            {
                var parms = new SqlParameter[]{
                    new SqlParameter("@CompanyId", reqById.CompanyId)};

                string sqlText = $"EXECUTE dbo.GetCompanySocialLinks @CompanyId";
                var dataList = await _context.CompanySocialLink.FromSqlRaw(sqlText, parms).ToListAsync();
                if (dataList != null && dataList.Count > 0)
                    return BindSocialData(dataList.FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
                throw;
            }
            return retVal;
        }
        private CompanySocialLinkResponse BindSocialData(CompanySocialLink socialData)
        {
            CompanySocialLinkResponse data = new CompanySocialLinkResponse();
            data.CompanyId = socialData.CompanyId;
            data.CompanySocialLinkId = socialData.CompanySocialLinkId;
            data.IsActive = socialData.IsActive;
            data.Facebook = socialData.Facebook;
            data.ShowFacebookOnline = socialData.ShowFacebookOnline;
            data.Instagram = socialData.Instagram;
            data.ShowInstagramOnline = socialData.ShowInstagramOnline;
            data.Twitter = socialData.Twitter;
            data.ShowTwitterOnline = socialData.ShowTwitterOnline;
            data.ContactPhone = socialData.ContactPhone;
            data.ShowContactPhoneOnline = socialData.ShowContactPhoneOnline;
            data.ContactEmail = socialData.ContactEmail;
            data.ShowContactEmailOnline = socialData.ShowContactEmailOnline;
            data.CreatedByUserId = socialData.CreatedByUserId;
            data.CreatedAt = socialData.CreatedAt;
            //data.CreatedAt = socialData.I(socialData.GetOrdinal("CreatedAt")) ? DateTime.Now : Convert.ToDateTime(socialData["CreatedAt"]);
            data.UpdatedByUserID = socialData.UpdatedByUserID;
            data.UpdatedAt = socialData.UpdatedAt;
            return data;
        }
        public async Task<CompanySocialLink> SaveUpdateSocialLink(CompanySocialLinkRequest request)
        {
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@CompanySocialLinkId", request.CompanySocialLinkId),
                    new SqlParameter("@CompanyId", request.CompanyId),
                    new SqlParameter("@IsActive", true),
                    new SqlParameter("@Facebook", request.Facebook),
                    new SqlParameter("@ShowFacebookOnline", request.ShowFacebookOnline),
                    new SqlParameter("@Instagram", request.Instagram),
                    new SqlParameter("@ShowInstagramOnline", request.ShowInstagramOnline),
                    new SqlParameter("@Twitter", request.Twitter),
                    new SqlParameter("@ShowTwitterOnline", request.ShowInstagramOnline),
                    new SqlParameter("@ContactEmail", request.ContactEmail),
                    new SqlParameter("@ShowContactEmailOnline", request.ShowContactEmailOnline),
                    new SqlParameter("@ContactPhone", request.ContactPhone),
                    new SqlParameter("@ShowContactPhoneOnline", request.ShowContactPhoneOnline),
                    new SqlParameter("@CreatedByUserId", request.CreatedByUserId == null ? new Guid() : request.CreatedByUserId),
                    new SqlParameter("@CreatedAt",  request.CreatedAt),
                   // new SqlParameter("@CreatedAt", Common.StringToDateTime(request.CreatedAt_Str)),
                    new SqlParameter("@UpdatedByUserID", request.UpdatedByUserID == null ? new Guid() : request.UpdatedByUserID),

                    new SqlParameter("@UpdatedAt", request.UpdatedAt??System.DateTime.Now)


                };
                string sqlText = $"EXECUTE dbo.SP_SaveUpdateSocialLink  @CompanySocialLinkId, @CompanyId, @IsActive, @Facebook, @ShowFacebookOnline, @Instagram, @ShowInstagramOnline, @Twitter, @ShowTwitterOnline, @ContactEmail, @ShowContactEmailOnline, @ContactPhone, @ShowContactPhoneOnline, @CreatedByUserId, @CreatedAt, @UpdatedByUserID, @UpdatedAt";
                var retval = await _context.CompanySocialLink.FromSqlRaw(sqlText, parms).ToListAsync();


                return retval.FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<CompanySocialLink> GetSocialLinkById(SocialReqById request)
        {
            try
            {
                var parms = new SqlParameter[] {
                    new SqlParameter("@CompanyId", request.CompanyId),
                    new SqlParameter("@CompanySocialLinkId", request.CompanySocialLinkId)
                };
                string sqlText = $"EXECUTE dbo.SP_GetFaqById @CompanyId, @CompanySocialLinkId";
                var socialDtl = await _context.CompanySocialLink.FromSqlRaw(sqlText, parms).ToListAsync();
                return socialDtl.FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
                throw;
            }
        }

    }
}
