using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Query.GetById;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.HolidaysLead.Query.GetById
{
    public class GetHolidayLeadByIdQueryHandler(
        IHolidaysLeadServices holidaysLeadServices,
    ILeadContactRepository leadContactRepository,
    ILeadPropertiesServices leadPropertiesServices,
    IVerticalServices verticalServices,
    ICountryRepository countryRepository,
    IUserService userService,
    IDomainStagesServices domainStagesServices,
    ICommonMasterRepository commonMasterRepository,
    IStagesServices stagesServices,
    ISubDomainServices subDomainServices,
    ITaskMasterServices taskMasterServices,
    ILeadDocumentServices leadDocumentServices
    ) : IRequestHandler<GetHolidayleadById, BaseResult<HoliDayLeadDto>>
    {
        public async Task<BaseResult<HoliDayLeadDto>> Handle(GetHolidayleadById request,
            CancellationToken cancellationToken)
        {
            var masters = await holidaysLeadServices.GetByIdChildAsync(a => a.Id == request.Id);
            var master = masters.Where(a => a.Status == 1).FirstOrDefault();
            if (master is null) return new Error(ErrorCode.NotFound, "Lead  not found");
            var contacts = await leadContactRepository.GetByIdChildAsync(a => a.Id == master.LeadContactId, c => c.Country, x => x.State, y => y.Gender, e => e.State);
            var contact = contacts.Where(a => a.Status == 1).ToList();
            var contVertical = contact.FirstOrDefault();
            var verticals = await verticalServices?.GetVerticalById(contVertical?.VerticalId);
            var leadVertical = await verticalServices.GetVerticalById(master?.VerticalId);


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
                VerticalName = verticals?.Data?.VerticalName,

                Status = master?.Status,
            }).FirstOrDefault();


            var taskList = await taskMasterServices.GetByIdChildAsync(a => a.LeadId == master.Id);

            //        var lastTask = taskList
            //.Where(t => t.Status == 1)
            //.OrderByDescending(t =>  t.TaskDate ?? DateTime.MinValue)
            //.FirstOrDefault();

            var lastTask = taskList
    .Where(t => t.Status == 1)
    .OrderByDescending(t => new[] { t.LastModified, t.Created }
        .Where(d => d.HasValue)
        .Select(d => d.Value)
        .DefaultIfEmpty(DateTime.MinValue)
        .Max())
    .FirstOrDefault();

            var domainStage = await domainStagesServices?.GetByIdAsync(lastTask?.DomainStagesId);

            // 2. Then get the Stages by Id using lambda
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
            var dtoTasks = new HoliDayLeadDto
            {

                Id = master.Id,
                LeadContactId = master.LeadContactId,
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
                AsignedAgent = userService.GetUserByIdAsync(master?.AsignedAgent)?.Result?.Data?.UserName,
                //AsignedAgent = master?.AsignedAgent,
                VerticalId = master?.VerticalId,
                VerticalName = leadVertical?.Data?.VerticalName,

                Status = master?.Status,
                LeadSourceId = master?.LeadSourceId,
                LeadSourceName = commonMasterRepository.GetByIdAsync(master.LeadSourceId)?.Result?.CommonName,
                LeadStatusId = master?.LeadStatusId,
                CategoryId = master?.CategoryId,
                Created = master?.Created,
                LastTask = lastTask?.Created,
                CategoryName = subDomainServices?.GetByIdAsync(master?.CategoryId)?.Result?.CategoryName,
                Stage = stagesName,

                LeadStatusName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(master.LeadStatusId)?.Result?.StagesId)?.Result?.Name,
                Other = master?.Other,
                Contact = dtoContact,
                LeadDocument = leadDocumentDto


            };
            return BaseResult<HoliDayLeadDto>.Ok(dtoTasks);

        }
    }
   
  
}
