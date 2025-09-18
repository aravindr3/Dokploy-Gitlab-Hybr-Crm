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
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.TaskMaster.Queries.GetById
{
    internal class GetTaskByIdQueryHandler(
    ILeadRepository leadRepository,
    IVerticalServices verticalServices,
    ITaskMasterServices taskMasterServices,
    IUserService userService,
    ILeadContactRepository leadContactRepository,
    IDomainRepository domainRepository, IDomainStagesServices domainStagesServices,
    IStagesServices stagesServices ,
    ICountryRepository countryRepository,
    ILeadDocumentServices leadDocumentServices
    ) : IRequestHandler<GetTaskByIdQuery, BaseResult<TaskMasterDto>>
    {
        public async Task<BaseResult<TaskMasterDto>> Handle(GetTaskByIdQuery request,
            CancellationToken cancellationToken)
        {
            var masters = await taskMasterServices.GetByIdChildAsync(a => a.Id == request.Id,  c => c.DomainStages);
            var master = masters.Where(a => a.Status == 1).FirstOrDefault();
            if (master is null) return new Error(ErrorCode.NotFound, "Task not found");
            var userResult = await userService?.GetUserByIdAsync(master.OwnerId);
            if (userResult == null)
            {

                return new Error(ErrorCode.BadRequest, "Owner not found");
            }
            var OwnerName = userResult?.Data?.UserName;
            var domainStages = domainStagesServices?.GetByIdAsync(master.DomainStagesId);
           
            var domain = domainRepository?.GetDomainById(domainStages?.Result?.DomainId)?.Result?.Data?.DomainName;
            var stages = stagesServices?.GetByIdAsync(domainStages?.Result?.StagesId)?.Result?.Name;

            var leadmasters = await leadRepository.GetByIdChildAsync(a => a.Id == master.LeadId, o => o.LeadContact, c => c.LeadSource, x => x.LeadStatus);
            var leadmaster = leadmasters.Where(a => a.Status == 1).FirstOrDefault();
            var contacts = await leadContactRepository.GetByIdChildAsync(a => a.Id == leadmaster.LeadContactId, c => c.Country, x => x.State, y => y.Gender, e => e.State);
            var contact = contacts.Where(a => a.Status == 1).ToList();
            var contVertical = contact.FirstOrDefault();
            var verticals = await verticalServices?.GetVerticalById(contVertical?.VerticalId);
            var leadVertical = await verticalServices.GetVerticalById(leadmaster?.VericalId);

            var dtoContact = contact.Select(c => new LeadContactDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber1 = c.PhoneNumber1,
                PhoneNumber2 = c.PhoneNumber2,
                WhatsAppNumber = c.WhatsAppNumber,
                Email = c.Email,
                GenderId = c.GenderId,
                GenderName = c?.Gender?.CommonName,
                ParentsName = c?.ParentsName,
                ParentsPhoneNumber = c?.ParentsPhoneNumber,
                CountryId = c?.CountryId,
                CountryName = c?.Country?.CountryName,
                StateId = c?.StateId,
                StateName = c?.StateId == "01JJ6JTZPM2N1RQBHKSGCNVE0" ? "" : c?.State?.StateName,
                StateOutsideIndia =c?.StateOutsideIndia,
                District = c?.District,
                City = c?.City,
                Locality = c?.Locality,
                VerticalId = c?.VerticalId,
                VerticalName = verticals?.Data?.VerticalName,

                Status = master?.Status,
            }).FirstOrDefault();

            var dtoLead = new LeadDto
            {
                Id = leadmaster.Id,
                LeadSourceId = leadmaster?.LeadSourceId,
                LeadSourceName = leadmaster?.LeadSource?.CommonName,
                LeadStatusId = leadmaster?.LeadStatusId,
                LeadStatusName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(leadmaster.LeadStatusId)?.Result?.StagesId)?.Result?.Name,
                VerticalId = leadmaster?.VericalId,
                VerticalName = leadVertical?.Data?.VerticalName,
                Notes = leadmaster?.Notes,
                Contact = dtoContact,

                Status = master?.Status,
            };
            var countryIds = master?.CountryInterestedIn?.Split(',')?.ToList() ?? new List<string>();

            var countryNames = new List<string>();

            foreach (var id in countryIds)
            {
                var name = (await countryRepository.GetByIdAsync(id))?.CountryName;
                if (!string.IsNullOrWhiteSpace(name))
                    countryNames.Add(name);
            }
            var documentData = await leadDocumentServices.GetByIdChildAsync(a => a.TaskId == request.Id && a.Status == 1);
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
            var dtoTasks = new TaskMasterDto
            {
                Id = master.Id,
                  LeadId =master.LeadId,
                  LeadName = contacts?.FirstOrDefault()?.FirstName,
                    OwnerId = master?.OwnerId,
                     OwnerName = OwnerName,
            DomainStagesId  = master?.DomainStagesId,
            DomainName = domain,
            StagesName = stages,
            DomainStagesName = domain +"-"+stages,
          TaskDate = master?.TaskDate,
           TaskNote = master?.TaskNote,
           TaskStatus = master?.TaskStatus,
                //CountryInterestedIn = master?.CountryInterestedIn,
                //CountryName = countryRepository.GetByIdAsync(master?.CountryInterestedIn)?.Result?.CountryName,
                CountryInterestedIn = countryIds,     // List of country IDs
                CountryName = countryNames,
                UniversityPreferred = master?.UniversityPreferred,
           SubDescription = master?.SubDescription,
           DepositPaidUniversity= master?.DepositPaidUniversity,
                CallType = master?.CallType,

                Status = master?.Status,
           Lead = dtoLead,
           LeadDocument = leadDocumentDto,

            };
            return BaseResult<TaskMasterDto>.Ok(dtoTasks);

        }
    }
   
}
