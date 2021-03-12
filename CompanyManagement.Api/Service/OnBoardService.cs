using AutoMapper;
using CompanyManagement.Api.Data;
using CompanyManagement.Api.Mapper;
using CompanyManagement.Api.Models;
using log4net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public class OnBoardService : IOnBoardService
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IMapper _mapper;

        static OnBoardService()
        {
            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            }).CreateMapper();
        }

        public OnBoardService(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<CompanyInfo> GetCompanyDetails(NewCompanyDetails request)
        {
            try
            {
                var res = new CompanyInfo();

                var data = await _context.Company
                    .Where(c => c.CompanyId == request.NewCompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();
                if (data == null)
                    data = new Company();
                if (data != null)
                {
                    _mapper.Map(data, res);
                    var reqlookUp = new RequestLookUp { CompanyId = request.CompanyId, LookUpType = "BusinessType" };
                    var lookup = GetCompanyLookUp(reqlookUp).Result;
                    res.LookUps = (from lk in lookup select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).ToList();
                    res.SelectedLookUp = (from lk in lookup select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).FirstOrDefault();
                    if (res.CompanyId <= 0)
                    {
                        var suggest = GetSuggestedCompanyId(res.SelectedLookUp.LookUpValue).Result;
                        res.SuggestedCompanyId = suggest.CompanyId;
                    }

                }

                return res;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<List<LookUpInfo>> GetRequiredDetails(RequestBase request)
        {
            try
            {
                var res = new List<LookUpInfo>();
                var reqlookUp = new RequestLookUp { CompanyId = request.CompanyId, LookUpType = "BusinessType" };
                var lookup = GetCompanyLookUp(reqlookUp).Result;
                res = (from lk in lookup select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<ResponseCompanyId> GetSuggestedCompanyId(string type)
        {
            ResponseCompanyId response = new ResponseCompanyId();
            response.CompanyId = 0;
            try
            {
                var parms = new SqlParameter[]
                {
                    new SqlParameter("@Type", type),
                };

                string sqlText = $"EXECUTE dbo.[GetSuggestedCompanyId] @Type";
                var dataList = _context.GetSuggestedCompanyId.FromSqlRaw(sqlText, parms).ToList();
                response.CompanyId = dataList.FirstOrDefault().CompanyId + 1;
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
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
                var dataList = _context.GetLookUpType.FromSqlRaw(sqlText, parms).ToList();

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
        public async Task<Response<CompanyInfo>> AddCompany(CompanyInfo request)
        {
            var retVal = new Response<CompanyInfo>();
            try
            {
                var res = new CompanyInfo();
                //var data = new Company();
                var data = await _context.Company
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();

                if (data != null)
                {
                    data = MapCompany(data, request);
                    data.ModifiedBy = request.CreatedBy;
                    data.ModifiedDate = DateTime.Now;
                    _context.Entry(data).State = EntityState.Modified;
                }
                else
                {
                    data = new Company();
                    data = MapCompany(data, request);
                    data.CreatedBy = request.CreatedBy;
                    data.CreatedDate = DateTime.Now;
                    _context.Company.Add(data);
                }

                _context.SaveChanges();
                request.CompanyId = data.CompanyId;
                retVal.Data = request;
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
        private Company MapCompany(Company preData, CompanyInfo postData)
        {
            if (preData == null)
                preData = new Company();
            preData.CompanyId = postData.SuggestedCompanyId;
            preData.Address1 = postData.Address1 == null ? "" : postData.Address1;
            preData.Address2 = postData.Address2 == null ? "" : postData.Address2;
            preData.AdminEmail = postData.AdminEmail == null ? "" : postData.AdminEmail;
            preData.AdminPhone = postData.AdminPhone == null ? "" : postData.AdminPhone;
            preData.BusinessType = postData.BusinessType == null ? "" : postData.BusinessType;
            preData.CountryCode = postData.CountryCode == null ? "" : postData.CountryCode;
            preData.CurrencyCode = postData.CurrencyCode == null ? "" : postData.CurrencyCode;
            preData.DistrictCode = postData.DistrictCode == null ? "" : postData.DistrictCode;
            preData.FavIconFileName = postData.FavIconFileName == null ? "" : postData.FavIconFileName;
            preData.GSTNumber = postData.GSTNumber == null ? "" : postData.GSTNumber;
            preData.ImageFilePath = postData.ImageFilePath == null ? "" : postData.ImageFilePath;
            preData.IsActive = postData.IsActive == null ? false : postData.IsActive;
            preData.LoginImageFileName = postData.LoginImageFileName == null ? "" : postData.LoginImageFileName;
            preData.LogoFileName = postData.LogoFileName == null ? "" : postData.LogoFileName;
            preData.Name = postData.Name == null ? "" : postData.Name;
            preData.PanNumber = postData.PanNumber == null ? "" : postData.PanNumber;
            preData.PIN = postData.PIN == null ? "" : postData.PIN;
            preData.PINRequired = postData.PINRequired == null ? false : postData.PINRequired;
            preData.SecondaryEmail = postData.SecondaryEmail == null ? "" : postData.SecondaryEmail;
            preData.ServiceEmail = postData.ServiceEmail == null ? "" : postData.ServiceEmail;
            preData.ServicePhone = postData.ServicePhone == null ? "" : postData.ServicePhone;
            preData.ShortName = postData.ShortName == null ? "" : postData.ShortName;
            preData.StateCode = postData.StateCode == null ? "" : postData.StateCode;
            preData.Website = postData.Website == null ? "" : postData.Website;
            return preData;
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
        private Theme MapTheme(Theme preData, CompanyTheme postData)
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
        private Branch MapBranch(Branch preData, BranchInfo postData)
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
        private Template MapTemplate(Template preData, GetCompanyTemplate postData)
        {
            preData.CompanyId = postData.CompanyId;
            preData.HTMLData = postData.HTMLData;
            preData.Name = postData.Name;
            preData.Title = postData.Title;
            preData.TemplateType = postData.TemplateType;
            preData.IsActive = postData.IsActive;
            return preData;
        }


        public async Task<ResponseCompanyId> SaveOnBoardProcess(OnBoardProcessinfo process, UserInfo user)
        {
            ResponseCompanyId response = new ResponseCompanyId();
            response.CompanyId = 0;
            try
            {
                if(process != null && process.OnBoardCompanyInfo != null && process.OnBoardCompanyInfo.CompanyInfo != null)
                {
                    var companyData = MapCompany(new Company(),process.OnBoardCompanyInfo.CompanyInfo);
                    if(companyData.CompanyId > 0)
                    {
                        companyData.CreatedBy = user.UserId;
                        companyData.CreatedDate = DateTime.Now;
                        response.CompanyId = companyData.CompanyId;
                        _context.Company.Add(companyData);
                        _context.SaveChanges();
                        if (process.OnBoardCompanyInfo.MailServerInfo != null)
                        {
                            var mail = MapMailServer(new MailServer(), process.OnBoardCompanyInfo.MailServerInfo);
                            mail.CreatedBy = user.UserId;
                            mail.CreatedDate = DateTime.Now;
                            mail.CompanyId = companyData.CompanyId;
                            _context.MailServer.Add(mail);
                        }
                        if(process.OnBoardCompanyInfo.BranchInfo != null && process.OnBoardCompanyInfo.BranchInfo.Count>0)
                        {
                            foreach(var item in process.OnBoardCompanyInfo.BranchInfo)
                            {
                                var branch = MapBranch(new Branch(),item);
                                branch.CreatedBy = user.UserId;
                                branch.CreatedDate = DateTime.Now;
                                branch.CompanyId = companyData.CompanyId;
                                _context.Branch.Add(branch);
                            }
                        }
                        if(process.OnBoardCompanyInfo.CompanySettingInfo != null && process.OnBoardCompanyInfo.CompanySettingInfo.Count>0)
                        {
                            foreach (var item in process.OnBoardCompanyInfo.CompanySettingInfo)
                            {
                                var setting = MapCompanySetting(new CompanySetting(), item);
                                setting.CreatedBy = user.UserId;
                                setting.CreatedDate = DateTime.Now;
                                setting.CompanyId = companyData.CompanyId;
                                _context.CompanySetting.Add(setting);
                            }
                        }
                        if(process.OnBoardCompanyInfo.CompanyTemplate != null && process.OnBoardCompanyInfo.CompanyTemplate.Count>0)
                        {
                            foreach (var item in process.OnBoardCompanyInfo.CompanyTemplate)
                            {
                                var template = MapTemplate(new Template(), item);
                                template.CreatedBy = user.UserId;
                                template.CreatedDate = DateTime.Now;
                                template.CompanyId = companyData.CompanyId;
                                _context.Template.Add(template);
                            }
                        }
                        if(process.OnBoardCompanyInfo.CompanyTheme != null && process.OnBoardCompanyInfo.CompanyTheme.Count>0)
                        {
                            foreach (var item in process.OnBoardCompanyInfo.CompanyTheme)
                            {
                                var theme = MapTheme(new Theme(), item);
                                theme.CreatedBy = user.UserId;
                                theme.CreatedDate = DateTime.Now;
                                theme.CompanyId = companyData.CompanyId;
                                _context.Theme.Add(theme);
                            }
                        }
                        _context.SaveChanges();
                    }


                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}
