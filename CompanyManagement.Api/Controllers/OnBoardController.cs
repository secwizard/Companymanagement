using CompanyManagement.Api.Data;
using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CompanyManagement.Api.Service;

namespace CompanyManagement.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OnBoardController : ControllerBase
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IOnBoardService _onBoardService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OnBoardController(CompanyDBContext context
            ,IOnBoardService onBoardService
            , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _onBoardService = onBoardService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpPost("GetCompanyDetails")]
        public async Task<IActionResult> GetCompanyDetails(NewCompanyDetails request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {
                //var user = (UserInfo)HttpContext.Items["User"];
                //if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                //{
                    responce.Data = await _onBoardService.GetCompanyDetails(request);
                //}
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
        [HttpPost("GetRequiredDetails")]
        public async Task<IActionResult> GetRequiredDetails(RequestBase request)
        {
            var responce = new ResponseList<LookUpInfo>();
            try
            {
                //var user = (UserInfo)HttpContext.Items["User"];
                //if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                //{
                responce.Data = await _onBoardService.GetRequiredDetails(request);
                //}
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
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany(CompanyInfo request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                //if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                //{
                    responce = await _onBoardService.AddCompany(request);
                //}
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
        [HttpPost("GetSuggestedCompanyId")]
        public async Task<IActionResult> GetSuggestedCompanyId(BusinessType request)
        {
            var responce = new Response<ResponseCompanyId>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _onBoardService.GetSuggestedCompanyId(request.Type);
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
        [HttpPost("SaveOnBoardProcess")]
        public async Task<IActionResult> SaveOnBoardProcess(OnBoardProcessinfo request)
        {
            var responce = new Response<ResponseCompanyId>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == -1)
                {
                    responce.Data = await _onBoardService.SaveOnBoardProcess(request,user);
                }
                if(responce != null && responce.Data != null && responce.Data.CompanyId>0)
                {
                    responce.Status = true;
                }
                else
                {
                    responce.Status = false;
                }
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
    }
}
