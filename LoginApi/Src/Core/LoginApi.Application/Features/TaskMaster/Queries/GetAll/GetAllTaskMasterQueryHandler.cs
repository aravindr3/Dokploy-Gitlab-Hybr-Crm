using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Queries.GetAll;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace HyBrCRM.Application.Features.TaskMaster.Queries.GetAll
{
    public class GetAllTaskMasterQueryHandler(ILeadRepository leadRepository,ITaskMasterServices taskMasterServices,
        IDomainStagesServices domainStagesServices,
        IStagesServices stagesServices,
        IDomainRepository domainRepository,
        ICountryRepository countryRepository,
        IHolidaysLeadServices holidaysLeadServices,
        ICommonMasterRepository commonMasterRepository,
        ILeadDocumentServices leadDocumentServices,
        IUserService userService, IVerticalServices verticalServices, ILeadContactRepository leadContactRepository)
    : IRequestHandler<GetAllTaskMasterQuery, BaseResult<List<TaskMasterDto>>>
    {
        public async Task<BaseResult<List<TaskMasterDto>>> Handle(GetAllTaskMasterQuery request, CancellationToken cancellationToken)
        {
            var masters = await taskMasterServices.GetAllWithChildAsync(c => c.DomainStages);
            var masterList = masters.Where(a => a.Status == 1).ToList();

            if (!masterList.Any())
                return new Error(ErrorCode.NotFound, "Task not found");

            var dtoList = new List<TaskMasterDto>();

            foreach (var master in masterList.OrderByDescending(x => x.TaskDate))
            {
                // Get lead and holiday lead
                var leadmasters = await leadRepository.GetByIdChildAsync(a => a.Id == master.LeadId, o => o.LeadContact, c => c.LeadSource, x => x.LeadStatus);
                var leadmaster = leadmasters.FirstOrDefault(a => a.Status == 1);

                var holidayLeadMasters = await holidaysLeadServices.GetByIdChildAsync(a => a.Id == master.LeadId);
                var holidayLeadMaster = holidayLeadMasters.FirstOrDefault(a => a.Status == 1);

                // Get contact
                HyBrCRM.Domain.Exchange.Entities.LeadContact contact = null;
                List<HyBrCRM.Domain.Exchange.Entities.LeadContact> cont = new();

                if (leadmaster?.LeadContactId != null)
                {
                    var contacts = await leadContactRepository.GetByIdChildAsync(a => a.Id == leadmaster.LeadContactId, c => c.Country, j => j.State, x => x.Gender);
                    cont = contacts.Where(a => a.Status == 1).ToList();
                    contact = cont.FirstOrDefault();
                }
                else if (holidayLeadMaster?.LeadContactId != null)
                {
                    var contacts = await leadContactRepository.GetByIdChildAsync(a => a.Id == holidayLeadMaster.LeadContactId, c => c.Country, j => j.State, x => x.Gender);
                    cont = contacts.Where(a => a.Status == 1).ToList();
                    contact = cont.FirstOrDefault();
                }

                var vertical = contact?.VerticalId != null ? await verticalServices.GetVerticalById(contact.VerticalId) : null;
                var leadVertical = holidayLeadMaster?.VerticalId != null ? await verticalServices.GetVerticalById(holidayLeadMaster.VerticalId) : null;

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
                    GenderName = c.Gender?.CommonName,
                    ParentsName = c.ParentsName,
                    ParentsPhoneNumber = c.ParentsPhoneNumber,
                    CountryId = c.CountryId,
                    CountryName = c.Country?.CountryName,
                    StateId = c.StateId,
                    StateName = c.StateId == "01JJ6JTZPM2N1RQBHKSGCNVE0" ? "" : c.State?.StateName,
                    StateOutsideIndia = c.StateOutsideIndia,
                    District = c.District,
                    City = c.City,
                    Locality = c.Locality,
                    VerticalId = c.VerticalId,
                    VerticalName = vertical?.Data?.VerticalName,
                    Status = master.Status
                }).FirstOrDefault();

                // Fetch lead vertical
                LeadDto dtoLead = new();
                if (leadmaster != null)
                {
                    var lead = await leadRepository.GetByIdChildAsync(a => a.Id == leadmaster.Id);
                    var leadV = lead.FirstOrDefault();
                    var leadvertical = leadV?.VericalId != null ? await verticalServices.GetVerticalById(leadV.VericalId) : null;

                    string leadStatusName = null;
                    if (leadmaster.LeadStatusId != null)
                    {
                        var domainStage = await domainStagesServices.GetByIdAsync(leadmaster.LeadStatusId);
                        if (domainStage?.StagesId != null)
                        {
                            var stage = await stagesServices.GetByIdAsync(domainStage.StagesId);
                            leadStatusName = stage?.Name;
                        }
                    }

                    dtoLead = new LeadDto
                    {
                        Id = leadmaster.Id,
                        LeadSourceId = leadmaster.LeadSourceId,
                        LeadSourceName = leadmaster.LeadSource?.CommonName,
                        LeadStatusId = leadmaster.LeadStatusId,
                        LeadStatusName = leadStatusName,
                        VerticalId = leadmaster.VericalId,
                        VerticalName = leadvertical?.Data?.VerticalName,
                        Notes = leadmaster.Notes,
                      
                        Contact = dtoContact,
                        Status = leadmaster.Status
                    };
                }

                // Holiday Lead DTO
                HoliDayLeadDto dtoHolidayLead = new();
                if (holidayLeadMaster != null)
                {
                    string holidayStatusName = null;
                    if (holidayLeadMaster.LeadStatusId != null)
                    {
                        var domainStage = await domainStagesServices.GetByIdAsync(holidayLeadMaster.LeadStatusId);
                        if (domainStage?.StagesId != null)
                        {
                            var stage = await stagesServices.GetByIdAsync(domainStage.StagesId);
                            holidayStatusName = stage?.Name;
                        }
                    }

                    string leadSourceName = null;
                    if (holidayLeadMaster.LeadSourceId != null)
                    {
                        var source = await commonMasterRepository.GetByIdAsync(holidayLeadMaster.LeadSourceId);
                        leadSourceName = source?.CommonName;
                    }

                    dtoHolidayLead = new HoliDayLeadDto
                    {
                        Id = holidayLeadMaster.Id,
                        LeadContactId = holidayLeadMaster.LeadContactId,
                        EnquiryType = holidayLeadMaster.EnquiryType,
                        TravelType = holidayLeadMaster.TravelType,
                        PrefferedDestination = holidayLeadMaster.PrefferedDestination,
                        TripDuration = holidayLeadMaster.TripDuration,
                        DepatureCity = holidayLeadMaster.DepatureCity,
                        StartDate = holidayLeadMaster.StartDate,
                        EndDate = holidayLeadMaster.EndDate,
                        Adults = holidayLeadMaster?.Adults ?? 0,
                        ChildWithBed = holidayLeadMaster?.ChildWithBed ?? 0,
                        ChildWithoutBed = holidayLeadMaster?.ChildWithoutBed ?? 0,
                        BeddingPreference = holidayLeadMaster.BeddingPreference,
                        RoomType = holidayLeadMaster.RoomType,
                        MealPlan = holidayLeadMaster.MealPlan,
                        Notes = holidayLeadMaster.Notes,
                        FollowUpDate = holidayLeadMaster.FollowUpDate,
                        AsignedAgent = holidayLeadMaster.AsignedAgent,
                        VerticalId = holidayLeadMaster.VerticalId,
                        VerticalName = leadVertical?.Data?.VerticalName,
                        Status = holidayLeadMaster.Status,
                        LeadSourceId = holidayLeadMaster.LeadSourceId,
                        LeadSourceName = leadSourceName,
                        LeadStatusId = holidayLeadMaster.LeadStatusId,
                        LeadStatusName = holidayStatusName,
                        CategoryId = holidayLeadMaster.CategoryId,
                    };
                }

                var user = await userService?.GetUserByIdAsync(master.OwnerId);
                var domainStages = await domainStagesServices?.GetByIdAsync(master.DomainStagesId);
                var domain = domainStages?.DomainId != null
                    ? (await domainRepository.GetDomainById(domainStages.DomainId))?.Data?.DomainName
                    : null;

                string countryName = null;
                if (!string.IsNullOrEmpty(master.CountryInterestedIn))
                {
                    var country = await countryRepository.GetByIdAsync(master.CountryInterestedIn);
                    countryName = country?.CountryName;
                }

                string stageName = null;
                if (domainStages?.StagesId != null)
                {
                    var stage = await stagesServices.GetByIdAsync(domainStages.StagesId);
                    stageName = stage?.Name;
                }
                var countryIds = master?.CountryInterestedIn?.Split(',')?.ToList() ?? new List<string>();

                var countryNames = new List<string>();

                foreach (var id in countryIds)
                {
                    var name = (await countryRepository.GetByIdAsync(id))?.CountryName;
                    if (!string.IsNullOrWhiteSpace(name))
                        countryNames.Add(name);
                }
                var documentData = await leadDocumentServices?.GetByIdChildAsync(a => a.TaskId == master.Id && a.Status == 1);
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
                var dto = new TaskMasterDto
                {
                    Id = master.Id,
                    LeadId = master.LeadId,
                    LeadName = contact?.FirstName,
                    OwnerId = master.OwnerId,
                    OwnerName = user?.Data?.UserName,
                    DomainStagesId = master.DomainStagesId,
                    DomainName = domain,
                    StagesName = stageName,
                    DomainStagesName = domain + "-" + stageName,
                    TaskDate = master.TaskDate,
                    TaskNote = master.TaskNote,
                    //CountryInterestedIn = master.CountryInterestedIn,
                    //CountryName = countryName,
                    CountryInterestedIn = countryIds,     // List of country IDs
                    CountryName = countryNames,
                    UniversityPreferred = master.UniversityPreferred,
                    SubDescription = master.SubDescription,
                    SubDescriptionName = stagesServices.GetByIdAsync((domainStagesServices?.GetByIdAsync(master?.SubDescription).Result?.StagesId))?.Result?.Name,
                    DepositPaidUniversity = master.DepositPaidUniversity,
                    Status = master.Status,
                    TaskStatus = master?.TaskStatus,
                    CallType = master?.CallType,
                    Lead = dtoLead,
                    LeadDocument = leadDocumentDto,
                    HolidayLead = dtoHolidayLead
                };

                dtoList.Add(dto);
            }

            return BaseResult<List<TaskMasterDto>>.Ok(dtoList);
        }


    }
}
