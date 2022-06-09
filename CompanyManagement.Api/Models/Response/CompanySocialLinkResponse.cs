using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Models.Response
{
    public class CompanySocialLinkResponse
    {             
            public long CompanySocialLinkId { get; set; }
            public long CompanyId { get; set; }
            public bool IsActive { get; set; }
            public string Facebook { get; set; }
            public bool ShowFacebookOnline { get; set; }
            public string Instagram { get; set; }
            public bool ShowInstagramOnline { get; set; }
            public string Twitter { get; set; }
            public bool ShowTwitterOnline { get; set; }
            public string ContactEmail { get; set; }
            public bool ShowContactEmailOnline { get; set; }
            public string ContactPhone { get; set; }
            public bool ShowContactPhoneOnline { get; set; }
            public DateTime CreatedAt { get; set; }
            public Guid CreatedByUserId { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public Guid? UpdatedByUserID { get; set; }
        
    }
}
