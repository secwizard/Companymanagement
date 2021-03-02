using AutoMapper;
using CompanyManagement.Api.Models;

namespace CompanyManagement.Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<CompanyInfo, Company>(MemberList.None);
            this.CreateMap<Company, CompanyInfo>(MemberList.None);
            this.CreateMap<CompanyMailServer, MailServer>(MemberList.None);
            this.CreateMap<MailServer, CompanyMailServer>(MemberList.None);
            this.CreateMap<CompanyTheme, Theme>(MemberList.None);
            this.CreateMap<Theme, CompanyTheme>(MemberList.None);
            this.CreateMap<BranchInfo, Branch>(MemberList.None);
            this.CreateMap<Branch, BranchInfo>(MemberList.None);
        }
    }
}
