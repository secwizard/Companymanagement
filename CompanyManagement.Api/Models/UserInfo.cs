using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public long CompanyId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
    }
}
