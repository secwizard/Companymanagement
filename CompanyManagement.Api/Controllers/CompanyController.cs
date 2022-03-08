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
    public class CompanyController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet]
        public IActionResult TestConnection()
        {
            return Ok("Company API connected.");
        }

        #region ===== Frontend ===== 

        [HttpPost("GetCompanyByUrl")]
        public async Task<IActionResult> GetCompanyByUrl(RequestCompanyUrl request)
        {
            var responce = new Response<ResponseCompanyDtlByIdFrontend>();
            try
            {
                responce.Data = await _companyService.GetCompanyByUrl(request);
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

        [HttpPost("GetCompanyNoAuth")]
        public async Task<IActionResult> GetCompanyNoAuth(RequestBase request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {
                responce.Data = await _companyService.GetCompany(request);
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

        [HttpPost("GetCompanyDtlFrontEnd")]
        public async Task<IActionResult> GetCompanyDtlFrontEnd(RequestBase request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {

                responce.Data = await _companyService.GetCompany(request);
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

        [HttpPost("CheckCompanyUrlAndShortName")]
        public async Task<IActionResult> CheckCompanyUrlAndShortName(RequestCheckCompanyUrlAndShortName request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {

                responce.Data = await _companyService.CheckCompanyUrlAndShortName(request);
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

        [HttpPost("CheckCompanyUrlFrontend")]
        public async Task<IActionResult> CheckCompanyUrlFrontend(RequestCheckCompanyUrlAndShortName request)
        {
            var responce = new Response<CompanyInfo>();
            try
            {

                responce.Data = await _companyService.CheckCompanyUrlFrontend(request);
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

        [HttpPost("GetCompanyDtlByIdFrontend")]
        public async Task<IActionResult> GetCompanyDtlByIdFrontend(RequestBase request)
        {
            var responce = new Response<ResponseCompanyDtlByIdFrontend>();
            try
            {

                responce.Data = await _companyService.GetCompanyDtlByIdFrontend(request);
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

        [HttpPost("GetIsPINRequired")]
        public async Task<IActionResult> GetIsPINRequired(RequestBase request)
        {
            var responce = new Response<bool>();
            try
            {
                responce.Data = await _companyService.GetIsPINRequired(request);
                responce.Status = responce.Data == true;
                responce.Message = responce.Data != true ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(responce);
        }

        [HttpPost("GetCompanyCurrencyCode")]
        public async Task<IActionResult> GetCompanyCurrencyCode(RequestBase request)
        {
            var responce = new Response<string>();
            try
            {
                responce.Data = await _companyService.GetCompanyCurrencyCode(request);
                responce.Status = responce.Data != "";
                responce.Message = responce.Data == "" ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(responce);
        }

        [HttpPost("GetCompanyDetailsForSentMail")]
        public async Task<IActionResult> GetCompanyDetailsForSentMail(RequestBase request)
        {
            var responce = new Response<CompanyDetailsForSentMail>();
            try
            {
                responce.Data = await _companyService.GetCompanyDetailsForSentMail(request);
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

        [HttpPost("GetCompanyTemplateByTypeAndName")]
        public async Task<IActionResult> GetCompanyTemplateByTypeAndName(RequestGetCompanyTemplateByTypeAndName request)
        {
            var responce = new ResponseList<GetCompanyTemplate>();
            try
            {
                responce.Data = await _companyService.GetCompanyTemplateByTypeAndName(request);
                responce.Status = responce.Data != null;
                responce.Message = responce.Data != null ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(responce);
        }

        #endregion


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
                responce.Status = true;
                responce.Message = responce == null ? "Data not found." : string.Empty;
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
                responce.Status = responce != null && responce.Status;
                responce.Message = responce == null ? "Data not found." : string.Empty;
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
                responce.Message = responce.Data != null ? string.Empty : "Data not found.";
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
        [HttpPost("EditThemeSetting")]
        public async Task<IActionResult> EditThemeSetting(GetCompanyTheme request)
        {
            var responce = new ResponseList<GetCompanyTheme>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.EditTheme(request);
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
        [HttpPost("DeleteThemeSetting")]
        public async Task<IActionResult> DeleteThemeSetting(DeleteCompanyTheme request)
        {
            var responce = new ResponseList<GetCompanyTheme>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.DeleteTheme(request);
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
                responce.Status = responce.Data != null;
                responce.Message = responce.Data != null ? string.Empty : "Data not found.";
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
        [HttpPost("DeleteCompanySetting")]
        public async Task<IActionResult> DeleteCompanySetting(DeleteCompanySettings request)
        {
            var responce = new ResponseList<CompanySettingInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.DeleteCompanySetting(request);
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
                responce.Status = responce.Data != null;
                responce.Message = responce.Data != null ? string.Empty : "Data not found.";
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
        [HttpPost("EditBranchSetting")]
        public async Task<IActionResult> EditBranchSetting(Branch request)
        {
            var responce = new ResponseList<BranchInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.EditBranch(request);
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
        [HttpPost("DeleteBranchSetting")]
        public async Task<IActionResult> DeleteBranchSetting(DeleteCompanyBranch request)
        {
            var responce = new ResponseList<BranchInfo>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.DeleteBranch(request);
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
                responce.Status = responce.Data != null;
                responce.Message = responce.Data != null ? string.Empty : "Data not found.";
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
        [HttpPost("DeleteTemplateSetting")]
        public async Task<IActionResult> DeleteTemplateSetting(DeleteCompanyTemplate request)
        {
            var responce = new ResponseList<GetCompanyTemplate>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce = await _companyService.DeleteTemplate(request);
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

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendEmail(RequestSendMail requestSendMail)
        {
            var responce = new ResponseMail
            {
                Status = false
            };
            try
            {
                var compMailServer = await _companyService.GetCompanySmtp(new RequestBase() { CompanyId = requestSendMail.CompanyId });

                if (string.IsNullOrEmpty(requestSendMail.EmailFrom)) requestSendMail.EmailFrom = compMailServer.FromEmailId;

                var notificationMetadata = new NotificationMetadata();
                notificationMetadata.Sender = requestSendMail.EmailFrom;
                notificationMetadata.Reciever = requestSendMail.EmailTo;
                if (compMailServer == null || string.IsNullOrEmpty(compMailServer.SMTPServer))
                {
                    notificationMetadata.SmtpServer = "smtp.gmail.com";
                    notificationMetadata.Port = 465;
                    notificationMetadata.UserName = "wizardcomm.mail@gmail.com";
                    notificationMetadata.Password = "wizard!@#";
                    notificationMetadata.EnableSSL = true;
                }
                else
                {
                    notificationMetadata.SmtpServer = compMailServer.SMTPServer ?? "smtp.zoho.com";
                    notificationMetadata.Port = compMailServer.SMTPPort ?? 465;
                    notificationMetadata.UserName = compMailServer.FromEmailId ?? "do-not-reply@wizardcomm.net";
                    notificationMetadata.Password = compMailServer.FromEmailPwd ?? "Admin@123$";
                    notificationMetadata.EnableSSL = compMailServer.EnableSSL ?? true;
                }

                responce = await _companyService.SendMail(notificationMetadata, requestSendMail);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                responce.Message = ex.Message;
            }
            return Ok(responce);
        }
        [Authorize]
        [HttpPost("EditProductInclusiveOfTax")]
        public async Task<IActionResult> EditProductInclusiveOfTax(RequestProductInclusiveOfTax request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Data = await _companyService.EditProductInclusiveOfTax(request);
                }
                response.Status = response.Data;
                response.Message = !response.Data ? "Error updating data." : string.Empty;
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
        [HttpPost("IsProductInclusiveOfTax")]
        public async Task<IActionResult> IsProductInclusiveOfTax(RequestBase request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _companyService.IsProductInclusiveOfTax(request);
                }
                response.Status = response.Data;
                response.Message = !response.Data ? "Error fetching company tax setting." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }
       
        [HttpPost("SaveNotificationService")]
        public async Task<IActionResult> SaveNotificationService(RequestSaveNotificationServiceDetails request)
        {
            Response<ResponseSaveTwillioNotificationService> response = new Response<ResponseSaveTwillioNotificationService>();
            try
            {
                var result = await _companyService.SaveTwillioNotificationService(request);
                response.Data = result;
                response.Status = result != null;
                response.Message = response.Status ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }
        [HttpPost("GetNotificationServiceService")]
        public async Task<IActionResult> GetNotificationServiceService(RequestGetNotificationSettingsServiceDetails request)
        {
            Response<ResponseGetNotificationServiceDetails> response = new Response<ResponseGetNotificationServiceDetails>();
            try
            {
                var result = await _companyService.GetNotificationServiceService(request);
                response.Data = result;
                response.Status = result != null;
                response.Message = response.Status ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Some error occured!";
                log.Error($"ErrorOn:{DateTime.UtcNow} Message:{ex.Message} InnerException: {ex.InnerException} StackTrace: {ex.StackTrace}");
            }
            return Ok(response);
        }
        #region ========== Zone settings======
        [Authorize]
        [HttpPost("SaveZoneSettings")]
        public async Task<IActionResult> SaveZoneSettings(RequestZoneSetting request)
        {
            var response = new Response<ZoneSetting>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response = await _companyService.SaveZoneSettings(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Error saving zone setting." : string.Empty;
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
        [HttpPost("GetZoneList")]
        public async Task<IActionResult> GetZoneList(RequestZoneSetting request)
        {
            var responce = new ResponseList<ResponseZoneSetting>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    responce.Data = await _companyService.GetZoneList(request);
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
        [HttpPost("GetZoneByZoneId")]
        public async Task<IActionResult> GetZoneByZoneId(RequestZoneSetting request)
        {
            Response<ResponseZoneSetting> response = new Response<ResponseZoneSetting>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _companyService.GetZoneByZoneId(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Status ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }
        
        [HttpPost("GetZoneId")]
        public async Task<IActionResult> GetZoneId(RequestZoneSetting request)
        {
            Response<ResponseZoneId> response = new Response<ResponseZoneId>();
            try
            {
                response.Data = await _companyService.GetZoneId(request);
                response.Status = response.Data != null;
                response.Message = response.Status ? string.Empty : "Data not found.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [HttpGet("GetZonePattern")]
        public async Task<IActionResult> GetZonePattern()
        {
            var responce = new ResponseList<ZonePatternDetails>();
            List<ZonePatternDetails> list = new List<ZonePatternDetails>();
            try
            {
                var getAllZoneSettingPatterns = Utility.ZoneSettingPattern.GetAllZoneSettingPatterns();
                if(getAllZoneSettingPatterns !=null)
                {
                    foreach (var item in getAllZoneSettingPatterns)
                    {
                        ZonePatternDetails eachItem = new ZonePatternDetails();
                        eachItem.PatternId = item.PatternId;
                        eachItem.PatternName = item.PatternName;
                        list.Add(eachItem);
                    }
                }
                responce.Data = list;
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
        #endregion
    }
}

