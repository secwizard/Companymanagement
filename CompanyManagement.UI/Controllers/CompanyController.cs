﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Controllers
{
    public class CompanyController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
