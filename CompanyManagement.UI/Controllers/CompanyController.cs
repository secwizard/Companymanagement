using CompanyManagement.UI.Models;
using CompanyManagement.UI.Models.Request;
using CompanyManagement.UI.Models.Request.Login;
using CompanyManagement.UI.Models.Response;
using CompanyManagement.UI.Services;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Wizard.ImageManagement.Models.Response;

namespace CompanyManagement.UI.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IServiceAPI _restAPI;
        readonly string _BaseUrl = string.Empty;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public CompanyController(IServiceAPI restAPI,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _restAPI = restAPI;
            _httpContextAccessor = httpContextAccessor;
            _BaseUrl = configuration.GetSection("AppSettings").GetValue<string>("BaseUrl");

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetCompanydetails()
        {
            Response<RequestCompanyInfo> result = new Response<RequestCompanyInfo>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.CompanyDtl(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl);
            }
            catch(Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
            }
            return PartialView("_Partial_Company", result);
        }
        [HttpPost]
        public async Task<IActionResult> EditCompany(RequestCompanyInfo companyInfo)
        {
            Response<RequestCompanyInfo> result = new Response<RequestCompanyInfo>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                companyInfo.CreatedBy = user.Id;
                var compDtl = await _restAPI.EditCompany(JsonConvert.SerializeObject(companyInfo), user.token);
                result = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl);
                if (result != null && result.Data != null && result.Status)
                {
                    return PartialView("_Partial_Company", result);
                }
                else
                {
                    return Json("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Json("NO");
            }
            
        }
        public async Task<IActionResult> GetMailDetails()
        {
            Response<ResponseMailServerDetails> result = new Response<ResponseMailServerDetails>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetMailDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<Response<ResponseMailServerDetails>>(compDtl);
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
            }
            return PartialView("_Partial_MailServer",result);
        }
        [HttpPost]
        public async Task<IActionResult> EditSTMPServer(ResponseMailServerDetails mailInfo)
        {
            Response<ResponseMailServerDetails> result = new Response<ResponseMailServerDetails>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                mailInfo.CreatedBy = user.Id;
                var compDtl = await _restAPI.EditSTMPServer(JsonConvert.SerializeObject(mailInfo), user.token);
                result = JsonConvert.DeserializeObject<Response<ResponseMailServerDetails>>(compDtl);
                if(result != null && result.Data != null && result.Status)
                {
                    return PartialView("_Partial_MailServer", result);
                }
                else
                {
                    return Json("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Json("NO");
            }
            
        }
        public async Task<IActionResult> GetCompanySettingsDetails()
        {
            ResponseList<ResponseCompanySetting> result = new ResponseList<ResponseCompanySetting>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetCompanySettingsDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanySetting>>(compDtl);
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
            }
            return PartialView("_PartialCompanySetting", result);
        }
        [HttpPost]
        public async Task<IActionResult> EditCompanySetting(ResponseCompanySetting companysetting)
        {
            ResponseList<ResponseCompanySetting> result = new ResponseList<ResponseCompanySetting>();
            try
            {
                
                    var user = Session.Get<UserToken>("CompanyConfiguration");
                    companysetting.CreatedBy = user.Id;
                    companysetting.CompanyId = user.CompanyId;
                    var compDtl = await _restAPI.EditCompanySetting(JsonConvert.SerializeObject(companysetting), user.token);
                    result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanySetting>>(compDtl);
                    if(result != null && result.Status)
                    {
                        return PartialView("_PartialCompanySetting", result);
                    }
                    else
                    {
                        return Json("NO");
                    }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Json("NO");
            }
            
        }
        public async Task<IActionResult> EditTemplate(ResponseCompanyTemplate companysetting)
        {
            ResponseList<ResponseCompanyTemplate> result = new ResponseList<ResponseCompanyTemplate>();
            try
            {

                var user = Session.Get<UserToken>("CompanyConfiguration");
                companysetting.CreatedBy = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.EditTemplate(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanyTemplate>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Template", result);
                }
                else
                {
                    return Json("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Json("NO");
            }

        }
        public async Task<IActionResult> GetTemplateDetails()
        {
            ResponseList<ResponseCompanyTemplate> result = new ResponseList<ResponseCompanyTemplate>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetTemplateDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanyTemplate>>(compDtl);
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
            }
            return PartialView("_Partial_Template", result);
        }
        public async Task<IActionResult> GetThemeDetails()
        {
            ResponseList<ResponseThemeDetails> result = new ResponseList<ResponseThemeDetails>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetThemeDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseThemeDetails>>(compDtl);
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
            }
            return PartialView("_Partial_Theme", result);
        }
        public async Task<IActionResult> EditTheme(ResponseThemeDetails companysetting)
        {
            ResponseList<ResponseThemeDetails> result = new ResponseList<ResponseThemeDetails>();
            try
            {

                var user = Session.Get<UserToken>("CompanyConfiguration");
                companysetting.CreatedBy = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.EditTheme(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseThemeDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Template", result);
                }
                else
                {
                    return Json("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Json("NO");
            }

        }
    }
    
}

