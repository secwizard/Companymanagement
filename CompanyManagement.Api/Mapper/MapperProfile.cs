using AutoMapper;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Response;

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
            this.CreateMap<ResponseCompanyDtlByIdFrontend, Company>(MemberList.None);
            this.CreateMap<Company, ResponseCompanyDtlByIdFrontend>(MemberList.None);
            this.CreateMap<ThemeData, Theme>(MemberList.None);
            this.CreateMap<Theme, ThemeData>(MemberList.None);
            this.CreateMap<Template, FooterData>(MemberList.None);
            this.CreateMap<FooterData, Template>(MemberList.None);
            this.CreateMap<OnBoardSubscriptions, SubscriptionMaster>(MemberList.None);
            this.CreateMap<OnBoardAddOns, AddOnMaster>(MemberList.None);
            this.CreateMap<SubscriptionMaster, OnBoardSubscriptions > (MemberList.None);
            this.CreateMap<AddOnMaster, OnBoardAddOns > (MemberList.None);
        }
    }
}
