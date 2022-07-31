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
            this.CreateMap<CompanyInfo, Company>(MemberList.None)
                .ForMember(s => s.CurrencyMaster, c => c.MapFrom(m => m.Currency));
            this.CreateMap<Company, CompanyInfo>(MemberList.None)
                .ForMember(s => s.Currency, c => c.MapFrom(m => m.CurrencyMaster));
            this.CreateMap<CompanyMailServer, MailServer>(MemberList.None);
            this.CreateMap<MailServer, CompanyMailServer>(MemberList.None);
            this.CreateMap<CompanyTheme, Theme>(MemberList.None);
            this.CreateMap<Theme, CompanyTheme>(MemberList.None);
            this.CreateMap<BranchInfo, Branch>(MemberList.None);
            this.CreateMap<Branch, BranchInfo>(MemberList.None);
            this.CreateMap<ResponseCompanyDtlByIdFrontend, Company>(MemberList.None)
                .ForMember(s => s.CurrencyMaster, c => c.MapFrom(m => m.Currency));
            this.CreateMap<Company, ResponseCompanyDtlByIdFrontend>(MemberList.None)
                .ForMember(s => s.Currency, c => c.MapFrom(m => m.CurrencyMaster));
            this.CreateMap<ThemeData, Theme>(MemberList.None);
            this.CreateMap<Theme, ThemeData>(MemberList.None);
            this.CreateMap<Template, FooterData>(MemberList.None);
            this.CreateMap<FooterData, Template>(MemberList.None);
            this.CreateMap<OnBoardSubscriptions, SubscriptionMaster>(MemberList.None);
            this.CreateMap<OnBoardAddOns, AddOnMaster>(MemberList.None);
            this.CreateMap<SubscriptionMaster, OnBoardSubscriptions>(MemberList.None);
            this.CreateMap<AddOnMaster, OnBoardAddOns>(MemberList.None);
            this.CreateMap<CompanyTemplate, ResponseCompanyTemplate>(MemberList.None)
            .ForMember(s => s.ResponseFontFamily, c => c.MapFrom(m => m.FontFamilyMaster))
             .ForMember(s => s.ResponseCompanyTemplateSections, c => c.MapFrom(m => m.CompanyTemplateSections));
            this.CreateMap<FronEndTemplate, ResponseFrontendTemplate>(MemberList.None);

            this.CreateMap<FronEndTemplate, CompanyTemplate>(MemberList.None)
                .ForMember(s => s.CompanyTemplateSections, c => c.MapFrom(m => m.TemplateDefaultSections));
            this.CreateMap<TemplateDefaultSection, CompanyTemplateSection>(MemberList.None);

            this.CreateMap<CompanyTemplateSection, ResponseCompanyTemplateSection>(MemberList.None)
             .ForPath(s => s.ResponseSectionItemAndImage.SectionImages, c => c.MapFrom(m => m.CompanyTemplateSectionImageMappings))
                .ForPath(s => s.ResponseSectionItemAndImage.SectionItems, c => c.MapFrom(m => m.CompanyTemplateSectionItemMappings));

            this.CreateMap<TaxName, TaxNameResponse>(MemberList.None).ReverseMap();
            this.CreateMap<TaxName, TaxNameRequest>(MemberList.None).ReverseMap();
            this.CreateMap<TaxDetails, TaxDetailsGet>(MemberList.None).ReverseMap();
            this.CreateMap<GetTaxDetails, GetTaxDetailsResponse>(MemberList.None).ReverseMap();
            this.CreateMap<CompanyTemplateSectionItemMapping, ResponseCompanyTemplateSectionItem>(MemberList.None);
            this.CreateMap<CompanyTemplateSectionImageMapping, ResponseCompanyTemplateSectionImage>(MemberList.None);

            this.CreateMap<CurrencyMaster, Currency>(MemberList.None).ReverseMap();

            this.CreateMap<FrontEndTemplateFontFamilyMaster, ResponseFrontEndTemplateFontFamilyMaster>(MemberList.None).ReverseMap();



            this.CreateMap<CompanyTemplateAdmin, ResponseAdminTemplate>(MemberList.None)
           .ForMember(s => s.ResponseFontFamily, c => c.MapFrom(m => m.FontFamilyMaster))
            .ForMember(s => s.ResponseCompanyTemplateSections, c => c.MapFrom(m => m.CompanyTemplateSections));

            this.CreateMap<CompanyTemplateSectionAdmin, ResponseAdminCompanyTemplateSection>(MemberList.None)
            .ForPath(s => s.ResponseSectionItemAndImage.SectionImages, c => c.MapFrom(m => m.CompanyTemplateSectionImageMappings))
               .ForPath(s => s.ResponseSectionItemAndImage.SectionItems, c => c.MapFrom(m => m.CompanyTemplateSectionItemMappings));

            this.CreateMap<CompanyTemplateSectionItemMappingAdmin, ResponseAdminCompanyTemplateSectionItem>(MemberList.None);
            this.CreateMap<CompanyTemplateSectionImageMappingAdmin, ResponseAdminCompanyTemplateSectionImage>(MemberList.None);

            this.CreateMap<CompanyTemplateSection, ResponseAdminCompanyTemplateSection>(MemberList.None)
             .ForPath(s => s.ResponseSectionItemAndImage.SectionImages, c => c.MapFrom(m => m.CompanyTemplateSectionImageMappings))
                .ForPath(s => s.ResponseSectionItemAndImage.SectionItems, c => c.MapFrom(m => m.CompanyTemplateSectionItemMappings));

            this.CreateMap<CompanyTemplateSectionItemMapping, ResponseAdminCompanyTemplateSectionItem>(MemberList.None);
            this.CreateMap<CompanyTemplateSectionImageMapping, ResponseAdminCompanyTemplateSectionImage>(MemberList.None);
        }
    }
}
