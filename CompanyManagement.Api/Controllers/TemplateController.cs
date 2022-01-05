using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Response;
using CompanyManagement.Api.Service;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [Authorize]
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
                response.Message = response.Data == null ? "Error creating template." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("EditCompanyTemplate")]
        public async Task<ActionResult<ResponseCompanyTemplate>> EditCompanyTemplate(RequestEditCompanyTemplate request)
        {
            var response = new Response<ResponseCompanyTemplate>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Data = await _temllateService.EditCompanyTemplate(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Error updating template." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("GetCompanyTemplateById")]
        public async Task<ActionResult<ResponseCompanyTemplate>> GetCompanyTemplateById(RequestGetCompanyTemplateById request)
        {
            var response = new Response<ResponseCompanyTemplate>();
            try
            {
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

        [Authorize]
        [HttpPost("GetCompanyTemplateList")]
        public async Task<ActionResult<List<ResponseCompanyTemplate>>> GetCompanyTemplateList(RequestBase request)
        {
            var response = new ResponseList<ResponseCompanyTemplate>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _temllateService.GetCompanyTemplates(request);
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

        //Updating the master data of sections//
        [Authorize]
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

        [Authorize]
        [HttpPost("EditCompanyTemplateSectionOrder")]
        public async Task<ActionResult> EditCompanyTemplateSectionOrder(RequestEditCompanyTemplateSectionOrder request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Status = await _temllateService.EditCompanyTemplateSectionOrder(request);
                    response.Message = response.Status == false ? "Error in ordering." : string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("AddSectionItem")]
        public async Task<ActionResult<ResponseSectionItemAndImage>> AddSectionItem(RequestAddSectionItem request)
        {
            var response = new Response<ResponseSectionItemAndImage>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Data = await _temllateService.AddSectionItem(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Item can't be added." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("EditSectionItemOrder")]
        public async Task<ActionResult> EditSectionItemOrder(RequestEditSectionItemOrder request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Status = await _temllateService.EditSectionItemOrder(request);
                    response.Message = response.Status == false ? "Error in ordering." : string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("EditSectionImageOrder")]
        public async Task<ActionResult> EditSectionImageOrder(RequestEditSectionImageOrder request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Status = await _temllateService.EditSectionImageOrder(request);
                    response.Message = response.Status == false ? "Error in ordering." : string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("ChangeCompanyTemplateDefault")]
        public async Task<ActionResult> ChangeCompanyTemplateDefault(RequestChangeCompanyTemplateDefault request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Status = await _temllateService.ChangeCompanyTemplateDefault(request);
                    response.Message = response.Status == false ? "Error updating default." : string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("ChangeCompanyTemplateB2C")]
        public async Task<ActionResult> ChangeCompanyTemplateB2C(RequestChangeCompanyTemplateB2C request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Status = await _temllateService.ChangeCompanyTemplateB2C(request);
                    response.Message = response.Status == false ? "Error updating B2C." : string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        //-------------------------------------------------------------//
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

        [HttpPost("DeleteCompanyTemplateSectionItem")]
        public async Task<ActionResult> DeleteCompanyTemplateSectionItem(RequestDeleteCompanyTemplateSectionItem request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Status = await _temllateService.DeleteCompanyTemplateSectionItem(request);
                    response.Message = response.Status == false ? "Error deleting." : string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [HttpPost("DeleteCompanyTemplateSectionImage")]
        public async Task<ActionResult> DeleteCompanyTemplateSectionImage(RequestDeleteCompanyTemplateSectionImage request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Status = await _temllateService.DeleteCompanyTemplateSectionImage(request);
                    response.Message = response.Status == false ? "Error deleting." : string.Empty;
                }
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