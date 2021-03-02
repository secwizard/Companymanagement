using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Models.Request
{
    public class ApiRequest
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string ReturnUrl { get; set; }
    }
}
