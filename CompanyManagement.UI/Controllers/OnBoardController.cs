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
    public class OnBoardController : Controller
    {
        private readonly IServiceAPI _restAPI;
        readonly string _BaseUrl = string.Empty;
        private UserToken user = new UserToken();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public OnBoardController(IServiceAPI restAPI,
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
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            user.CompanyId = 0;
            user.Logo = null;
            Session.Set("CompanyConfiguration", user);

            OnBoardProcessinfo onBoardProcessinfo = new OnBoardProcessinfo();
            RequestCompanyInfo company = new RequestCompanyInfo();
            List<ResponseBranchDetails> branch = new List<ResponseBranchDetails>();
            ResponseMailServerDetails mail = new ResponseMailServerDetails();
            List<ResponseCompanySetting> setting = new List<ResponseCompanySetting>();
            List<ResponseCompanyTemplate> tempalte = new List<ResponseCompanyTemplate>();
            List<ResponseThemeDetails> theme = new List<ResponseThemeDetails>();
            OnBoardCompany onCompany = new OnBoardCompany();
            List<OnBoardSubscriptions> subscription = new List<OnBoardSubscriptions>();
            List<OnBoardAddOns> addons = new List<OnBoardAddOns>();
            NewCompanyDetails details = new NewCompanyDetails();
            details.CompanyId = user.CompanyId;
            details.UserId = user.Id;
            var compDtl = await _restAPI.NewCompanyDtl(JsonConvert.SerializeObject(details), user.token);
            var alldtl = JsonConvert.DeserializeObject<Response<CompanyAllDetails>>(compDtl).Data;
            subscription = alldtl.SubscriptionDtl;
            addons = alldtl.AddOnDtl;
            company = alldtl.CompanyDtl;
            onCompany.CompanyInfo = company;
            onCompany.BranchInfo = branch;
            onCompany.CompanySettingInfo = setting;
            onCompany.CompanyTemplate = tempalte;
            onCompany.CompanyTheme = theme;
            onCompany.MailServerInfo = mail;
            onBoardProcessinfo.OnBoardCompanyInfo = new OnBoardCompany();
            OnBoardCompany comp = new OnBoardCompany();
            comp.CompanyInfo = company;
            comp.BranchInfo = branch;
            comp.CompanySettingInfo = setting;
            comp.CompanyTemplate = tempalte;
            comp.CompanyTheme = theme;
            comp.MailServerInfo = mail;
            onBoardProcessinfo.OnBoardCompanyInfo = comp;
            onBoardProcessinfo.OnBoardSubscriptionInfo = new List<OnBoardSubscriptions>();
            onBoardProcessinfo.OnBoardAddOn = new List<OnBoardAddOns>();
            onBoardProcessinfo.OnBoardSubscriptionInfo = subscription;
            onBoardProcessinfo.OnBoardAddOn = addons;



            return View(onBoardProcessinfo);
        }
        [HttpPost]
        public async Task<IActionResult> GetRequiredDetails()
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            ResponseList<LookUpInfo> result = new ResponseList<LookUpInfo>();
            try
            {
                ResponseCompanyId newCompanyDetails = new ResponseCompanyId();
                newCompanyDetails.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.GetRequiredDetails(JsonConvert.SerializeObject(newCompanyDetails), user.token);
                result = JsonConvert.DeserializeObject<ResponseList<LookUpInfo>>(compDtl);

                if (result != null && result.Data != null && result.Status)
                {
                    return Json(result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetNewCompanydetails(NewCompanyDetails newCompanyDetails)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            Response<RequestCompanyInfo> result = new Response<RequestCompanyInfo>();
            try
            {
                //newCompanyDetails.UserId = user.Id;
                //newCompanyDetails.CompanyId = user.CompanyId;
                //var compDtl = await _restAPI.NewCompanyDtl(JsonConvert.SerializeObject(newCompanyDetails), user.token);
                //result = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl);
                //if(result != null && result.Data != null && result.Status)
                //{
                //Session.Set("NewCompanyId", result.Data.CompanyId);
                return PartialView("_Partial_OnBoardCompany");
                //}
                //else
                //{
                //    return Ok("NO");
                //}
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        public async Task<IActionResult> GetSuggestedCompanyId(BusinessType newCompanyDetails)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            Response<ResponseCompanyId> result = new Response<ResponseCompanyId>();
            try
            {
                newCompanyDetails.CompanyId = user.CompanyId;
                var compDtl = await _restAPI.GetSuggestedCompanyId(JsonConvert.SerializeObject(newCompanyDetails), user.token);
                result = JsonConvert.DeserializeObject<Response<ResponseCompanyId>>(compDtl);
                if (result != null && result.Data != null && result.Status)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        public async Task<IActionResult> GetBranchdetails()
        {

            return PartialView("_Partial_OnBoardBranch");
        }
        public async Task<IActionResult> AddCompany(RequestCompanyInfo companyInfo)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            var dt = ViewBag.NewCompanyId;
            Response<RequestCompanyInfo> result = new Response<RequestCompanyInfo>();
            try
            {
                var newCompanyId = Session.Get<long>("NewCompanyId");
                var user = Session.Get<UserToken>("CompanyConfiguration");
                companyInfo.CreatedBy = user.Id;
                var compDtl = await _restAPI.AddCompany(JsonConvert.SerializeObject(companyInfo), user.token);
                result = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl);
                if (result != null && result.Data != null && result.Status)
                {
                    Session.Set("NewCompanyId", result.Data.CompanyId);
                    return PartialView("_Partial_Company", result);
                }
                else
                {
                    return Ok("NO");
                }
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                return Ok("NO");
            }

        }
        [HttpPost]
        public async Task<IActionResult> SaveOnBoardProcess(OnBoardProcessinfo request)
        {
            if (string.IsNullOrEmpty(user?.token)) return Ok("login");
            var responce = new Response<ResponseCompanyId>();
            try
            {
                var result = await _restAPI.SaveOnBoardProcess(JsonConvert.SerializeObject(request), user.token);
                responce = JsonConvert.DeserializeObject<Response<ResponseCompanyId>>(result);
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
