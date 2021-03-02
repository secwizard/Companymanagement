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
    public class OnBoardController : Controller
    {
        private readonly IServiceAPI _restAPI;
        readonly string _BaseUrl = string.Empty;

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

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetCompanydetails()
        {
            return PartialView("_Partial_OnBoardCompany");
        }
        public async Task<IActionResult> GetBranchdetails()
        {
            return PartialView("_Partial_OnBoardBranch");
        }
        public async Task<IActionResult> AddCompany(RequestCompanyInfo companyInfo)
        {
            Response<RequestCompanyInfo> result = new Response<RequestCompanyInfo>();
            try
            {
                var user = Session.Get<UserToken>("CompanyConfiguration");
                companyInfo.CreatedBy = user.Id;
                var compDtl = await _restAPI.AddCompany(JsonConvert.SerializeObject(companyInfo), user.token);
                result = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl);
            }
            catch (Exception ex)
            {
                result.Message = "Either UserName Or Password is Incorrect";
                result.Status = false;
                log.Info("***LogVerify*** Date : " + DateTime.UtcNow + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
            }
            return PartialView("_Partial_Company", result);
        }
    }
    
}
