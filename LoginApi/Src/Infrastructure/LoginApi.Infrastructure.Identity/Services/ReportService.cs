using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.Report.Request;
using HyBrForex.Application.DTOs.Report.Response;
using HyBrForex.Application.DTOs.Role.Requests;
using HyBrForex.Application.DTOs.Role.Responses;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Infrastructure.Identity.Models;
using HyBrForex.Application.Wrappers;
using LoginApi.Infrastructure.Identity.Contexts;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class ReportService(IdentityContext identityContext, IMapper mapper) : IReportService
    {
        public async Task<BaseResult<ReportResponse>> CreateReportAsync(ReportRequest request)
        {
            try
            {
                // Check if report with the same name exists
                var existingReport = await identityContext.Reports
                    .FirstOrDefaultAsync(r => r.Name == request.Name);

                string reportId;

                if (existingReport != null)
                {
                    reportId = existingReport.Id;

                    // Check if the same type already exists for this report
                    bool reportTypeExists = await identityContext.ReportTypes
                        .AnyAsync(rt => rt.ReportID == reportId && rt.Type == request.Type);

                    if (reportTypeExists)
                    {
                        return new Error(ErrorCode.AlreadyExists, "A report with this name and type already exists.");
                    }

                    // Add new type for existing report
                    identityContext.ReportTypes.Add(new ReportType
                    {
                        Id = Ulid.NewUlid().ToString(),
                        ReportID = reportId,
                        Type = request.Type,
                        Query = request.Query,
                        Align = request.Align
                    });
                }
                else
                {
                    // New report
                    reportId = Ulid.NewUlid().ToString();

                    identityContext.Reports.Add(new Report
                    {
                        Id = reportId,
                        Name = request.Name,
                        ParentId = request.ParentId,
                        Orderby = request.Orderby,
                        SequenceId = request.SequenceId,
                        Category = request.Category,// Optional: You might not need this here
                    });

                    identityContext.ReportTypes.Add(new ReportType
                    {
                        Id = Ulid.NewUlid().ToString(),
                        ReportID = reportId,
                        Type = request.Type,
                        Query = request.Query,
                        Align = request.Align
                    });
                }

                await identityContext.SaveChangesAsync();

                return new ReportResponse
                {
                    Id = reportId,
                    Name = request.Name,
                    Query = request.Query,
                    ParentId = request.ParentId,
                    Orderby = request.Orderby,
                    SequenceId = request.SequenceId,
                    Type = request.Type,
                    Category = request.Category,
                };
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return new Error(ErrorCode.BadRequest, "An error occurred while creating the report.");
            }
        }


        public async Task<BaseResult<ReportResponse>> UpdateReportAsync(string id, ReportRequest request)
        {
            try
            {
                var report = await identityContext.Reports
                       .FirstOrDefaultAsync(r => r.Id == id);

                if (report == null)
                {
                    return new Error(ErrorCode.NotFound, "Report not found.");
                }

                // Normalize and check if report type exists
                var typeToFind = string.IsNullOrWhiteSpace(request.Type) ? null : request.Type.Trim();

                var reportType = await identityContext.ReportTypes
                    .FirstOrDefaultAsync(rt => rt.ReportID == id && rt.Type == typeToFind);

                if (reportType == null)
                {
                    return new Error(ErrorCode.NotFound, "The specified report type was not found for this report.");
                }

                // Update Report fields (do NOT convert Name to uppercase)
                report.Name = request.Name;
                report.ParentId = request.ParentId;
                report.Orderby = request.Orderby;
                report.SequenceId = request.SequenceId;

                // Update ReportType fields
                reportType.Query = request.Query;
                reportType.Align = request.Align;

                await identityContext.SaveChangesAsync();

                return new ReportResponse
                {
                    Id = report.Id,
                    Name = report.Name,
                    Query = reportType.Query,
                    ParentId = report.ParentId,
                    Orderby = report.Orderby,
                    SequenceId = report.SequenceId,
                    Type = reportType.Type,
                    Category = request.Category // Optional
                };
            }
            catch (Exception ex)
            {
                // TODO: Add logging
                return new Error(ErrorCode.BadRequest, "An error occurred while updating the report.");
            }


        }


        public async Task<BaseResult<bool>> DeleteReportAsync(string id)
        {
            try
            {
                var report = await identityContext.Reports.FindAsync(id) ?? throw new KeyNotFoundException("Report not found");
                identityContext.Reports.Remove(report);
                await identityContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while deleting the report.");
            }
        }

        public async Task<BaseResult<ReportResponse?>> GetByIdAsync(string id)
        {
            try
            {
                var report = await identityContext.Reports.FindAsync(id);
                if (report == null) return null;

                return new ReportResponse
                {
                    Id = report.Id,
                    Name = report.Name ?? string.Empty,
                    Query = report.Query,
                    ParentId = report.ParentId,

                };
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while fetching the report by ID.");
            }
        }

        public async Task<BaseResult<IEnumerable<ReportResponse>>> GetAllAsync()
        {
            try
            {
                var report = await identityContext.Reports
                    .Select(report => new ReportResponse
                    {
                        Id = report.Id,
                        Name = report.Name ?? string.Empty,
                        ParentId = report.ParentId,
                        Query = report.Query,
                    })
                    .ToListAsync();

                return report;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while fetching all reports.");
            }
        }
        public async Task<BaseResult<ReportFeatureResponse>> CreateAsync(ReportFeatureRequest request)
        {
            try
            {
                // Check if a feature with the same name already exists
                var existingFeature = await identityContext.ApplicationReports
                    .FirstOrDefaultAsync(f => f.ReportFeatureName == request.ReportFeaturName);

                if (existingFeature != null)
                {
                    return new Error(ErrorCode.AlreadyExists, "Report feature with the same name already exists.");
                }

                var feature = mapper.Map<ApplicationReport>(request);
                feature.Id = Ulid.NewUlid().ToString();

                identityContext.ApplicationReports.Add(feature);
                await identityContext.SaveChangesAsync();

                if (request.RoleIds?.Any() == true)
                {
                    var roleMappings = request.RoleIds.Select(roleId => new ReportRoleMapping
                    {
                        Id = Ulid.NewUlid().ToString(),
                        RoleId = roleId,
                        ReportID = feature.Id,
                    }).ToList();

                    identityContext.reportRoleMappings.AddRange(roleMappings);
                    await identityContext.SaveChangesAsync();
                }

                return mapper.Map<ReportFeatureResponse>(feature);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while creating Feature...");
            }
        }


        public async Task<BaseResult<ReportFeatureResponse>> UpdateAsync(string id, ReportFeatureRequest request)
        {
            try
            {
                var feature = await identityContext.ApplicationReports.FindAsync(id);
                if (feature == null)
                    return new Error(ErrorCode.NotFound, " Feature not found...");

                mapper.Map(request, feature);
                await identityContext.SaveChangesAsync();

                var existingRoleMappings = identityContext.reportRoleMappings
                    .Where(mapping => mapping.ReportID == feature.Id).ToList();

                identityContext.reportRoleMappings.RemoveRange(existingRoleMappings);
                await identityContext.SaveChangesAsync();

                if (request.RoleIds?.Any() == true)
                {
                    var roleMappings = request.RoleIds.Select(roleId => new ReportRoleMapping
                    {
                        Id = Ulid.NewUlid().ToString(),
                        ReportID = feature.Id,
                        RoleId = roleId
                    }).ToList();

                    identityContext.reportRoleMappings.AddRange(roleMappings);
                    await identityContext.SaveChangesAsync();
                }

                return mapper.Map<ReportFeatureResponse>(feature);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while updating  Feature...");
            }
        }


        public async Task<BaseResult<bool>> DeleteAsync(string id)
        {
            try
            {
                var feature = await identityContext.ApplicationReports
                    .FirstOrDefaultAsync(fm => fm.Id == id);

                if (feature == null)
                {
                    return new Error(ErrorCode.NotFound, " Feature not found.");
                }

                // Remove related role mappings first
                var roleMappings = await identityContext.reportRoleMappings
                    .Where(fr => fr.ReportID == id)
                    .ToListAsync();

                if (roleMappings.Any())
                {
                    identityContext.reportRoleMappings.RemoveRange(roleMappings);
                    await identityContext.SaveChangesAsync();
                }

                // Remove the functional master
                identityContext.ApplicationReports.Remove(feature);
                await identityContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while deleting  Feature...");
            }
        }


        public async Task<BaseResult<List<ReportFeatureResponse>>> GetAllFeatureAsync()
        {
            try
            {
                var features = await identityContext.ApplicationReports.ToListAsync();

                var responses = features.Select(fm => new ReportFeatureResponse
                {
                    Id = fm.Id,
                    ReportFeaturName = fm.ReportFeatureName,
                    Csv = fm.Csv,
                    Excel = fm.Excel,
                    PDF = fm.PDF,
                    Print = fm.Print,
                    Branch = fm.Branch,
                    Currency = fm.Currency,
                    Transaction = fm.Transaction,
                    DateRange = fm.DateRange,
                    IncludeCharts = fm.IncludeCharts,
                    ReportOption = fm.ReportOption,
                    ReportId = identityContext.reportRoleMappings
                       .Where(fr => fr.ReportID == fm.Id)
                       .Select(fr => fr.ReportID)
                       .FirstOrDefault(),
                    RoleIds = identityContext.reportRoleMappings
                        .Where(fr => fr.ReportID == fm.Id)
                        .Select(fr => fr.RoleId)
                        .ToList()
                }).ToList();

                return responses;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving  Feature...");
            }
        }

        public async Task<BaseResult<ReportFeatureResponse>> GetByIdFeatureAsync(string id)
        {
            try
            {
                var feature = await identityContext.ApplicationFeatures.FindAsync(id);
                if (feature == null)
                    return new Error(ErrorCode.NotFound, "Functional master not found...");

                var response = mapper.Map<ReportFeatureResponse>(feature);

                if (await identityContext.reportRoleMappings.AnyAsync(fr => fr.ReportID == id))
                {
                    var functional = await identityContext.reportRoleMappings.Where(fr => fr.ReportID == id).FirstOrDefaultAsync();
                    response.ReportId = functional?.ReportID;
                    response.RoleIds = await identityContext.reportRoleMappings
                        .Where(fr => fr.ReportID == id)
                    .Select(fr => fr.RoleId)
                        .ToListAsync();
                }

                return mapper.Map<ReportFeatureResponse>(response);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving Feature...");
            }
        }
        public async Task<BaseResult<ReportRoleResponse>> GetFeaturePermissionsByRoleIdAsync(string roleId)
        {
            var role = await identityContext.Roles.FindAsync(roleId);
            if (role == null)
            {
                return new Error(ErrorCode.NotFound, "Role not found");
            }

            var permissions = identityContext.reportFeatures
                .Where(fp => fp.RoleId == roleId)
                .Select(fp => new ReportPermissionDto
                {
                    ReportId = fp.ReportId,
                    Assign = fp.Assign,
                    Csv = fp.Csv,
                    Excel = fp.Excel,
                    PDF = fp.PDF,
                    Print = fp.Print,
                    Branch = fp.Branch,
                    Currency = fp.Currency,
                    Transaction = fp.Transaction,
                    DateRange = fp.DateRange,
                    IncludeCharts = fp.IncludeCharts,
                    ReportOption = fp.ReportOption,
                }).ToList();

            return BaseResult<ReportRoleResponse>.Ok(new ReportRoleResponse
            {
                RoleId = roleId,
                Permissions = permissions
            });
        }
        public async Task<BaseResult<ReportRoleResponse>> GetFeaturePermissionsByRoleIdCategoryAsync(string roleId, string category)
        {
            var role = await identityContext.Roles.FindAsync(roleId);
            if (role == null)
            {
                return new Error(ErrorCode.NotFound, "Role not found");
            }
            // Get list of report IDs that match the given category
            var matchingReportIds = await identityContext.Reports
                .Where(rt => rt.Category == category)
                .Select(rt => rt.Id)
                .ToListAsync();

            var permissions = identityContext.reportFeatures
                .Where(fp => fp.RoleId == roleId && matchingReportIds.Contains(fp.ReportId))
                .Select(fp => new ReportPermissionDto
                {
                    ReportId = fp.ReportId,
                    Assign = fp.Assign,
                    Csv = fp.Csv,
                    Excel = fp.Excel,
                    PDF = fp.PDF,
                    Print = fp.Print,
                    Branch = fp.Branch,
                    Currency = fp.Currency,
                    Transaction = fp.Transaction,
                    DateRange = fp.DateRange,
                    IncludeCharts = fp.IncludeCharts,
                    ReportOption = fp.ReportOption,
                }).ToList();

            return BaseResult<ReportRoleResponse>.Ok(new ReportRoleResponse
            {
                RoleId = roleId,
                Permissions = permissions
            });
        }

        public async Task<BaseResult<string>> CreateFeaturePermissionsAsync(ReportRoleMappingRequest request)
        {
            var role = await identityContext.Roles.FindAsync(request.RoleId);
            if (role == null)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving Functional Masters...");
            }
            var existingPermissions = identityContext.reportFeatures
    .Where(fp => fp.RoleId == request.RoleId)
    .ToList();

            var existingMappings = identityContext.reportMappings
                .Where(f => f.RoleId == request.RoleId)
                .ToList();

            foreach (var permission in request.Permissions)
            {
                var existingPermission = existingPermissions.FirstOrDefault(x => x.ReportId == permission.ReportId);
                if (existingPermission != null)
                {
                   
                        // Update
                        existingPermission.Assign = permission.Assign;
                        existingPermission.Csv = permission.Csv;
                        existingPermission.Excel = permission.Excel;
                        existingPermission.PDF = permission.PDF;
                        existingPermission.Print = permission.Print;
                        existingPermission.Branch = permission.Branch;
                        existingPermission.Currency = permission.Currency;
                        existingPermission.Transaction = permission.Transaction;
                        existingPermission.DateRange = permission.DateRange;
                        existingPermission.IncludeCharts = permission.IncludeCharts;
                        existingPermission.ReportOption = permission.ReportOption;
                    identityContext.reportFeatures.Update(existingPermission);
                    await identityContext.SaveChangesAsync();

                }
                else
                {
                    // Insert
                    var newFeature = new ReportFeature
                    {
                        Id = Ulid.NewUlid().ToString(),
                        RoleId = request.RoleId,
                        ReportId = permission.ReportId,
                        Assign = permission.Assign,
                        Csv = permission.Csv,
                        Excel = permission.Excel,
                        PDF = permission.PDF,
                        Print = permission.Print,
                        Branch = permission.Branch,
                        Currency = permission.Currency,
                        Transaction = permission.Transaction,
                        DateRange = permission.DateRange,
                        IncludeCharts = permission.IncludeCharts,
                        ReportOption = permission.ReportOption,
                    };
                    identityContext.reportFeatures.Add(newFeature);
                    await identityContext.SaveChangesAsync();
                }

                var existingMapping = existingMappings.FirstOrDefault(m => m.ReportId == permission.ReportId);
                if (existingMapping == null)
                {
                    var newMapping = new ReportMapping
                    {
                        Id = Ulid.NewUlid().ToString(),
                        ReportId = permission.ReportId,
                        RoleId = request.RoleId
                    };
                    identityContext.reportMappings.Add(newMapping);
                }
            }

            await identityContext.SaveChangesAsync();

            return BaseResult<string>.Ok("Successfully Saved");

        }

        public async Task<BaseResult<RoleTyprReportResponse>> GetReportPermissionsByRoleIdAsync(string roleId)
        {
            var role = await identityContext.Roles.FindAsync(roleId);
            if (role == null)
            {
                return new Error(ErrorCode.NotFound, "Role not found");
            }

            var permissions = await identityContext.reportFeatures
                .Where(fp => fp.RoleId == roleId)
                .Select(fp => new ReportTypeDto
                {
                    ReportId = fp.ReportId,
                    Assign = fp.Assign,
                    Csv = fp.Csv,
                    Excel = fp.Excel,
                    PDF = fp.PDF,
                    Print = fp.Print,
                    Branch = fp.Branch,
                    Currency = fp.Currency,
                    Transaction = fp.Transaction,
                    DateRange = fp.DateRange,
                    IncludeCharts = fp.IncludeCharts,
                    ReportOption = fp.ReportOption,

                    // Additional fields from Report
                    Name = identityContext.Reports
                        .Where(r => r.Id == fp.ReportId)
                        .Select(r => r.Name)
                        .FirstOrDefault() ?? string.Empty,

                    ParentId = identityContext.Reports
                        .Where(r => r.Id == fp.ReportId)
                        .Select(r => r.ParentId)
                        .FirstOrDefault(),

                    RoleIds = identityContext.reportFeatures
                        .Where(rf => rf.ReportId == fp.ReportId)
                        .Select(rf => rf.RoleId)
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return BaseResult<RoleTyprReportResponse>.Ok(new RoleTyprReportResponse
            {
                RoleId = roleId,
                Permissions = permissions
            });
        }
        public async Task<BaseResult<ReportQueryResponse>> ExecuteReportAsync(ReportQueryRequest request)
        {
            var filters = request.Filters ?? new Dictionary<string, object>();
            var parameters = new List<NpgsqlParameter>();

            var reportOption = filters.TryGetValue("reportOption", out var opt) ? opt?.ToString() : null;

            string? sql = await GetReportQueryAsync(request.ReportIdOrName, reportOption);
            if (string.IsNullOrWhiteSpace(sql))
            {
                return new Error(ErrorCode.BadRequest, "No report found with given Name.");
            }

            AddFilterParameters(filters, parameters);

            var result = await ExecuteSqlAsync(sql, parameters);
            return new ReportQueryResponse { Records = result };

        }
        private async Task<string?> GetReportQueryAsync(string reportIdOrName, string? reportOption)
        {
            // Validate the report exists
            var report = await identityContext.Reports
                   .FirstOrDefaultAsync(r => r.Id == reportIdOrName || r.Name == reportIdOrName);
            // Build ReportType query
            var reportTypeQuery = identityContext.Set<ReportType>().AsQueryable()
                .Where(rt => rt.ReportID == report.Id);

            if (!string.IsNullOrWhiteSpace(reportOption))
            {
                reportTypeQuery = reportTypeQuery.Where(rt => rt.Type == reportOption);
            }
            else
            {
                reportTypeQuery = reportTypeQuery.Where(rt => string.IsNullOrEmpty(rt.Type));
            }

            var reportType = await reportTypeQuery.FirstOrDefaultAsync();
            return reportType?.Query;

        }

        private void AddFilterParameters(Dictionary<string, object> filters, List<NpgsqlParameter> parameters)
        {
            // ReportOption
            parameters.Add(new NpgsqlParameter("@ReportOption", NpgsqlDbType.Text)
            {
                Value = filters.TryGetValue("reportOption", out var reportOptionObj) && reportOptionObj != null
            ? reportOptionObj.ToString()
            : DBNull.Value
            });
            // StartDate
            parameters.Add(new NpgsqlParameter("@StartDate", NpgsqlDbType.Date)
            {
                Value = filters.TryGetValue("startDate", out var startDateObj) &&
                        DateTime.TryParse(startDateObj?.ToString(), out var startDate)
                    ? startDate.Date
                    : DBNull.Value
            });

            // EndDate
            parameters.Add(new NpgsqlParameter("@EndDate", NpgsqlDbType.Date)
            {
                Value = filters.TryGetValue("endDate", out var endDateObj) &&
                        DateTime.TryParse(endDateObj?.ToString(), out var endDate)
                    ? endDate.Date
                    : DBNull.Value
            });

            // Generic JSON Array handler
            AddJsonParameter(filters, parameters, "selectedBranches", "@LocationId");
            AddJsonParameter(filters, parameters, "selectedCurrencies", "@CurrencyId");
            AddJsonParameter(filters, parameters, "selectedTransactions", "@TransactionId");

        }
        private void AddJsonParameter(Dictionary<string, object> filters, List<NpgsqlParameter> parameters, string key, string paramName)
        {
            if (filters.TryGetValue(key, out var obj) &&
            !string.IsNullOrWhiteSpace(obj?.ToString()) &&
            obj.ToString()?.Length > 2)
            {
                var doc = JsonDocument.Parse(obj.ToString());
                var val = doc.RootElement[0].GetString();
                parameters.Add(new NpgsqlParameter(paramName, NpgsqlDbType.Text) { Value = val });
            }
            else
            {
                parameters.Add(new NpgsqlParameter(paramName, NpgsqlDbType.Text) { Value = DBNull.Value });
            }
        }

        private async Task<List<Dictionary<string, object>>> ExecuteSqlAsync(string sql, List<NpgsqlParameter> parameters)
        {
            var results = new List<Dictionary<string, object>>();
            using var connection = identityContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            foreach (var param in parameters)
                command.Parameters.Add(param);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                }
                results.Add(row);
            }

            return results;
        }
    }
}

