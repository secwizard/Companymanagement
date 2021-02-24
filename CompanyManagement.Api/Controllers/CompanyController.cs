using CompanyManagement.Api.Data;
using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Service;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession Session => _httpContextAccessor.HttpContext.Session;

        public CompanyController(CompanyDBContext context
            , ICompanyService companyService
            , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _companyService = companyService;
            _httpContextAccessor = httpContextAccessor;
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
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId)
                {
                    responce.Data = await _companyService.GetCompany(request);
                }
                responce.Status = responce.Data != null;
                responce.Message = responce.Data == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(responce);
        }

        [Authorize]
        [HttpPost("GetCompanyList")]
        public async Task<IActionResult> GetCompanyList()
        {
            var responce = new ResponseList<CompanyInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetCompanyList();
                }
                responce.Status = responce.Data != null;
                responce.Message = responce.Data == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(responce);
        }

        [Authorize]
        [HttpPost("GetCompanySmtp")]
        public async Task<IActionResult> GetCompanySmtp(RequestBase request)
        {
            var responce = new Response<CompanyMailServer>();
            try
            {
                var result = await _companyService.GetCompanySmtp(request);
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

        [Authorize]
        [HttpPost("GetCompanyTheme")]
        public async Task<IActionResult> GetCompanyTheme(RequestBase request)
        {
            var responce = new Response<CompanyTheme>();
            try
            {
                var result = await _companyService.GetCompanyTheme(request);
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


        [Authorize]
        [HttpPost("GetCompanySetting")]
        public async Task<IActionResult> GetCompanySetting(RequestCompanySetting request)
        {
            var responce = new ResponseList<CompanySettingInfo>();
            try
            {
                var result = await _companyService.GetCompanySetting(request);
                responce.Data = result;
                responce.Status = result?.Count > 0;
                responce.Message = result?.Count > 0 ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(responce);
        }

        [Authorize]
        [HttpPost("GetCompanyBranch")]
        public async Task<IActionResult> GetCompanyBranch(RequestBase request)
        {
            var responce = new ResponseList<BranchInfo>();
            try
            {
                responce.Data = await _companyService.GetCompanyBranch(request);
                responce.Status = responce.Data?.Count > 0;
                responce.Message = responce.Data?.Count > 0 ? string.Empty : "Data not found.";
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
