using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Domain.CurrencyBoard.Entities;
using HyBrForex.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IAuthenticatedUserService authenticatedUser) : DbContext(options)
{
    public DbSet<CommonTypeMsater> CommonTypeMsater { get; set; }
    public DbSet<CountryMaster> CountryMaster { get; set; }
    public DbSet<Stages> Stages { get; set; }
    public DbSet<DomainStages> DomainStages { get; set; }
    public DbSet<StateMaster> StateMaster { get; set; }
    public DbSet<LeadContact> LeadContact { get; set; }
    public DbSet<Lead>Lead { get; set; }
    public DbSet<TaskMaster> TaskMaster { get; set; }
    public DbSet<LeadProperties> LeadProperties { get; set; }
    public DbSet<ActivityLog> ActivityLog { get; set; }
    public DbSet<SubDomain>SubDomain { get; set; }
    public DbSet<HoliDaysLead> HoliDaysLead { get; set; }
    public DbSet<LeadProperyDefinition> LeadProperyDefinition { get; set; }
    public DbSet<LeadProperyValue>LeadProperyValues { get; set; }
    public DbSet<InBondCall> InBondCall { get; set; }
    public DbSet<LeadDocument> LeadDocument { get; set; }
    public DbSet<BonvoiceCallLog> BonvoiceCallLog { get; set; }





    //public DbSet<CommonMsater> CommonMsater { get; set; }

    //public DbSet<CurrencyMaster> CurrencyMaster { get; set; }
    //public DbSet<BannerDtl> BannerDtl { get; set; }
    //public DbSet<CurrencyBoardDtl> CurrencyBoardDtl { get; set; }
    //public DbSet<CurrencyConversionDtl> CurrencyConversionDtl { get; set; }
    //public DbSet<NotificationDtl> NotificationDtl { get; set; }

    //public DbSet<EmployeeMaster> EmployeeMaster { get; set; }



    //public DbSet<StateMaster> StateMaster { get; set; }

    //public DbSet<Bankmasters> Bankmaster { get; set; }
    //public DbSet<Bankdetails> Bankdetails { get; set; }



    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ChangeTracker.ApplyAuditing(authenticatedUser);

        return base.SaveChangesAsync(cancellationToken);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseLazyLoadingProxies();
    //}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Application");
        this.ConfigureDecimalProperties(builder);

       



        //builder.Entity<EmployeeMaster>(entity => { entity.ToTable("EmployeeMaster"); });
        //builder.Entity<EmployeeMaster>().HasKey(u => u.Id);


      

        builder.Entity<CommonMsater>()
.HasOne(s => s.CommonTypeMsater)
.WithMany()
.HasForeignKey(s => s.CommonTypeId);

        builder.Entity<LeadContact>()
.HasOne(s => s.Gender)
.WithMany()
.HasForeignKey(s => s.GenderId);

        builder.Entity<LeadContact>()
.HasOne(s => s.Country)
.WithMany()
.HasForeignKey(s => s.CountryId);

        builder.Entity<LeadContact>()
.HasOne(s => s.State)
.WithMany()
.HasForeignKey(s => s.StateId);

      

        builder.Entity<DomainStages>()
            .HasOne(s=>s.Stages)
            .WithMany()
            .HasForeignKey(s => s.StagesId);

        builder.Entity<Lead>().HasOne(s=>s.LeadContact)
            .WithMany()
            .HasForeignKey(s => s.LeadContactId);

        builder.Entity<Lead>().HasOne(s=>s.LeadSource)
            .WithMany()
            .HasForeignKey(s=>s.LeadSourceId);

        builder.Entity<Lead>()
            .HasOne(s=>s.LeadStatus)
            .WithMany()
            .HasForeignKey(s=>s.LeadStatusId);
       

        builder.Entity<StateMaster>()
            .HasOne(s=>s.Country)
            .WithMany()
            .HasForeignKey(s=>s.CountryId);

        builder.Entity<TaskMaster>()
.HasOne(s => s.DomainStages)
.WithMany()
.HasForeignKey(s => s.DomainStagesId);

        builder.Entity<LeadProperyValue>()
.HasOne(s => s.PropertyDefinition)
.WithMany()
.HasForeignKey(s => s.PropertyDefinitionId);

        builder.Entity<DomainStages>()
.HasOne(s => s.Parent)
.WithMany()
.HasForeignKey(s => s.ParentId);


        //        builder.Entity<TaskMaster>()
        //.HasOne(s => s.Lead)
        //.WithMany()
        //.HasForeignKey(s => s.LeadId);

        //builder.Entity<Bankmasters>(entity => { entity.ToTable("Bankmaster"); });
        //builder.Entity<Bankmasters>().HasKey(u => u.Id);
        //builder.Entity<Bankdetails>(entity => { entity.ToTable("Bankdetails"); });
        //builder.Entity<Bankdetails>().HasKey(u => u.Id);

    }
}