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
using System.Reflection;
using System.Threading.Tasks;
using Wizard.ImageManagement.Models.Response;

namespace CompanyManagement.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceAPI _restAPI;
        readonly string _BaseUrl = string.Empty;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public LoginController(IServiceAPI restAPI,
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

        public async Task<IActionResult> CompanySelection()
        {
            
            var user = Session.Get<UserToken>("CompanyConfiguration");
            var res = await _restAPI.CompanyList(user.token);
            var data = JsonConvert.DeserializeObject<ResponseList<CompanyInfo>>(res);
            return View(data.Data);
        }

        private IActionResult View(ResponseCompanyList responseCompanyList)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> LogVerify(RequestLogin input)
        {
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                if (string.IsNullOrEmpty(user?.token))
                {
                    var data = JsonConvert.SerializeObject(input);
                    var res = await _restAPI.LoginUser(data);
                    var result = JsonConvert.DeserializeObject<Response<UserToken>>(res);
                    if (result.Data != null && result.Status == true)
                    {
                        user = result.Data;
                    }
                    else
                    {
                        input.ErrorMessage = "Either UserName Or Password is Incorrect";
                    }
                }
                else
                {
                    user.CompanyId = input.CompanyId;
                }
                

                //if (user.CompanyId == -1)
                //{
                //    CreateLoginObject(user);
                //    input.ReturnUrl = "Login/CompanySelection";
                //    input.ErrorMessage = "OK";
                //    return Json(input);
                //}

                var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
                var compDtl = await _restAPI.CompanyDtl(JsonConvert.SerializeObject(request), user.token);
                var result2 = JsonConvert.DeserializeObject<Response<ResponseCompanyDtl>>(compDtl);
                if (result2 != null && result2.Status == true)
                {
                    user.ImageFilePath = result2.Data.ImageFilePath;
                    user.Logo = result2.Data.LogoFileName;
                    user.BusinessType = result2.Data.BusinessType;

                    CreateLoginObject(user);
                    input.ReturnUrl = "Company/Index";
                    input.ErrorMessage = "OK";
                }
                else
                {
                    input.ErrorMessage = "Either UserName Or Password is Incorrect";
                }
                
            }
            catch (Exception ex)
            {
                input.ErrorMessage = "Either UserName Or Password is Incorrect";
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
            }
            return Json(input);
        }

        private void CreateLoginObject(UserToken data)
        {
            Session.Set("CompanyConfiguration", data);
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult ConLog(ConLogRequest data)
        {
            try
            {
                log.Info("Message : " + data.Message + " Stack : " + data.Stack);
                return Json("ConLog success");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return Json("ConLog catch");
            }
        }
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
