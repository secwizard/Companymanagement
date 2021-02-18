﻿using CompanyManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface ICompanyService
    {
        Task<CompanyInfo> GetCompany(RequestBase request);
    }
}