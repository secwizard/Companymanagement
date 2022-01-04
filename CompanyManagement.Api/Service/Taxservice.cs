using AutoMapper;
using CompanyManagement.Api.Data;
using CompanyManagement.Api.Mapper;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Tax;
using log4net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public class TaxService : ITaxService
    {
        private readonly CompanyDBContext _context;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IMapper _mapper;

        static TaxService()
        {
            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MapperProfile>();
            }).CreateMapper();
        }

        public TaxService(CompanyDBContext context)
        {
            _context = context;
        }

        public async Task<TaxNameResponse> GetTaxName(RequestBase request)
        {
            try
            {
                var data = await _context.TaxName
                    .Where(c => c.CompanyId == request.CompanyId).FirstOrDefaultAsync();

                if (data != null)
                {
                    return _mapper.Map<TaxName, TaxNameResponse>(data);
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<TaxDetailsGet> SaveTaxDetails(TaxDetailsSet request)
        {
            try
            {
                var taxset = new TaxDetails
                {
                    CompanyId = request.CompanyId,
                    IsDefault = request.IsDefault,
                    Tax1Percentage = request.Tax1Percentage,
                    Tax2Percentage = request.Tax2Percentage,
                    Tax3Percentage = request.Tax3Percentage,
                    Tax4Percentage = request.Tax4Percentage,
                    Tax5Percentage = request.Tax5Percentage,
                    TaxDetailsId = request.TaxDetailsId,
                    TaxName = request.TaxName,
                    Total = request.Total
                };

                if (request.TaxDetailsId == 0)
                {
                    taxset.CreatedById = request.UserId;
                    taxset.CreatedOnUTC = DateTime.UtcNow;
                    await _context.TaxDetails.AddAsync(taxset);
                }
                else
                {
                    taxset.UpdatedById = request.UserId;
                    taxset.UpdatedOnUTC = DateTime.UtcNow;
                    _context.Entry(taxset).State = EntityState.Modified;
                    _context.Entry(taxset).Property(x => x.CreatedById).IsModified = false;
                    _context.Entry(taxset).Property(x => x.CreatedOnUTC).IsModified = false;
                }
                await _context.SaveChangesAsync();
                return _mapper.Map<TaxDetails, TaxDetailsGet>(taxset);
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<List<TaxDetailsGet>> GetTaxDetails(RequestBase request)
        {
            try
            {
                var taxdetails = _context.TaxDetails.Where(k => k.CompanyId == request.CompanyId).ToList();
                return await Task.FromResult(
                    _mapper.Map<List<TaxDetails>, List<TaxDetailsGet>>(taxdetails));
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<TaxDetailsGet> GetSpecificTaxDetails(SingleTaxDetailsRequest request)
        {
            try
            {
                var taxdetails = await _context.TaxDetails.FirstOrDefaultAsync(k => k.CompanyId == request.CompanyId && k.TaxDetailsId == request.TaxDetailsId);
                return
                    _mapper.Map<TaxDetails, TaxDetailsGet>(taxdetails);
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public Task<bool> CreateTaxName(RequestBase requestBase)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateTaxName(TaxNameRequest taxNameRequest)
        {
            try
            {
                StringBuilder fieldToUpdate = new StringBuilder();

                fieldToUpdate.Append("update TaxName set ");

                if (!string.IsNullOrWhiteSpace(taxNameRequest.Tax1Name))
                    fieldToUpdate.Append("Tax1Name='" + taxNameRequest.Tax1Name + "',");
                if (!string.IsNullOrWhiteSpace(taxNameRequest.Tax2Name))
                    fieldToUpdate.Append("Tax2Name='" + taxNameRequest.Tax2Name + "',");
                if (!string.IsNullOrWhiteSpace(taxNameRequest.Tax3Name))
                    fieldToUpdate.Append("Tax3Name='" + taxNameRequest.Tax3Name + "',");
                if (!string.IsNullOrWhiteSpace(taxNameRequest.Tax4Name))
                    fieldToUpdate.Append("Tax4Name='" + taxNameRequest.Tax4Name + "',");
                if (!string.IsNullOrWhiteSpace(taxNameRequest.Tax5Name))
                    fieldToUpdate.Append("Tax5Name='" + taxNameRequest.Tax5Name + "',");

                fieldToUpdate.Append($"UpdatedBy='{taxNameRequest.UserId}',UpdatedOnUTC=@updatedOn ");
                fieldToUpdate.Append($"where CompanyId={taxNameRequest.CompanyId}");

                var res = await _context.Database.ExecuteSqlRawAsync(fieldToUpdate.ToString(), new[]
                {
                    new SqlParameter("@updatedOn",DateTime.UtcNow)
                });
                if (res > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                return false;
            }
        }

        #region Item

        public async Task<CompanyTaxDetailsResponse> GetAllTax(CompanyTaxDetailsRequest request)
        {
            try
            {
                var response = new CompanyTaxDetailsResponse();
                var taxdetails = await _context.TaxDetails.Where(k => k.CompanyId == request.CompanyId).ToListAsync();
                if (taxdetails != null && taxdetails.Count > 0)
                {
                    response.AllTaxes = new List<TaxDetailsBase>();
                    foreach (var details in taxdetails.OrderByDescending(x => x.IsDefault))
                    {
                        response.AllTaxes.Add(_mapper.Map<TaxDetails, TaxDetailsGet>(details));
                    }
                    if (request.TaxId != 0)
                    {
                        response.SelectedTax = response.AllTaxes.Where(x => x.TaxDetailsId == request.TaxId).FirstOrDefault();
                    }
                }
                return response;

            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        public async Task<List<GetTaxDetailsResponse>> GetTaxDetailsWithCompanySetting(CompanyTaxDetailsListRequest request)
        {
            try
            {
                List<GetTaxDetailsResponse> listData = new List<GetTaxDetailsResponse>();
                var inclisiveAllTaxes = false;
                var taxDetailsIds = "";
                foreach (var td in request.TaxId)
                {

                        taxDetailsIds +=  td + "," ;

                }
                taxDetailsIds = taxDetailsIds.Remove(taxDetailsIds.Length - 1);
                var parms = new SqlParameter[]
                 {
                    new SqlParameter("@CompanyId", request.CompanyId),
                    new SqlParameter("@TaxDetailsIds", taxDetailsIds)
                 };

                string sqlText = $"EXECUTE dbo.[GetTaxDetails] @CompanyId , @TaxDetailsIds";
                var taxdetails = await _context.GetTaxDetails.FromSqlRaw(sqlText, parms).ToListAsync();
                var inclusiveOfAllTaxes = await _context.CompanySetting.FirstOrDefaultAsync(k => k.CompanyId == request.CompanyId && k.IsActive == true);
                if (inclusiveOfAllTaxes != null)
                    inclisiveAllTaxes = inclusiveOfAllTaxes.IsAllProductInclusiveOfTax;
                if (taxdetails != null && taxdetails.Count > 0)
                {
                    foreach (var tax in taxdetails)
                    {
                        var retData = _mapper.Map<GetTaxDetails, GetTaxDetailsResponse>(tax);
                        retData.IsAllProductInclusiveOfTax = inclisiveAllTaxes;
                        listData.Add(retData);
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
                throw;
            }
        }

        #endregion
    }
}