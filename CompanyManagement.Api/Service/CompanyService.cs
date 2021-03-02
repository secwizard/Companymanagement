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

        public async Task<CompanyInfo> GetCompany(RequestBase request)
        {
            try
            {
                var res = new CompanyInfo();

                var data = await _context.Company
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();

                if(data !=null)
                {
                    _mapper.Map(data, res);
                    var reqlookUp = new RequestLookUp { CompanyId = request.CompanyId, LookUpType = "BusinessType" };
                    var lookup = GetCompanyLookUp(reqlookUp).Result;

                    res.LookUps = (from lk in lookup select new LookUpInfo() { LookUpText = lk.LookUpDescription, LookUpValue = lk.LookUpValue }).ToList();
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
                    data = MapMailServer(data,request);
                    data.ModifiedBy = request.CreatedBy;
                    data.ModifiedDate = DateTime.Now;
                    _context.Entry(data).State = EntityState.Modified;
                    _context.SaveChanges();
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
        private MailServer MapMailServer(MailServer preData, CompanyMailServer postData)
        {
            preData.SMTPPort = postData.SMTPPort == null ? 0 : postData.SMTPPort;
            preData.SMTPServer = postData.SMTPServer == null ? "" : postData.SMTPServer;
            preData.FromEmailDisplayName = postData.FromEmailDisplayName == null ? "" : postData.FromEmailDisplayName;
            preData.FromEmailId = postData.FromEmailId == null ? "" : postData.FromEmailId;
            preData.FromEmailPwd = postData.FromEmailPwd == null ? "" : postData.FromEmailPwd;
            preData.IsActive = postData.FromEmailPwd == null ? false : postData.IsActive;
            preData.EnableSSL = postData.EnableSSL == null ? false : postData.EnableSSL;
            return preData;
        }
        public async Task<CompanyTheme> GetCompanyTheme(RequestBase request)
        {
            try
            {
                var res = new CompanyTheme();

                var data = await _context.Theme
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
        public async Task<List<BranchInfo>> GetCompanyBranch(RequestBase request)
        {
            try
            {
                var res = new List<BranchInfo>();

                var data = await _context.Branch
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).ToListAsync();

                if (data != null)
                {
                    res = _mapper.Map<List<Branch>, List<BranchInfo>>(data);
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
                var dataList = _context.CompanySettingInfo.FromSqlRaw(sqlText, parms).ToList();

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
        
        public async Task<ResponseList<CompanySettingInfo>> EditCompanySetting(List<CompanySettingInfo> request)
        {
            var retVal = new ResponseList<CompanySettingInfo>();
            var retData = new List<CompanySettingInfo>();
            try
            {
                foreach(var item in request)
                {
                    var data = await _context.CompanySetting
                    .Where(c => c.CompanyId == item.CompanyId
                    && c.CompanySettingId == item.CompanySettingId
                    && c.IsActive == true).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data = MapCompanySetting(data, item);
                        data.ModifiedBy = item.CreatedBy;
                        data.ModifiedDate = DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;
                        
                    }
                }

                _context.SaveChanges();
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
        private CompanySetting MapCompanySetting(CompanySetting preData, CompanySettingInfo postData)
        {
            preData.DataText = postData.DataText == null ? "" : postData.DataText;
            preData.DataValue = postData.DataValue == null ? "" : postData.DataValue;
            preData.Option1 = postData.Option1 == null ? "" : postData.Option1;
            preData.Option2 = postData.Option2 == null ? "" : postData.Option2;
            preData.Option3 = postData.Option3 == null ? "" : postData.Option3;
            preData.IsActive = postData.IsActive == null ? false : postData.IsActive;
            preData.SettingType = postData.SettingType == null ? "" : postData.SettingType;
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
                var dataList = _context.GetCompanyTemplate.FromSqlRaw(sqlText, parms).ToList();

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

    }
}
