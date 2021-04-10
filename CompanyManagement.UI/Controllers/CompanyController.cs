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
        private UserToken user = new UserToken();
        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public CompanyController(IServiceAPI restAPI,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _restAPI = restAPI;
            _httpContextAccessor = httpContextAccessor;
            _BaseUrl = configuration.GetSection("AppSettings").GetValue<string>("BaseUrl");
            GetSessionValue();
        }
        private void GetSessionValue()
        {
            user = Session.Get<UserToken>("CompanyConfiguration");
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetCompanydetails()
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            Response<RequestCompanyInfo> result = new Response<RequestCompanyInfo>();
            try
            {
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.CompanyDtl(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl);
                if (result != null && result.Data != null && result.Status)
                {
                    return PartialView("_Partial_Company", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditCompany(RequestCompanyInfo companyInfo)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            Response<RequestCompanyInfo> result = new Response<RequestCompanyInfo>();
            try
            {
                companyInfo.CreatedBy = user.Id;
                var compDtl = await _restAPI.EditCompany(JsonConvert.SerializeObject(companyInfo), user.token);
                result = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl);
                if (result != null && result.Data != null && result.Status)
                {
                    return PartialView("_Partial_Company", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        public async Task<IActionResult> GetMailDetails()
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            Response<ResponseMailServerDetails> result = new Response<ResponseMailServerDetails>();
            try
            {
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetMailDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<Response<ResponseMailServerDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_MailServer", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditSTMPServer(ResponseMailServerDetails mailInfo)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            Response<ResponseMailServerDetails> result = new Response<ResponseMailServerDetails>();
            try
            {
                mailInfo.CreatedBy = user.Id;
                mailInfo.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.EditSTMPServer(JsonConvert.SerializeObject(mailInfo), user.token);
                result = JsonConvert.DeserializeObject<Response<ResponseMailServerDetails>>(compDtl);
                if (result != null  && result.Status)
                {
                    return PartialView("_Partial_MailServer", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }

        public async Task<IActionResult> GetCompanySettingsDetails()
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseCompanySetting> result = new ResponseList<ResponseCompanySetting>();
            try
            {
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetCompanySettingsDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanySetting>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_PartialCompanySetting", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditCompanySetting(ResponseCompanySetting companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseCompanySetting> result = new ResponseList<ResponseCompanySetting>();
            try
            {
                companysetting.CreatedBy = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.EditCompanySetting(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanySetting>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_PartialCompanySetting", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteCompanySetting(DeleteCompanySettings companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseCompanySetting> result = new ResponseList<ResponseCompanySetting>();
            try
            {
                companysetting.UserId = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.DeleteCompanySetting(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanySetting>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_PartialCompanySetting", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        [HttpPost]
        public async Task<IActionResult> EditTemplate(ResponseCompanyTemplate companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseCompanyTemplate> result = new ResponseList<ResponseCompanyTemplate>();
            try
            {
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
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteTemplate(DeleteCompanyTemplate companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseCompanyTemplate> result = new ResponseList<ResponseCompanyTemplate>();
            try
            {
                companysetting.UserId = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.DeleteTemplate(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanyTemplate>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Template", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        public async Task<IActionResult> GetTemplateDetails()
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseCompanyTemplate> result = new ResponseList<ResponseCompanyTemplate>();
            try
            {
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetTemplateDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseCompanyTemplate>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Template", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }
        }
        public async Task<IActionResult> GetThemeDetails()
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseThemeDetails> result = new ResponseList<ResponseThemeDetails>();
            try
            {
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetThemeDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseThemeDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Theme", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditTheme(ResponseThemeDetails companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseThemeDetails> result = new ResponseList<ResponseThemeDetails>();
            try
            {
                companysetting.CreatedBy = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.EditTheme(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseThemeDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Theme", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteTheme(DeleteCompanyTheme companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseThemeDetails> result = new ResponseList<ResponseThemeDetails>();
            try
            {
                companysetting.UserId = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.DeleteTheme(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseThemeDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Theme", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }

        public async Task<IActionResult> GetBranchDetails()
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseBranchDetails> result = new ResponseList<ResponseBranchDetails>();
            try
            {
                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.GetBranchDetails(JsonConvert.SerializeObject(request), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseBranchDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Branch", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Worng";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditBranch(ResponseBranchDetails companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseBranchDetails> result = new ResponseList<ResponseBranchDetails>();
            try
            {
                companysetting.CreatedBy = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.EditBranch(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseBranchDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Branch", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteBranch(DeleteCompanyBranch companysetting)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<ResponseBranchDetails> result = new ResponseList<ResponseBranchDetails>();
            try
            {
                companysetting.UserId = user.Id;
                companysetting.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.DeleteBranch(JsonConvert.SerializeObject(companysetting), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<ResponseBranchDetails>>(compDtl);
                if (result != null && result.Status)
                {
                    return PartialView("_Partial_Branch", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wrong";
                result.Status = false;
                log.Info("***EditCompanySetting*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
    }

}

