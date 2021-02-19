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

    }
}
