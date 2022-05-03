using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Response;
using CompanyManagement.Api.Models.Tax;
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
        public DbSet<GetLookUpType> GetLookUpType { get; set; }
        public DbSet<GetCompanyTheme> GetCompanyTheme { get; set; }
        public DbSet<GetSuggestedCompanyId> GetSuggestedCompanyId { get; set; }
        public DbSet<SubscriptionMaster> SubscriptionMaster { get; set; }
        public DbSet<AddOnMaster> AddOnMaster { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<AddOns> AddOns { get; set; }
        public DbSet<CompanyDetailsForSentMail> CompanyDetailsForSentMail { get; set; }
        public DbSet<GetTemplate> GetTemplate { get; set; }
        public DbSet<CompanyTemplateSection> CompanyTemplateSection { get; set; }
        public DbSet<CompanyTemplateSectionImageMapping> CompanyTemplateSectionImageMapping { get; set; }
        public DbSet<GetTemplateBySectionId> GetTemplateBySectionId { get; set; }
        public DbSet<CompanyTemplate> CompanyTemplate { get; set; }
        public DbSet<FronEndTemplate> FronEndTemplate { get; set; }
        public DbSet<TemplateDefaultSection> TemplateDefaultSection { get; set; }
        public DbSet<TaxName> TaxName { get; set; }
        public DbSet<TaxDetails> TaxDetails { get; set; }
        public DbSet<CompanyTemplateSectionItemMapping> CompanyTemplateSectionItemMapping { get; set; }
        public DbSet<CurrencyMaster> CurrencyMaster { get; set; }

        public DbSet<FrontEndTemplateFontFamilyMaster> FrontEndTemplateFontFamilyMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TemplateDefaultSection>()
                        .HasOne<FronEndTemplate>(t => t.Template)
                        .WithMany(t => t.TemplateDefaultSections)
                        .HasForeignKey(t => t.TemplateId);

            modelBuilder.Entity<CompanyTemplateSection>()
                       .HasOne<CompanyTemplate>(t => t.CompanyTemplate)
                       .WithMany(t => t.CompanyTemplateSections)
                       .HasForeignKey(t => t.CompanyTemplateId);

            modelBuilder.Entity<CompanyTemplateSectionItemMapping>()
                       .HasOne<CompanyTemplateSection>(t => t.CompanyTemplateSection)
                       .WithMany(t => t.CompanyTemplateSectionItemMappings)
                       .HasForeignKey(t => t.CompanyTemplateSectionId);

        }


        public DbSet<GetTaxDetails> GetTaxDetails { get; set; }
        public DbSet<ResponseSaveTwillioNotificationService> AddEditTwillioNotificationService { get; set; }
        public DbSet<ResponseGetNotificationServiceDetails> ResponseGetNotificationServiceDetails { get; set; }
        public DbSet<CompanySocialLink> CompanySocialLink { get; set; }

    }
}