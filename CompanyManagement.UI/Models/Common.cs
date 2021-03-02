using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public long CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string token { get; set; }
        public string ImageFilePath { get; set; }
        public string Logo { get; set; }
        public string BusinessType { get; set; }
        //public string ErrorMessage { get; set; }
    }

}
