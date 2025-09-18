using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Queries.GetById;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Lead.Query.GetById
{
    public class GetLeadByIdQueryHandler(
        ILeadRepository leadRepository,
    ILeadContactRepository leadContactRepository,
    ILeadPropertiesServices leadPropertiesServices,
    IVerticalServices verticalServices,
    ICountryRepository countryRepository,
    IUserService userService,
    IStagesServices stagesServices,
    IDomainStagesServices domainStagesServices,
    ISubDomainServices subDomainServices,
    ILeadPropertiesValueServices leadPropertiesValueServices ,
    IleadPropertyDefinitionServices leadPropertyDefinitionServices,
    ITaskMasterServices taskMasterServices,
    ILeadDocumentServices leadDocumentServices
    
    ) : IRequestHandler<GetLeadByIdQuery, BaseResult<LeadDto>>
    {
        public async Task<BaseResult<LeadDto>> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken)
        {
            var masters = await leadRepository.GetByIdChildAsync(
                a => a.Id == request.Id,
                o => o.LeadContact,
                c => c.LeadSource,
                x => x.LeadStatus
            );

            var master = masters.FirstOrDefault(a => a.Status == 1);
            if (master is null)
                return new Error(ErrorCode.NotFound, "Lead Contact not found");

            var contacts = await leadContactRepository.GetByIdChildAsync(
                a => a.Id == master.LeadContactId,
                c => c.Country,
                x => x.State,
                y => y.Gender,
                e => e.State
            );

            var contact = contacts.FirstOrDefault(c => c.Status == 1);
            var verticals = await verticalServices?.GetVerticalById(contact?.VerticalId);
            var leadVertical = await verticalServices?.GetVerticalById(master?.VericalId);

            var dtoContact = contact == null ? null : new LeadContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber1 = contact.PhoneNumber1,
                PhoneNumber2 = contact.PhoneNumber2,
                WhatsAppNumber = contact.WhatsAppNumber,
                Email = contact.Email,
                GenderId = contact.GenderId,
                GenderName = contact?.GenderId == "01JXVQH4EANY2GCTS8PDR5PWKS1" ? "" : contact?.Gender?.CommonName,
                ParentsName = contact?.ParentsName,
                ParentsPhoneNumber = contact?.ParentsPhoneNumber,
                CountryId = contact?.CountryId == "01JJ6JTZPM2N1RQBHKSGCNVE010" ? "" : contact?.CountryId,
                CountryName = contact?.CountryId == "01JJ6JTZPM2N1RQBHKSGCNVE010" ? "" : contact?.Country?.CountryName,
                StateId = contact?.StateId,
                StateName = contact?.StateId == "01JJ6JTZPM2N1RQBHKSGCNVE0" ? "" : contact?.State?.StateName,
                StateOutsideIndia = contact?.StateOutsideIndia,
                District = contact?.District,
                City = contact?.City,
                Locality = contact?.Locality,
                VerticalId = contact?.VerticalId,
                VerticalName = verticals?.Data?.VerticalName,
                Status = master?.Status,
            };

            // Preload needed data
            var allDefinitions = await leadPropertyDefinitionServices.GetAllAsync();
            var allCountries = await countryRepository.GetAllAsync();
            var allLeads = await leadRepository.GetAllAsync();
            var allLeadContacts = await leadContactRepository.GetAllAsync();
            var allUsers = await userService.GetAllUsersAsync();

            var values = await leadPropertiesValueServices.GetByIdChildAsync(a => a.LeadId == master.Id);

            var dtoProperties = values == null
                ? new List<LeadPropertyValueDto>()
                : (await Task.WhenAll(values
                    .Where(v => v.Status == 1)
                    .Select(async v =>
                    {
                        var def = allDefinitions.FirstOrDefault(d => d.Id == v.PropertyDefinitionId);
                        var propertyLead = allLeads.FirstOrDefault(l => l.Id == v.LeadId);
                        var leadContact = allLeadContacts.FirstOrDefault(lc => lc.Id == propertyLead?.LeadContactId);
                        var owner = allUsers.Data.FirstOrDefault(a => a.Id == v.OwnerId);

                        string value;
                        if (v.PropertyDefinitionId == "01JZT20S7BZKW3YGM05S5GVFY2" && !string.IsNullOrWhiteSpace(v.Value))
                        {
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
                            OwnerName = owner?.UserName,
                            PropertyDefinitionId = v.PropertyDefinitionId,
                            PropertyDefinitionName = def?.FieldName,
                            PropertyDefinitionDisplayName = def?.DisplayName,
                            Value = value,
                            Status = v.Status
                        };
                    }))).ToList();

            var category = await subDomainServices?.GetByIdAsync(master?.CategoryId);
            var leadStatusStage = await domainStagesServices.GetByIdAsync(master.LeadStatusId);
            var leadStage = await stagesServices.GetByIdAsync(leadStatusStage?.StagesId ?? "");
            var preferedCountry = await countryRepository?.GetByIdAsync(master?.PreferedCountry);
            var taskList = await taskMasterServices.GetByIdChildAsync(a => a.LeadId == master.Id);


            //        var lastTask = taskList
            //.Where(t => t.Status == 1)
            //.OrderByDescending(t => t.TaskDate ?? DateTime.MinValue)
            //.FirstOrDefault();
            var lastTask = taskList
            .Where(t => t.Status == 1)
            .OrderByDescending(t => new[] { t.LastModified, t.Created }
                .Where(d => d.HasValue)
                .Select(d => d.Value)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max())
            .FirstOrDefault();


            // 1. Get DomainStages by Id (assuming async returns Task<DomainStage>)
            var domainStage = await domainStagesServices?.GetByIdAsync(lastTask?.DomainStagesId);

            // 2. Then get the Stages by Id using lambda
            //var status = "";
            var stagesName = "";
            if (domainStage != null)
            {
                var status = await stagesServices?.GetByIdChildAsync(x => x.Id == domainStage.StagesId && x.Status ==1);
                 stagesName = status.FirstOrDefault()?.Name;

            }
            var documentData = await leadDocumentServices.GetByIdChildAsync(a => a.LeadId == request.Id && a.Status == 1);
            var leadDocumentDto = documentData.Select(c => new LeadDocumentDto
            {
                Id = c?.Id,
                LeadId = c?.LeadId,
                FileName = c?.FileName,
                Remark = c?.Remark,
                FileType = c?.FileType,
                FileSize = c?.FileSize,

                Status = c.Status,
            }).ToList();

            var dtoLead = new LeadDto
            {
                Id = master.Id,
                LeadSourceId = master?.LeadSourceId,
                LeadSourceName = master?.LeadSource?.CommonName,
                LeadCategoryId = master?.CategoryId,
                LeadCategoryName = category?.CategoryName,
                LeadStatusId = master?.LeadStatusId,
                LeadStatusName = leadStage?.Name,
                VerticalId = master?.VericalId,
                VerticalName = leadVertical?.Data?.VerticalName,
                CategoryId = master?.CategoryId,
                CategoryName = category?.CategoryName,
                Notes = master?.Notes,
                PreferedCountry = master?.PreferedCountry == "01JJ6JTZPM2N1RQBHKSGCNVE010" ? "" : master?.PreferedCountry,
                PreferedCountryName = master?.PreferedCountry == "01JJ6JTZPM2N1RQBHKSGCNVE010" ? "" : preferedCountry?.CountryName,
                Created = master?.Created,
                LastTask = lastTask?.Created,
                Stage = stagesName,
                Contact = dtoContact,
                Status = master?.Status,
                LeadProperties = dtoProperties,
                LeadDocument = leadDocumentDto
                
            };

            return BaseResult<LeadDto>.Ok(dtoLead);
        }

    }

}
