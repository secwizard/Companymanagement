using AutoMapper;
using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Response;
using CompanyManagement.Api.Models.Tax;

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
            this.CreateMap<CompanyTemplate, ResponseCompanyTemplate>(MemberList.None)
             .ForMember(s => s.ResponseCompanyTemplateSections, c => c.MapFrom(m => m.CompanyTemplateSections));
            this.CreateMap<FronEndTemplate, ResponseFrontendTemplate>(MemberList.None);

            this.CreateMap<FronEndTemplate, CompanyTemplate>(MemberList.None)
                .ForMember(s => s.CompanyTemplateSections, c => c.MapFrom(m => m.TemplateDefaultSections));
            this.CreateMap<TemplateDefaultSection, CompanyTemplateSection>(MemberList.None);

            this.CreateMap<CompanyTemplateSection, ResponseCompanyTemplateSection>(MemberList.None);

            this.CreateMap<TaxName, TaxNameResponse>(MemberList.None).ReverseMap();
            this.CreateMap<TaxName, TaxNameRequest>(MemberList.None).ReverseMap();
            this.CreateMap<TaxDetails, TaxDetailsGet>(MemberList.None).ReverseMap();
            this.CreateMap<GetTaxDetails, GetTaxDetailsResponse>(MemberList.None).ReverseMap();
        }
    }
}
