using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using HyBrCRM.Application.Features.LeadContact.Queries.GetById;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperties.Queries.GetById
{
    public class GetLeadProperiesByIdQueryHandler(
    ILeadPropertiesServices leadPropertiesServices,
    ICountryRepository countryRepository,
    IUserService userService,
    ILeadPropertiesValueServices leadPropertiesValueServices,
    IleadPropertyDefinitionServices leadPropertyDefinition,
    ILeadContactRepository leadContactRepository,
    IDomainRepository domainRepository,
    ILeadRepository leadRepository,
    IHolidaysLeadServices holidaysLeadServices
    
   
    ) : IRequestHandler<LeadPropertiesGetByIdQuery, BaseResult<List<LeadPropertyValueDto>>>
    {


        public async Task<BaseResult<List<LeadPropertyValueDto>>> Handle(
    LeadPropertiesGetByIdQuery request,
    CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.LeadId))
            {
                return BaseResult<List<LeadPropertyValueDto>>.Failure(
                    new Error(ErrorCode.BadRequest, "LeadId is required."));
            }

            // Fetch all property values for this LeadId, including PropertyDefinition
            var values = (await leadPropertiesValueServices.GetAllAsync())
                .Where(d => d.LeadId == request.LeadId)
                .ToList();
            //var values = await leadPropertiesValueServices.GetAllAsync().
            //    x => x.LeadId == request.LeadId);

            if (!values.Any())
            {
                return BaseResult<List<LeadPropertyValueDto>>.Failure(
                    new Error(ErrorCode.NotFound, "No property values found for the specified LeadId."));
            }
            var domain = domainRepository.GetDomainById(request?.DomainId);
            var domainName = domain.Result?.Data?.DomainName;
            var leadName = "";
            if (domainName.ToLower() == "holidays")
            {
                var lead = await holidaysLeadServices?.GetByIdAsync(request.LeadId);
                var leadContact = leadContactRepository?.GetByIdAsync(lead?.LeadContactId);

                leadName = leadContact?.Result?.FirstName;
            }
            else
            {
                var lead = await leadRepository.GetByIdAsync(request.LeadId);
                var leadContact = leadContactRepository.GetByIdAsync(lead.LeadContactId);

                leadName = leadContact?.Result?.FirstName;
            }

            // Optionally fetch lead name and owner name once

            //var lead = await leadContactRepository.GetByIdAsync(request.LeadId);
            // var leadName = lead?.FirstName;

            // You can assume all values have the same OwnerId
            var ownerId = values.FirstOrDefault()?.OwnerId;
            var owner = await userService.GetUserByIdAsync(ownerId);
            var ownerName = owner?.Data?.UserName;
            //        var dtoList = values
            //.Where(a => a.Status == 1 && request.LeadId == a.LeadId)
            //.Select(v => new LeadPropertyValueDto
            //{
            //    Id = v.Id,
            //    LeadId = v.LeadId,
            //    LeadName = leadName,
            //    OwnerId = ownerId,
            //    OwnerName = ownerName,
            //    PropertyDefinitionId = v?.PropertyDefinitionId,
            //    Value = (v.PropertyDefinitionId == "01JZT20S7BZKW3YGM05S5GVFY2")
            //        ? countryRepository.GetByIdAsync(v.Value)?.Result?.CountryName
            //        : v.Value,
            //    PropertyDefinitionName = leadPropertyDefinition?.GetByIdAsync(v.PropertyDefinitionId)?.Result?.FieldName,
            //    PropertyDefinitionDisplayName = leadPropertyDefinition?.GetByIdAsync(v.PropertyDefinitionId)?.Result?.DisplayName,
            //    Status = v?.Status
            //})
            //.ToList();
            var countryDefId = "01JZT20S7BZKW3YGM05S5GVFY2";

            var dtoList = new List<LeadPropertyValueDto>();

            foreach (var v in values.Where(a => a.Status == 1 && request.LeadId == a.LeadId))
            {
                string? value;
                if (v.PropertyDefinitionId == countryDefId && !string.IsNullOrWhiteSpace(v.Value))
                {
                    // Handle multiple country IDs
                    var countryIds = v.Value.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var countryNames = new List<string>();

                    foreach (var id in countryIds)
                    {
                        var country = await countryRepository.GetByIdAsync(id.Trim());
                        if (country != null && !string.IsNullOrWhiteSpace(country.CountryName))
                            countryNames.Add(country.CountryName);
                    }

                    value = string.Join(", ", countryNames);
                }
                else
                {
                    value = v.Value;
                }

                var definition = await leadPropertyDefinition.GetByIdAsync(v.PropertyDefinitionId ?? "");

                dtoList.Add(new LeadPropertyValueDto
                {
                    Id = v.Id,
                    LeadId = v.LeadId,
                    LeadName = leadName,
                    OwnerId = ownerId,
                    OwnerName = ownerName,
                    PropertyDefinitionId = v?.PropertyDefinitionId,
                    Value = value,
                    PropertyDefinitionName = definition?.FieldName,
                    PropertyDefinitionDisplayName = definition?.DisplayName,
                    Status = v?.Status
                });
            }




            return BaseResult<List<LeadPropertyValueDto>>.Ok(dtoList);
        }


    }
}
