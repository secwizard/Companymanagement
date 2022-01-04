using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CompanyManagement.Api.Models.Tax
{
    public class TaxDetailsBase
    {
        public int TaxDetailsId { get; set; }
        public string TaxName { get; set; }
        public decimal Tax1Percentage { get; set; }
        public decimal Tax2Percentage { get; set; }
        public decimal Tax3Percentage { get; set; }
        public decimal Tax4Percentage { get; set; }
        public decimal Tax5Percentage { get; set; }
        public bool IsDefault { get; set; }
        public int CompanyId { get; set; }
        public decimal Total { get; set; }
    }
    public class TaxDetailsGet : TaxDetailsBase
    {
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? CreatedOnUTC { get; set; }
        public DateTime? UpdatedOnUTC { get; set; }
    }
    public class TaxDetailsSet : TaxDetailsBase
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
    public class SingleTaxDetailsRequest
    {
        public int CompanyId { get; set; }
        public int TaxDetailsId { get; set; }
    }

    public class CompanyTaxDetailsRequest
    {
        public int CompanyId { get; set; }
        public int TaxId { get; set; }
    }
    public class CompanyTaxDetailsResponse
    {
        public List<TaxDetailsBase> AllTaxes { get; set; }
        public TaxDetailsBase SelectedTax { get; set; }
    }


    public class CompanyTaxDetailsListRequest
    {
        public int CompanyId { get; set; }
        public List<int> TaxId { get; set; }
    }
    public class GetTaxDetailsResponse
    {
        public int TaxDetailsId { get; set; }
        public string TaxName { get; set; }
        public string Tax1Name { get; set; }
        public decimal Tax1Percentage { get; set; }
        public string Tax2Name { get; set; }
        public decimal Tax2Percentage { get; set; }
        public string Tax3Name { get; set; }
        public decimal Tax3Percentage { get; set; }
        public string Tax4Name { get; set; }
        public decimal Tax4Percentage { get; set; }
        public string Tax5Name { get; set; }
        public decimal Tax5Percentage { get; set; }
        public bool IsDefault { get; set; }
        public decimal Total { get; set; }
        public bool IsAllProductInclusiveOfTax { get; set; }
    }
}