using CompanyManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Data
{
    public class CompanyDBContext : DbContext
    {
        public CompanyDBContext(DbContextOptions<CompanyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<CompanySetting> CompanySetting { get; set; }
        public DbSet<MailServer> MailServer { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<CompanySettingInfo> CompanySettingInfo { get; set; }
        public DbSet<GetCompanyTemplate> GetCompanyTemplate { get; set; }
        



    }
}
