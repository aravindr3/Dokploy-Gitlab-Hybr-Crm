using System;
using System.Reflection.Emit;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using HyBrForex.Infrastructure.Identity.Extensions;
using LoginApi.Application.Interfaces;
using HyBrForex.Infrastructure.Identity.Models;
using HyBrForex.Application.Interfaces;
using HyBrCRM.Infrastructure.Identity.Models;

namespace LoginApi.Infrastructure.Identity.Contexts
{
    public class IdentityContext(DbContextOptions<IdentityContext> options, IAuthenticatedUserService authenticatedUser) : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        public DbSet<ApplicationTenant> Tenants { get; set; }
        public DbSet<BranchMaster> BranchMaster { get; set; }
        public DbSet<BranchUser> BranchUser { get; set; }
        public DbSet<ApplicationLoginInfo> ApplicationLoginInfo { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<FunctionalMaster> FunctionalMasters { get; set; }
        public DbSet<ApplicationFeature> ApplicationFeatures { get; set; }
        public DbSet<FeatureRoleMapping> featureRoleMappings { get; set; }
        public DbSet<FunctionalRoleMapping> FunctionalRoleMappings { get; set; }
        public DbSet<FeatureRole> featureRoles { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<Notifications> notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<ReportFeature> reportFeatures { get; set; }
        public DbSet<ReportRoleMapping> reportRoleMappings { get; set; }
        public DbSet<ApplicationReport> ApplicationReports { get; set; }
        public DbSet<ReportMapping> reportMappings { get; set; }
        public DbSet<ApplicationDomain>Domain {  get; set; }
        public DbSet<Vertical> Vertical { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.ApplyAuditing(authenticatedUser);

            return base.SaveChangesAsync(cancellationToken);
        }
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<Ulid>()
        //        .AreUnicode(false)
        //        .AreFixedLength(true)
        //        .HaveMaxLength(26)
        //        .HaveConversion<UlidToStringConverter>();
                

        //}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");

            //  builder.Entity<AuthorizedDealerCategory>(entity =>
            //  {
            //      entity.ToTable(name: "AuthorizedDealerCategory");
            //  });

            //  builder.Entity<AuthorizedDealerCategory>()
            //   .Property(role => role.Id)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<AuthorizedDealerCategory>()
            //    .Property(role => role.CreatedBy)
            //    .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<AuthorizedDealerCategory>()
            //   .Property(role => role.LastModifiedBy)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));


            //  builder.Entity<ApplicationTenant>(entity =>
            //  {
            //      entity.ToTable(name: "Tenants");
            //  });

            //  builder.Entity<ApplicationTenant>()
            //    .Property(role => role.Id)
            //    .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<ApplicationTenant>()
            //    .Property(role => role.CreatedBy)
            //    .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<ApplicationTenant>()
            //   .Property(role => role.LastModifiedBy)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<ApplicationTenant>()
            //   .Property(role => role.AuthorizedDealerId)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));

            //  builder.Entity<BranchMaster>(entity =>
            //  {
            //      entity.ToTable(name: "BranchMaster");
            //  });

            //  builder.Entity<BranchMaster>()
            //   .Property(role => role.Id)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<BranchMaster>()
            //    .Property(role => role.CreatedBy)
            //    .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<BranchMaster>()
            //   .Property(role => role.LastModifiedBy)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<BranchMaster>()
            //  .Property(role => role.TenantId)
            //  .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));


            //  builder.Entity<ApplicationUser>(entity =>
            //  {
            //      entity.ToTable(name: "User");
            //  });

            //  builder.Entity<ApplicationUser>()
            //     .HasKey(u => u.Id);

            // // builder.Entity<ApplicationUser>()
            // //.Property(role => role.Id)
            // //.HasConversion(v => Ulid.NewUlid().ToString(), v => (v));

            //  builder.Entity<ApplicationUser>()
            //    .Property(role => role.TenantId)
            //     .HasConversion(v => Ulid.NewUlid().ToString(),v => (v));





            //  builder.Entity<BranchUser>(entity =>
            //  {
            //      entity.ToTable(name: "BranchUser");
            //  });

            //  builder.Entity<BranchUser>()
            //   .Property(role => role.Id)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<BranchUser>()
            //   .Property(role => role.BranchId)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //              builder.Entity<BranchUser>()
            //   .Property(role => role.UserId)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<BranchUser>()
            //    .Property(role => role.CreatedBy)
            //    .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));
            //  builder.Entity<BranchUser>()
            //   .Property(role => role.LastModifiedBy)
            //   .HasConversion(v => Ulid.NewUlid().ToString(), v => (v));






            //  builder.Entity<ApplicationRole>(entity =>
            //  {
            //      entity.ToTable(name: "Role");
            //  });

            //  builder.Entity<ApplicationRole>()
            //.Property(role => role.Id)
            //.HasConversion(v => Ulid.NewUlid().ToString(), v => (v));


            //  builder.Entity<IdentityUserRole<string>>(entity =>
            //  {
            //      entity.ToTable("UserRoles");
            //  });



            //  builder.Entity<IdentityUserClaim<string>>(entity =>
            //  {
            //      entity.ToTable("UserClaims");
            //  });

            //  builder.Entity<IdentityUserLogin<string>>(entity =>
            //  {
            //      entity.ToTable("UserLogins");
            //  });

            //  builder.Entity<IdentityRoleClaim<string>>(entity =>
            //  {
            //      entity.ToTable("RoleClaims");

            //  });

            //  builder.Entity<IdentityUserToken<string>>(entity =>
            //  {
            //      entity.ToTable("UserTokens");
            //  });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<ApplicationUser>().HasKey(u => u.Id);


            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
