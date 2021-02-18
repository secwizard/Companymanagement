using AutoMapper;
using CompanyManagement.Api.Data;
using CompanyManagement.Api.Mapper;
using CompanyManagement.Api.Models;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.Xml;
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
                var companyInfo = new CompanyInfo();

                var company = await _context.Company.Where(c => c.CompanyId == request.CompanyId).FirstOrDefaultAsync();

                if(company !=null)
                {
                    _mapper.Map(company, companyInfo);
                }
                return companyInfo;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
    }
}
