using AutoMapper;
using CompanyManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<CompanyInfo, Company>(MemberList.None);
            this.CreateMap<Company, CompanyInfo>(MemberList.None);
        }
    }
}
