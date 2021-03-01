using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Controllers
{
    public class OnBoardController : Controller
    {
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
    }
    
}
