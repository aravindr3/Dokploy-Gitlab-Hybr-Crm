using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using HyBrForex.Application.DTOs.Tenant.Responses;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Mappings;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Contexts;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class FunctionalMasterService(IdentityContext identityContext, IMapper mapper) : IFunctionalMasterService
    {

        public async Task<BaseResult<FunctionalMasterResponse>> CreateAsync(FunctionalMasterRequest request)
        {
            try
            {
                // Check if FunctionalMaster already exists based on a unique property.
                var existingFunctionalMaster = await identityContext.FunctionalMasters
                    .FirstOrDefaultAsync(fm => fm.FunctionalName == request.FunctionalName);

                if (existingFunctionalMaster != null)
                {
                    return new Error(ErrorCode.AlreadyExists, "Functional Master with the same name already exists.");
                }

                // Map the request to the entity (this mapping now handles ChildRouterLinks)
                var functionalMaster = FunctionalMasterMapper.ToEntity(request);
                functionalMaster.Id = Ulid.NewUlid().ToString();

                // Determine the Orderby value based on header/subheader flags.
                if (functionalMaster.isHeader)
                {
                    var lastHeader = await identityContext.FunctionalMasters
                        .Where(fm => fm.isHeader)
                        .OrderByDescending(fm => fm.Orderby)
                        .FirstOrDefaultAsync();

                    functionalMaster.Orderby = lastHeader != null ? lastHeader.Orderby + 1 : 1;
                }
                else if (functionalMaster.isSubHeader)
                {
                    var lastSubHeader = await identityContext.FunctionalMasters
                        .Where(fm => fm.isSubHeader)
                        .OrderByDescending(fm => fm.Orderby)
                        .FirstOrDefaultAsync();

                    functionalMaster.Orderby = lastSubHeader != null ? lastSubHeader.Orderby + 1 : 1;
                }
                else
                {
                    // Optionally, assign Orderby from the request or use a default.
                    functionalMaster.Orderby = request.Orderby;
                }

                identityContext.FunctionalMasters.Add(functionalMaster);
                await identityContext.SaveChangesAsync();

                return FunctionalMasterMapper.ToResponse(functionalMaster);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here.
                return new Error(ErrorCode.ErrorInIdentity, "An error occurred while creating Functional Master.");
            }
        }

        public async Task<BaseResult<FunctionalMasterResponse>> UpdateAsync(string id, FunctionalMasterRequest request)
        {
            try
            {
                var functionalMaster = await identityContext.FunctionalMasters.FindAsync(id);
                if (functionalMaster == null)
                    return new Error(ErrorCode.NotFound, "Functional master not found...");

                // Update the entity using the mapping helper method that includes ChildRouterLinks.
                FunctionalMasterMapper.Map(request, functionalMaster);
                await identityContext.SaveChangesAsync();

                return FunctionalMasterMapper.ToResponse(functionalMaster);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here.
                return new Error(ErrorCode.NotFound, "An error occurred while updating Functional Master...");
            }
        }



        public async Task<BaseResult<bool>> DeleteAsync(string id)
        {
            try
            {
                var functionalMaster = await identityContext.FunctionalMasters
                    .FirstOrDefaultAsync(fm => fm.Id == id);

                if (functionalMaster == null)
                {
                    return new Error(ErrorCode.NotFound, "Functional Master not found.");
                }
                // Remove the functional master
                identityContext.FunctionalMasters.Remove(functionalMaster);
                await identityContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while deleting Functional Master...");
            }
        }


        public async Task<BaseResult<List<FunctionalMasterResponse>>> GetAllAsync()
        {
            try
            {
                var functionalMasters = await identityContext.FunctionalMasters.ToListAsync();

                var responses = functionalMasters.Select(fm => new FunctionalMasterResponse
                {
                    Id = fm.Id,
                    FunctionalName = fm.FunctionalName,
                    RouterName = fm.RouterName,
                    RouterDescription = fm.RouterDescription,
                    RouterLink = fm.RouterLink,
                    SequenceId = fm.SequenceId,
                    Icon = fm.Icon,
                    ParentId = fm.ParentId,
                    isHeader = fm.isHeader,
                    isSubHeader = fm.isSubHeader,
                    Orderby = fm.Orderby
                }).ToList();

                return responses;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving Functional Masters...");
            }
        }

        public async Task<BaseResult<FunctionalMasterResponse>> GetByIdAsync(string id)
        {
            try
            {
                var functionalMaster = await identityContext.FunctionalMasters.FindAsync(id);
                if (functionalMaster == null)
                    return new Error(ErrorCode.NotFound, "Functional master not found...");

                var response = mapper.Map<FunctionalMasterResponse>(functionalMaster);

                return mapper.Map<FunctionalMasterResponse>(response);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving Functional Masters...");
            }
        }

        public async Task<BaseResult<string>> CreateFeaturePermissionsAsync(FeatureRoleMappingRequest request)
        {
            var role = await identityContext.Roles.FindAsync(request.RoleId);
            if (role == null)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving Functional Masters...");
            }

            // Remove existing feature roles
            var existingPermissions = identityContext.featureRoles
                .Where(fp => fp.RoleId == request.RoleId)
                .ToList();
            identityContext.featureRoles.RemoveRange(existingPermissions);

            // Remove existing functional role mappings
            var existingFunctionalMappings = identityContext.FunctionalRoleMappings
                .Where(f => f.RoleId == request.RoleId)
                .ToList();
            if (existingFunctionalMappings.Any())
            {
                identityContext.FunctionalRoleMappings.RemoveRange(existingFunctionalMappings);
            }

            // Add new feature permissions
            var newPermissions = request.Permissions.Select(p => new FeatureRole
            {
                Id = Ulid.NewUlid().ToString(),
                RoleId = request.RoleId,
                FeatureId = p.FeatureId,
                Assign = p.Assign,
                Create = p.Create,
                Edit = p.Edit,
                Delete = p.Delete,
                View = p.View,
                Print = p.Print,
                Approval = p.Approval
            });
            await identityContext.featureRoles.AddRangeAsync(newPermissions);

            // Add new functional role mappings
            var newFunctionalMappings = request.Permissions.Select(p => new FunctionalRoleMapping
            {
                Id = Ulid.NewUlid().ToString(),
                FunctionalId = p.FeatureId, // Or p.FunctionalId if available in your request
                RoleId = request.RoleId
            });
            await identityContext.FunctionalRoleMappings.AddRangeAsync(newFunctionalMappings);

            await identityContext.SaveChangesAsync();

            return BaseResult<string>.Ok("Successfully Saved");
        }


        public async Task<BaseResult<FeatureRoleResponse>> GetFeaturePermissionsByRoleIdAsync(string roleId)
        {
            var role = await identityContext.Roles.FindAsync(roleId);
            if (role == null)
            {
                return new Error(ErrorCode.NotFound, "Role not found");
            }

            var permissions = identityContext.featureRoles
                .Where(fp => fp.RoleId == roleId)
                .Select(fp => new FeaturePermissionDto
                {
                    FeatureId = fp.FeatureId,
                    Assign = fp.Assign,
                    Create = fp.Create,
                    Edit = fp.Edit,
                    Delete = fp.Delete,
                    View = fp.View,
                    Print = fp.Print,
                    Approval = fp.Approval
                }).ToList();

            return BaseResult<FeatureRoleResponse>.Ok(new FeatureRoleResponse
            {
                RoleId = roleId,
                Permissions = permissions
            });
        }
        public async Task<BaseResult<List<FunctionalMasterResponse>>> GetAllNewAsync(List<string> roleIds)
        {
            try
            {
                // Fetch function IDs based on the provided role IDs from the FunctionalRoleMapping table
                var functionIds = await identityContext.FunctionalRoleMappings
                    .Where(frm => roleIds.Contains(frm.RoleId))
                    .Select(frm => frm.FunctionalId)
                    .Distinct()  // Avoid duplicate IDs if any role maps to the same function
                    .ToListAsync();

                // If no function IDs are found, return an empty list.
                if (!functionIds.Any())
                {
                    return new BaseResult<List<FunctionalMasterResponse>>
                    {
                        Data = new List<FunctionalMasterResponse>(),
                        Success = true
                    };
                }

                // Retrieve FunctionalMasters that have an ID matching one of the functionIds
                var functionalMasters = await identityContext.FunctionalMasters
                    .Where(fm => functionIds.Contains(fm.Id))
                    .ToListAsync();

                // Map the result to the response DTO
                var responses = functionalMasters.Select(fm => new FunctionalMasterResponse
                {
                    Id = fm.Id,
                    FunctionalName = fm.FunctionalName,
                    RouterName = fm.RouterName,
                    RouterDescription = fm.RouterDescription,
                    RouterLink = fm.RouterLink,
                    SequenceId = fm.SequenceId,
                    Icon = fm.Icon,
                    ParentId = fm.ParentId,
                    isHeader = fm.isHeader,
                    isSubHeader = fm.isSubHeader,
                }).ToList();

                // Order the list: first by isHeader (headers first), then by isSubHeader, and finally by SequenceId.
                var orderedResponses = responses
                    .OrderByDescending(r => r.isHeader)      // true headers come first
                    .ThenByDescending(r => r.isSubHeader)      // then true subheaders
                    .ThenBy(r => r.SequenceId)                 // then by sequence
                    .ToList();

                return new BaseResult<List<FunctionalMasterResponse>>
                {
                    Data = orderedResponses,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // Log the exception as needed for debugging.
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving Functional Masters...");
            }

        }
        public async Task<BaseResult<GetRouterLinksResponse>> GetRouterLinksByRoleAsync(List<string> roleIds)
        {
            // Step 1: Fetch mappings for the given roles
            var mappings = await identityContext.FunctionalRoleMappings
                .Where(m => roleIds.Contains(m.RoleId))
                .ToListAsync();

            if (mappings == null || !mappings.Any())
            {
                return new BaseResult<GetRouterLinksResponse>
                {
                    Data = new GetRouterLinksResponse(),
                    Success = false,
                };
            }

            var functionalIds = mappings.Select(m => m.FunctionalId).Distinct().ToList();

            // Step 2: Fetch FunctionalMasters
            var functionalMasters = await identityContext.FunctionalMasters
                .Where(fm => functionalIds.Contains(fm.Id))
                .ToListAsync();

            // Step 3: Fetch FeaturePermissions for all roleIds
            var featurePermissions = await identityContext.featureRoles
                .Where(fp => roleIds.Contains(fp.RoleId))
                .Select(fp => new
                {
                    fp.FeatureId,
                    fp.RoleId,
                    fp.Assign,
                    fp.Create,
                    fp.Edit,
                    fp.Delete,
                    fp.View,
                    fp.Print,
                    fp.Approval
                })
                .ToListAsync();

            // Step 4: Combine RouterLinks + Permissions
            var routerLinks = functionalMasters
                .SelectMany(fm =>
                    new[] { fm.RouterLink }
                    .Concat(fm.ChildRouterLinks ?? Enumerable.Empty<string>())
                    .Select(link =>
                    {
                        var permission = featurePermissions.FirstOrDefault(fp => fp.FeatureId == fm.Id);

                        return new RouterLinkDto
                        {
                            FunctionalId = fm.Id,
                            RouterLink = link,
                            Assign = permission?.Assign,
                            Create = permission?.Create,
                            Edit = permission?.Edit,
                            Delete = permission?.Delete,
                            View = permission?.View,
                            Print = permission?.Print,
                            Approval = permission?.Approval
                        };
                    })
                )
                .DistinctBy(x => x.RouterLink)
                .ToList();

            return new BaseResult<GetRouterLinksResponse>
            {
                Data = new GetRouterLinksResponse
                {
                    RouterLinks = routerLinks
                },
                Success = true
            };
        }

    }

}