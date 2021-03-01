﻿using AutoMapper;
using CompanyManagement.Api.Data;
using CompanyManagement.Api.Mapper;
using CompanyManagement.Api.Models;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public class OnBoardService :IOnBoardService
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IMapper _mapper;

        static OnBoardService()
        {
            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            }).CreateMapper();
        }

        public OnBoardService(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<CompanyInfo> GetCompanyDetails(RequestBase request)
        {
            try
            {
                var res = new CompanyInfo();
                var data = await _context.Company
                    .Where(c => c.CompanyId == request.CompanyId
                    && c.IsActive == true).FirstOrDefaultAsync();

                if (data != null)
                {
                    _mapper.Map(data, res);
                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }
        public async Task<Response<CompanyInfo>> AddCompany(CompanyInfo request)
        {
            var retVal = new Response<CompanyInfo>();
            try
            {
                var res = new CompanyInfo();
                var data = new Company();
                //var data = await _context.Company
                //    .Where(c => c.CompanyId == request.CompanyId
                //    && c.IsActive == true).FirstOrDefaultAsync();

                //if (data != null)
                //{
                //    data = MapCompany(data,request);
                //    data.ModifiedBy = request.CreatedBy;
                //    data.ModifiedDate = DateTime.Now;
                //    _context.Entry(data).State = EntityState.Modified;
                //}
                //else
                //{
                data = MapCompany(data, request);
                data.CreatedBy = request.CreatedBy;
                data.CreatedDate = DateTime.Now;
                _context.Company.Add(data);
                //}
                _context.SaveChanges();
                request.CompanyId = data.CompanyId;
                retVal.Data = request;
                retVal.Message = "OK";
                retVal.Status = true;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                retVal.Message = "ERROR";
                retVal.Status = false;
            }

            return retVal;
        }
        private Company MapCompany(Company preData, CompanyInfo postData)
        {
            if (preData == null)
                preData = new Company();
            preData.Address1 = postData.Address1 == null?"" : postData.Address1;
            preData.Address2 = postData.Address2 == null ? "" : postData.Address2;
            preData.AdminEmail = postData.AdminEmail == null ? "" : postData.AdminEmail;
            preData.AdminPhone = postData.AdminPhone == null ? "" : postData.AdminPhone;
            preData.BusinessType = postData.BusinessType == null ? "" : postData.BusinessType;
            preData.CountryCode = postData.CountryCode == null ? "" : postData.CountryCode;
            preData.CurrencyCode = postData.CurrencyCode == null ? "" : postData.CurrencyCode;
            preData.DistrictCode = postData.DistrictCode == null ? "" : postData.DistrictCode;
            preData.FavIconFileName = postData.FavIconFileName == null ? "" : postData.FavIconFileName;
            preData.GSTNumber = postData.GSTNumber == null ? "" : postData.GSTNumber;
            preData.ImageFilePath = postData.ImageFilePath == null ? "" : postData.ImageFilePath;
            preData.IsActive = postData.IsActive == null ? false : postData.IsActive;
            preData.LoginImageFileName = postData.LoginImageFileName == null ? "" : postData.LoginImageFileName;
            preData.LogoFileName = postData.LogoFileName == null ? "" : postData.LogoFileName;
            preData.Name = postData.Name == null ? "" : postData.Name;
            preData.PanNumber = postData.PanNumber == null ? "" : postData.PanNumber;
            preData.PIN = postData.PIN == null ? "" : postData.PIN;
            preData.PINRequired = postData.PINRequired == null ? false : postData.PINRequired;
            preData.SecondaryEmail = postData.SecondaryEmail == null ? "" : postData.SecondaryEmail;
            preData.ServiceEmail = postData.ServiceEmail == null ? "" : postData.ServiceEmail;
            preData.ServicePhone = postData.ServicePhone == null ? "" : postData.ServicePhone;
            preData.ShortName = postData.ShortName == null ? "" : postData.ShortName;
            preData.StateCode = postData.StateCode == null ? "" : postData.StateCode;
            preData.Website = postData.Website == null ? "" : postData.Website;
            return preData;
        }
    }
}
