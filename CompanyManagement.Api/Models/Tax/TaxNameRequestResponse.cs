using System;
using System.Text.Json.Serialization;

namespace CompanyManagement.Api.Models.Tax
{
    public class TaxNameBase
    {
        public string Tax1Name { get; set; }
        public string Tax2Name { get; set; }
        public string Tax3Name { get; set; }
        public string Tax4Name { get; set; }
        public string Tax5Name { get; set; }
        public int CompanyId { get; set; }
    }
    public class TaxNameResponse : TaxNameBase
    {
        public int TaxNameId { get; set; }
    }
    public class TaxNameRequest : TaxNameBase {

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}