using CompanyManagement.Api.Data;
using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Request;
using CompanyManagement.Api.Models.Response;
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
    public class TemplateController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ITemplateService _temllateService;

        public TemplateController(ITemplateService templateService)
        {
            _temllateService = templateService;
        }

        [HttpPost("GetTemplate")]
        public async Task<IActionResult> GetTemplate(RequestCompanyTemplate request)
        {
            var responce = new Response<FrontEndTemplate>();
            try
            {
                responce.Data = await _temllateService.GetTemplate(request);
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

        [HttpPost("GetItemsFromSectionId")]
        public async Task<IActionResult> GetItemsFromSectionId(RequestItemBySectionId request)
        {
            var responce = new Response<List<ItemIdBySection>>();
            try
            {
                responce.Data = await _temllateService.GetTemplateBySectionID(request);
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

        [HttpPost("GetCompanyTemplate")]
        public async Task<ActionResult<List<ResponseCompanyTemplate>>> GetCompanyTemplate(RequestBase request)
        {
            var response = new ResponseList<ResponseCompanyTemplate>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _temllateService.GetCompanyTemplate(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data != null ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [HttpGet("GetFrontendTemplate")]
        public async Task<ActionResult<List<ResponseFrontendTemplate>>> GetFrontendTemplate()
        {
            var response = new ResponseList<ResponseFrontendTemplate>();
            try
            {
                response.Data = await _temllateService.GetFrontendTemplate();
                response.Status = response.Data != null;
                response.Message = response.Data != null ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [HttpPost("AddCompanyTemplate")]
        public async Task<ActionResult<ResponseCompanyTemplate>> AddCompanyTemplate(RequestAddCompanyTemplate request)
        {
            var response = new Response<ResponseCompanyTemplate>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Data = await _temllateService.AddCompanyTemplate(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Route("[action]/{templateId}/{companyId}")]
        [HttpGet]
        public async Task<ActionResult<ResponseCompanyTemplate>> GetCompanyTemplateById(int templateId, int companyId)
        {
            var response = new Response<ResponseCompanyTemplate>();
            try
            {
                var request = new RequestAddCompanyTemplate { TemplateId = templateId, CompanyId = companyId };
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Data = await _temllateService.GetCompnayTemplateById(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [HttpPost("EditCompanyTemplateSection")]
        public async Task<ActionResult<ResponseCompanyTemplateSection>> EditCompanyTemplateSection(RequestEditCompanyTemplateSection request)
        {
            var response = new Response<ResponseCompanyTemplateSection>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Data = await _temllateService.EditCompanyTemplateSection(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [HttpPost("EditCompanyTemplateSectionOrder")]
        public async Task<ActionResult> EditCompanyTemplateSectionOrder(RequestEditCompanyTemplateSectionOrder request)
        {
            var response = new Response<ResponseCompanyTemplateSection>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Status = await _temllateService.EditCompanyTemplateSectionOrder(request);
                    response.Message = response.Status == false ? "Data not found." : string.Empty;
                }
                //response.Status = response.Data != null;
                //response.Message = response.Data == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }
    }
}