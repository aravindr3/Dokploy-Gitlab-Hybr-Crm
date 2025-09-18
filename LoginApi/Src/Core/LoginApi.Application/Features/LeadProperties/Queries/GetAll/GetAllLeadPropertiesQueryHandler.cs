using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Queries.GetAll;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperties.Queries.GetAll
{
    internal class GetAllLeadPropertiesQueryHandler(ILeadPropertiesServices leadPropertiesServices,
        IUserService userService,
        ILeadPropertiesValueServices leadPropertiesValueServices,
        IleadPropertyDefinitionServices leadPropertyDefinitionServices,
        ILeadRepository leadRepository,
        ILeadContactRepository leadContactRepository,
        ICountryRepository countryRepository)
    : IRequestHandler<GetAllLeadPropertiesquery, BaseResult<List<LeadPropertyValueDto>>>
    {
        public async Task<BaseResult<List<LeadPropertyValueDto>>> Handle(
    GetAllLeadPropertiesquery request,
    CancellationToken cancellationToken)
        {
            // Step 1: Fetch all values
            var values = await leadPropertiesValueServices.GetAllAsync();

            if (!values.Any())
            {
                return BaseResult<List<LeadPropertyValueDto>>.Failure(
                    new Error(ErrorCode.NotFound, "No property values found."));
            }

            // Step 2: Preload supporting data to avoid multiple DB hits
            var allDefinitions = await leadPropertyDefinitionServices.GetAllAsync();
            var allCountries = await countryRepository.GetAllAsync();
            var allLeads = await leadRepository.GetAllAsync();
            var allLeadContacts = await leadContactRepository.GetAllAsync();
            var allUsers = await userService.GetAllUsersAsync(); // Or however your userService supports getting multiple users

            // Step 3: Build DTO list
            //    var dtoList = values
            //        .Where(v => v.Status == 1)
            //        .Select(v =>
            //        {
            //            var def = allDefinitions.FirstOrDefault(d => d.Id == v.PropertyDefinitionId);
            //            var lead = allLeads.FirstOrDefault(l => l.Id == v.LeadId);
            //            var leadContact = allLeadContacts.FirstOrDefault(lc => lc.Id == lead?.LeadContactId);
            //            var owner =userService.GetUserByIdAsync( v.OwnerId);

            //            var value = v.PropertyDefinitionId == "01JZT20S7BZKW3YGM05S5GVFY2"
            //                ? allCountries.FirstOrDefault(c => c.Id == v.Value)?.CountryName
            //                : v.Value;

            //            return new LeadPropertyValueDto
            //            {
            //                Id = v.Id,
            //                LeadId = v.LeadId,
            //                LeadName = leadContact?.FirstName,
            //                OwnerId = v.OwnerId,
            //                OwnerName = owner?.Result?.Data?.UserName,
            //                PropertyDefinitionId = v.PropertyDefinitionId,
            //                PropertyDefinitionName = def?.FieldName,
            //                PropertyDefinitionDisplayName = def?.DisplayName,
            //                Value = value,
            //                Status = v.Status
            //            };
            //        })
            //        .ToList();

            //    return BaseResult<List<LeadPropertyValueDto>>.Ok(dtoList);
            //}
            var dtoList = values
        .Where(v => v.Status == 1)
        .Select(v =>
        {
            var def = allDefinitions.FirstOrDefault(d => d.Id == v.PropertyDefinitionId);
            var lead = allLeads.FirstOrDefault(l => l.Id == v.LeadId);
            var leadContact = allLeadContacts.FirstOrDefault(lc => lc.Id == lead?.LeadContactId);
            var owner = userService.GetUserByIdAsync(v.OwnerId); // still async, caution!

            string value;

            if (v.PropertyDefinitionId == "01JZT20S7BZKW3YGM05S5GVFY2" && !string.IsNullOrWhiteSpace(v.Value))
            {
                // Multiple country IDs (comma-separated)
                var countryIds = v.Value.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var countryNames = allCountries
                    .Where(c => countryIds.Contains(c.Id))
                    .Select(c => c.CountryName)
                    .ToList();

                value = string.Join(", ", countryNames);
            }
            else
            {
                value = v.Value;
            }

            return new LeadPropertyValueDto
            {
                Id = v.Id,
                LeadId = v.LeadId,
                LeadName = leadContact?.FirstName,
                OwnerId = v.OwnerId,
                OwnerName = owner?.Result?.Data?.UserName,  // Ideally cache user list to avoid .Result
                PropertyDefinitionId = v.PropertyDefinitionId,
                PropertyDefinitionName = def?.FieldName,
                PropertyDefinitionDisplayName = def?.DisplayName,
                Value = value,
                Status = v.Status
            };
        })
        .ToList();

            return BaseResult<List<LeadPropertyValueDto>>.Ok(dtoList);


        }
        }
    }
