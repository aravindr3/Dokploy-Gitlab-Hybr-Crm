using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Query.GetAll;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using Newtonsoft.Json.Linq;

namespace HyBrCRM.Application.Features.HolidaysLead.Query.GetAll
{
    public class GetAllHolidayLeadQueryHandler(ILeadContactRepository leadContactRepository, IHolidaysLeadServices holidaysLeadServices,
        ICountryRepository countryRepository,
        ISubDomainServices subDomainServices,
        IUserService userService,
        ICommonMasterRepository commonMasterRepository,
        IStagesServices stagesServices,
        IDomainRepository domainRepository,
        IDomainStagesServices domainStagesServices,
        IleadPropertyDefinitionServices leadPropertyDefinitionServices,
        ILeadPropertiesValueServices leadPropertiesValueServices,
        ITaskMasterServices taskMasterServices,
        ILeadDocumentServices leadDocumentServices,

        IVerticalServices verticalServices, ILeadPropertiesServices leadPropertiesServices)
    : IRequestHandler<GetAllHolidayleadQuery, BaseResult<List<HoliDayLeadDto>>>
    {
        public async Task<BaseResult<List<HoliDayLeadDto>>> Handle(GetAllHolidayleadQuery request, CancellationToken cancellationToken)
        {
            var leads = await holidaysLeadServices.GetAllAsync();

            

            var activeLeads = leads.Where(l => l.Status == 1).ToList();
            if (!activeLeads.Any())
                return new Error(ErrorCode.NotFound, "No active leads found");
            var allDefinitions = await leadPropertyDefinitionServices.GetAllAsync();
            var allCountries = await countryRepository.GetAllAsync();
            var allLeads = await holidaysLeadServices.GetAllAsync();
            var allLeadContacts = await leadContactRepository.GetAllAsync();
            var allUsers = await userService.GetAllUsersAsync();
            var result = new List<HoliDayLeadDto>();
            foreach (var master in activeLeads)
            {
                var contacts = await leadContactRepository.GetByIdChildAsync(a => a.Id == master.LeadContactId, c => c.Country, j => j.State, x => x.Gender);
                var cont = contacts.Where(a => a.Status == 1).ToList();
                var contact = cont.FirstOrDefault();
                var properties = await leadPropertiesValueServices?.GetByIdChildAsync(a => a.LeadId == master.Id && a.Status == 1);

                var agent = allUsers?.Data.FirstOrDefault(a => a.Id == master.AsignedAgent);

                var vertical = await verticalServices?.GetVerticalById(contact?.VerticalId);
                var dtoContact = cont.Select(c => new LeadContactDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber1 = c.PhoneNumber1,
                    PhoneNumber2 = c.PhoneNumber2,
                    WhatsAppNumber = c.WhatsAppNumber,
                    Email = c.Email,
                    GenderId = c.GenderId,
                    GenderName = c?.GenderId == "01JXVQH4EANY2GCTS8PDR5PWKS1" ? "" : c?.Gender?.CommonName,
                    ParentsName = c?.ParentsName,
                    ParentsPhoneNumber = c?.ParentsPhoneNumber,
                    CountryId = c?.CountryId,
                    CountryName = c?.Country?.CountryName,
                    StateId = c?.StateId,
                    StateName = c?.StateId == "01JJ6JTZPM2N1RQBHKSGCNVE0" ? "" : c?.State?.StateName,
                    StateOutsideIndia = c?.StateOutsideIndia,
                    District = c?.District,
                    City = c?.City,
                    Locality = c?.Locality,
                    VerticalId = c?.VerticalId,
                    VerticalName = vertical?.Data?.VerticalName,

                    Status = master?.Status,
                }).FirstOrDefault();
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


                var lead = await holidaysLeadServices.GetByIdChildAsync(a => a.Id == master.Id);
                var leadV = lead.FirstOrDefault();
                var leadvertical = await verticalServices?.GetVerticalById(leadV?.VerticalId);
                var taskList = await taskMasterServices.GetByIdChildAsync(a => a.LeadId == master.Id);

                var lastTask = taskList
                    .OrderByDescending(t => t.TaskDate ?? DateTime.MinValue)
                    .FirstOrDefault();
                var domainStage = await domainStagesServices?.GetByIdAsync(lastTask?.DomainStagesId);

                // 2. Then get the Stages by Id using lambda
                var stagesName = "";
                if (domainStage != null)
                {
                    var status = await stagesServices?.GetByIdChildAsync(x => x.Id == domainStage.StagesId && x.Status == 1);
                     stagesName = status.FirstOrDefault()?.Name;

                }
                var documentData = await leadDocumentServices?.GetByIdChildAsync(a => a.LeadId == master.Id && a.Status == 1);
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
                var dtoLead = new HoliDayLeadDto
                {
                    Id = master.Id,
                    LeadContactId = master.LeadContactId,
                    LeadSourceId = master?.LeadSourceId,
                    LeadSourceName = commonMasterRepository.GetByIdAsync(master.LeadSourceId)?.Result?.CommonName,
                    CategoryId = master?.CategoryId,
                    CategoryName = subDomainServices?.GetByIdAsync(master?.CategoryId)?.Result?.CategoryName,
                    LeadStatusId = master?.LeadStatusId,
                    LeadStatusName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(master.LeadStatusId)?.Result?.StagesId)?.Result?.Name,
                    EnquiryType = master.EnquiryType,
                    TravelType = master.TravelType,
                    PrefferedDestination = master?.PrefferedDestination,
                    TripDuration = master?.TripDuration,
                    DepatureCity = master?.DepatureCity,
                    StartDate = master?.StartDate,
                    EndDate = master?.EndDate,
                    Adults = master?.Adults ?? 0,
                    ChildWithBed = master?.ChildWithBed ?? 0,
                    ChildWithoutBed = master?.ChildWithoutBed ?? 0,
                    BeddingPreference = master?.BeddingPreference,
                    RoomType = master?.RoomType,
                    MealPlan = master?.MealPlan,
                    Notes = master?.Notes,
                    FollowUpDate = master?.FollowUpDate,
                    AsignedAgent = agent?.UserName,
                    //AsignedAgent = userService?.GetUserByIdAsync(master?.AsignedAgent)?.Result?.Data?.UserName,
                    //AsignedAgent = master?.AsignedAgent,
                    VerticalId = master?.VerticalId,
                    VerticalName = leadvertical?.Data?.VerticalName,
                    Created = master?.Created,
                    LastTask = lastTask?.Created,
                    Stage = stagesName,
                    LeadProperties = dtoProperties,
                    Status = master?.Status,
                    Other = master?.Other,
                    Contact = dtoContact,
                    LeadDocument = leadDocumentDto
                };

                result.Add(dtoLead);
            }

            return BaseResult<List<HoliDayLeadDto>>.Ok(result);
        }
    
    }
}
