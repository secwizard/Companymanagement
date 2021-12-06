using CompanyManagement.Api.Helpers;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Tax;
using CompanyManagement.Api.Service;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ITaxService _taxService;

        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [Authorize]
        [HttpPost("GetTaxName")]
        public async Task<IActionResult> GetTaxName(RequestBase request)
        {
            var response = new Response<TaxNameResponse>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _taxService.GetTaxName(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Data not found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("SaveTaxDetails")]
        public async Task<IActionResult> SaveTaxDetails(TaxDetailsSet request)
        {
            var response = new Response<TaxDetailsGet>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                request.UserId = user.UserId;
                request.CompanyId = (int)user.CompanyId;

                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _taxService.SaveTaxDetails(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "Tax creation error." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("GetTaxDetails")]
        public async Task<IActionResult> GetTaxDetails(RequestBase request)
        {
            var response = new Response<List<TaxDetailsGet>>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _taxService.GetTaxDetails(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "No data found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("GetSpecificTaxDetails")]
        public async Task<IActionResult> GetSpecificTaxDetails(SingleTaxDetailsRequest request)
        {
            var response = new Response<TaxDetailsGet>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _taxService.GetSpecificTaxDetails(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "No data found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("UpdateTaxName")]
        public async Task<IActionResult> UpdateTaxName(TaxNameRequest request)
        {
            var response = new Response<bool>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    request.UserId = user.UserId;
                    response.Data = await _taxService.UpdateTaxName(request);
                }
                response.Status = response.Data;
                response.Message = !response.Data ? "Error updating data." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }


        #region Item

        [Authorize]
        [HttpPost("GetAllTax")]
        public async Task<IActionResult> GetAllTax(CompanyTaxDetailsRequest request)
        {
            var response = new Response<CompanyTaxDetailsResponse>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _taxService.GetAllTax(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null || response.Data.AllTaxes == null ? "Error updating data." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }


        [HttpPost("GetTaxDetailsWithCompanySetting")]
        public async Task<IActionResult> GetTaxDetailsWithCompanySetting(CompanyTaxDetailsListRequest request)
        {
            var response = new Response<List<GetTaxDetailsResponse>>();
            try
            {
                var user = (UserInfo)HttpContext.Items["User"];
                if (user?.CompanyId == request.CompanyId || user?.CompanyId == -1)
                {
                    response.Data = await _taxService.GetTaxDetailsWithCompanySetting(request);
                }
                response.Status = response.Data != null;
                response.Message = response.Data == null ? "No data found." : string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return Ok(response);
        }

        #endregion

    }
}