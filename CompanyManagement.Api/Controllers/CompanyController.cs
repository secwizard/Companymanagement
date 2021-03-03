using CompanyManagement.Api.Data;
using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Service;
using log4net;
using Microsoft.AspNetCore.Http;
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
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1) 
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
        [HttpPost("EditCompany")]
        public async Task<IActionResult> EditCompany(CompanyInfo request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.EditCompany(request);
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
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetCompanySmtp(request);
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
        [HttpPost("EditSTMPServer")]
        public async Task<IActionResult> EditSTMPServer(CompanyMailServer request)
        {
            var responce = new Response<CompanyMailServer>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.EditSTMPServer(request);
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
        [HttpPost("GetCompanyTheme")]
        public async Task<IActionResult> GetCompanyTheme(RequestBase request)
        {
            var responce = new ResponseList<GetCompanyTheme>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetCompanyTheme(request);
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
        //[Authorize]
        //[HttpPost("EditThemeSetting")]
        //public async Task<IActionResult> EditThemeSetting(Theme request)
        //{
        //    var responce = new ResponseList<>();
        //    try
        //    {
        //        var user = (UserInfo)HttpContext.Items["User"];
        //        if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
        //        {
        //            responce = await _companyService.EditTemplate(request);
        //        }
        //        responce.Status = responce.Data != null;
        //        responce.Message = responce.Data == null ? "Data not found." : string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        responce.Status = false;
        //        responce.Message = ex.Message;
        //        log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
        //    }
        //    return Ok(responce);
        //}
        [Authorize]
        [HttpPost("GetCompanySetting")]
        public async Task<IActionResult> GetCompanySetting(RequestCompanySetting request)
        {
            var responce = new ResponseList<CompanySettingInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetCompanySetting(request);
                }
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
        [Authorize]
        [HttpPost("EditCompanySetting")]
        public async Task<IActionResult> EditCompanySetting(CompanySettingInfo request)
        {
            var responce = new ResponseList<CompanySettingInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.EditCompanySetting(request);
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
        [HttpPost("GetCompanyBranch")]
        public async Task<IActionResult> GetCompanyBranch(RequestBase request)
        {
            var responce = new ResponseList<BranchInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetCompanyBranch(request);
                }
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
        [Authorize]
        [HttpPost("GetCompanyTemplate")]
        public async Task<IActionResult> GetCompanyTemplate(RequestBase request)
        {
            var responce = new ResponseList<GetCompanyTemplate>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetCompanyTemplate(request);
                }
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

        [Authorize]
        [HttpPost("EditTemplateSetting")]
        public async Task<IActionResult> EditTemplateSetting(Template request)
        {
            var responce = new ResponseList<GetCompanyTemplate>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.EditTemplate(request);
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
        [HttpPost("GetCompanyLookUp")]
        public async Task<IActionResult> GetCompanyLookUp(RequestLookUp request)
        {
            var responce = new ResponseList<GetLookUpType>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetCompanyLookUp(request);
                }
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

