using CompanyManagement.Api.Data;
using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Service;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ICompanyService _companyService;



        public CompanyController(CompanyDBContext context
            , ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }


        [HttpGet]
        public async Task<IActionResult> TestConnection()
        {
            return Ok("Company API connected.");
        }

        [Authorize]
        [HttpPost("GetCompany")]
        public async Task<IActionResult> GetCompany(RequestBase request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {
                var result = await _companyService.GetCompany(request);
                responce.Data = result;
                responce.Status = result != null;
                responce.Message = result == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(responce);
        }
    }
}
