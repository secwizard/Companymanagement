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
            var user = Session.Get<UserToken>("CompanyConfiguration");
            var request = new RequestCompanyDtl() { CompanyId = user.CompanyId };
            var compDtl = await _restAPI.CompanyDtl(JsonConvert.SerializeObject(request), user.token);
            var result2 = JsonConvert.DeserializeObject<Response<RequestCompanyInfo>>(compDtl).Data;
            return PartialView("_Partial_OnBoardCompany", result2);
        }
        public async Task<IActionResult> GetBranchdetails()
        {
            return PartialView("_Partial_OnBoardBranch");
        }
    }
    
}
