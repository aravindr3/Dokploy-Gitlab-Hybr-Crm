using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HyBrForex.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class tenants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "ApplicationFeatures",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FeatureName = table.Column<string>(type: "text", nullable: true),
                    Create = table.Column<bool>(type: "boolean", nullable: false),
                    Edit = table.Column<bool>(type: "boolean", nullable: false),
                    Delete = table.Column<bool>(type: "boolean", nullable: false),
                    View = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationLoginInfo",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    SessionToken = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    DeviceInfo = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    LoginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLoginInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationReports",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReportFeatureName = table.Column<string>(type: "text", nullable: true),
                    Csv = table.Column<bool>(type: "boolean", nullable: false),
                    Excel = table.Column<bool>(type: "boolean", nullable: false),
                    PDF = table.Column<bool>(type: "boolean", nullable: false),
                    Print = table.Column<bool>(type: "boolean", nullable: false),
                    Branch = table.Column<bool>(type: "boolean", nullable: false),
                    Currency = table.Column<bool>(type: "boolean", nullable: false),
                    Transaction = table.Column<bool>(type: "boolean", nullable: false),
                    ReportOption = table.Column<bool>(type: "boolean", nullable: false),
                    DateRange = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeCharts = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizedDealerCategory",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DealerCategory = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedDealerCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchMaster",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false),
                    CorporateCode = table.Column<string>(type: "text", nullable: false),
                    CorporateName = table.Column<string>(type: "text", nullable: false),
                    ROCode = table.Column<string>(type: "text", nullable: false),
                    ROName = table.Column<string>(type: "text", nullable: false),
                    BranchCode = table.Column<string>(type: "text", nullable: false),
                    BranchName = table.Column<string>(type: "text", nullable: false),
                    FACode = table.Column<string>(type: "text", nullable: true),
                    RBILicNo = table.Column<string>(type: "text", nullable: true),
                    BranchHeadsId = table.Column<string>(type: "text", nullable: true),
                    Place = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Pin = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Zone = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Fax = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    ServiceTaxNo = table.Column<string>(type: "text", nullable: true),
                    CIN_No = table.Column<string>(type: "text", nullable: true),
                    PAN = table.Column<string>(type: "text", nullable: true),
                    GSTIN = table.Column<string>(type: "text", nullable: true),
                    ISDNo = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchUser",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BranchId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "featureRoleMappings",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FeatureId = table.Column<string>(type: "text", nullable: true),
                    FunctionalId = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_featureRoleMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "featureRoles",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    FeatureId = table.Column<string>(type: "text", nullable: true),
                    Assign = table.Column<bool>(type: "boolean", nullable: false),
                    Create = table.Column<bool>(type: "boolean", nullable: false),
                    Edit = table.Column<bool>(type: "boolean", nullable: false),
                    Delete = table.Column<bool>(type: "boolean", nullable: false),
                    View = table.Column<bool>(type: "boolean", nullable: false),
                    Print = table.Column<bool>(type: "boolean", nullable: false),
                    Approval = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_featureRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PageTitle = table.Column<string>(type: "text", nullable: true),
                    FeedbackType = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalMasters",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FunctionalName = table.Column<string>(type: "text", nullable: true),
                    RouterName = table.Column<string>(type: "text", nullable: true),
                    RouterDescription = table.Column<string>(type: "text", nullable: true),
                    RouterLink = table.Column<string>(type: "text", nullable: true),
                    SequenceId = table.Column<int>(type: "integer", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<string>(type: "text", nullable: true),
                    isHeader = table.Column<bool>(type: "boolean", nullable: false),
                    isSubHeader = table.Column<bool>(type: "boolean", nullable: false),
                    Orderby = table.Column<int>(type: "integer", nullable: false),
                    ChildRouterLinks = table.Column<string[]>(type: "text[]", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalRoleMappings",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FunctionalId = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalRoleMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reportFeatures",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    ReportId = table.Column<string>(type: "text", nullable: true),
                    Assign = table.Column<bool>(type: "boolean", nullable: false),
                    Csv = table.Column<bool>(type: "boolean", nullable: false),
                    Excel = table.Column<bool>(type: "boolean", nullable: false),
                    PDF = table.Column<bool>(type: "boolean", nullable: false),
                    Print = table.Column<bool>(type: "boolean", nullable: false),
                    Branch = table.Column<bool>(type: "boolean", nullable: false),
                    Currency = table.Column<bool>(type: "boolean", nullable: false),
                    Transaction = table.Column<bool>(type: "boolean", nullable: false),
                    ReportOption = table.Column<bool>(type: "boolean", nullable: false),
                    DateRange = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeCharts = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reportMappings",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReportId = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reportRoleMappings",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReportFeatureID = table.Column<string>(type: "text", nullable: true),
                    ReportID = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportRoleMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Query = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Orderby = table.Column<int>(type: "integer", nullable: false),
                    SequenceId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReportID = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Query = table.Column<string>(type: "text", nullable: true),
                    Align = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AuthorizedDealerId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    ConnectionString = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LegalName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TaxId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Address2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TwoFactorSecretKey = table.Column<string>(type: "text", nullable: true),
                    IsTwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResetPasswordExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationFeatures",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ApplicationLoginInfo",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ApplicationReports",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AuthorizedDealerCategory",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "BranchMaster",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "BranchUser",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "featureRoleMappings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "featureRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "feedbacks",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "FunctionalMasters",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "FunctionalRoleMappings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "notifications",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "reportFeatures",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "reportMappings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "reportRoleMappings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ReportTypes",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");
        }
    }
}
