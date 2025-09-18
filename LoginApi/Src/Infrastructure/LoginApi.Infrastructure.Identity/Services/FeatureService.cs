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
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class FeatureService(IdentityContext identityContext, IMapper mapper):IFeatureService
    {
        public async Task<BaseResult<FeatureResponse>> CreateAsync(FeatureRequest request)
        {
            try
            {
                // Check if a feature with the same name already exists
                var existingFeature = await identityContext.ApplicationFeatures
                    .FirstOrDefaultAsync(f => f.FeatureName == request.FeatureName);

                if (existingFeature != null)
                {
                    return new Error(ErrorCode.AlreadyExists, "Feature with the same name already exists.");
                }

                var feature = mapper.Map<ApplicationFeature>(request);
                feature.Id = Ulid.NewUlid().ToString();

                identityContext.ApplicationFeatures.Add(feature);
                await identityContext.SaveChangesAsync();

                if (request.RoleIds?.Any() == true)
                {
                    var roleMappings = request.RoleIds.Select(roleId => new FeatureRoleMapping
                    {
                        Id = Ulid.NewUlid().ToString(),
                        FeatureId = feature.Id,
                        RoleId = roleId,
                        FunctionalId = request.FunctionalId
                    }).ToList();

                    identityContext.featureRoleMappings.AddRange(roleMappings);
                    await identityContext.SaveChangesAsync();
                }

                return mapper.Map<FeatureResponse>(feature);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while creating Feature...");
            }
        }


        public async Task<BaseResult<FeatureResponse>>UpdateAsync(string id, FeatureRequest request)
        {
            try
            {
                var feature = await identityContext.ApplicationFeatures.FindAsync(id);
                if (feature == null)
                    return new Error(ErrorCode.NotFound, " Feature not found...");

                mapper.Map(request, feature);
                await identityContext.SaveChangesAsync();

                var existingRoleMappings = identityContext.featureRoleMappings
                    .Where(mapping => mapping.FeatureId == feature.Id).ToList();

                identityContext.featureRoleMappings.RemoveRange(existingRoleMappings);
                await identityContext.SaveChangesAsync();

                if (request.RoleIds?.Any() == true)
                {
                    var roleMappings = request.RoleIds.Select(roleId => new FeatureRoleMapping
                    {
                        Id = Ulid.NewUlid().ToString(),
                        FeatureId=feature.Id,
                        FunctionalId = request.FunctionalId,
                        RoleId = roleId
                    }).ToList();

                    identityContext.featureRoleMappings.AddRange(roleMappings);
                    await identityContext.SaveChangesAsync();
                }

                return mapper.Map<FeatureResponse>(feature);
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
                var feature = await identityContext.ApplicationFeatures
                    .FirstOrDefaultAsync(fm => fm.Id == id);

                if (feature == null)
                {
                    return new Error(ErrorCode.NotFound, " Feature not found.");
                }

                // Remove related role mappings first
                var roleMappings = await identityContext.featureRoleMappings
                    .Where(fr => fr.FeatureId == id)
                    .ToListAsync();

                if (roleMappings.Any())
                {
                    identityContext.featureRoleMappings.RemoveRange(roleMappings);
                    await identityContext.SaveChangesAsync();
                }

                // Remove the functional master
                identityContext.ApplicationFeatures.Remove(feature);
                await identityContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while deleting  Feature...");
            }
        }


        public async Task<BaseResult<List<FeatureResponse>>> GetAllAsync()
        {
            try
            {
                var features = await identityContext.ApplicationFeatures.ToListAsync();

                var responses = features.Select(fm => new FeatureResponse
                {
                    Id = fm.Id,
                    FeatureName = fm.FeatureName,
                    Create = fm.Create,
                    Edit  = fm.Edit,
                    Delete = fm.Delete,
                    View = fm.View,
                    FunctionalId = identityContext.featureRoleMappings
                       .Where(fr => fr.FeatureId == fm.Id)
                       .Select(fr => fr.FunctionalId)
                       .FirstOrDefault(),
                    RoleIds = identityContext.featureRoleMappings
                        .Where(fr => fr.FeatureId == fm.Id)
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

        public async Task<BaseResult<FeatureResponse>> GetByIdAsync(string id)
        {
            try
            {
                var feature = await identityContext.ApplicationFeatures.FindAsync(id);
                if (feature == null)
                    return new Error(ErrorCode.NotFound, "Functional master not found...");

                var response = mapper.Map<FeatureResponse>(feature);

                if (await identityContext.featureRoleMappings.AnyAsync(fr => fr.FeatureId == id))
                {
                    var functional = await identityContext.featureRoleMappings.Where(fr => fr.FeatureId == id).FirstOrDefaultAsync();
                    response.FunctionalId= functional?.FunctionalId;
                    response.RoleIds = await identityContext.featureRoleMappings
                        .Where(fr => fr.FeatureId == id)
                        .Select(fr => fr.RoleId)
                        .ToListAsync();
                }

                return mapper.Map<FeatureResponse>(response);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while retrieving Feature...");
            }
        }
    }
}
