using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Features.CommonMaster.Queries.GetAllCommonMaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrCRM.Application.Features.LeadContact.Queries.GetAll
{
    public class GetAllLeadContactQueryHandler(ILeadContactRepository leadContactRepository, IVerticalServices verticalServices)
    : IRequestHandler<GetAllLeadContactQuery, BaseResult<List<LeadContactDto>>>
    {
        public async Task<BaseResult<List<LeadContactDto>>> Handle(GetAllLeadContactQuery request, CancellationToken cancellationToken)
        {
            var masters = await leadContactRepository.GetAllWithChildAsync(o => o.Gender, c => c.Country, x => x.State);
            var masterList = masters.Where(a => a.Status == 1).ToList();

            if (!masterList.Any()) return new Error(ErrorCode.NotFound, "Lead Contact not found");

            var dtoList = new List<LeadContactDto>();

            foreach (var contact in masterList)
            {
                var vertical = await verticalServices?.GetVerticalById(contact?.VerticalId);
                var dto = new LeadContactDto
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
                    CountryName = contact?.CountryId == "01JJ6JTZPM2N1RQBHKSGCNVE010" ? "": contact?.Country?.CountryName,
                    StateId = contact?.StateId,
                    StateOutsideIndia = contact?.StateOutsideIndia,
                    StateName = contact?.StateId == "01JJ6JTZPM2N1RQBHKSGCNVE0" ? "" : contact?.State?.StateName,
                    District = contact?.District,
                    City = contact?.City,
                    Locality = contact?.Locality,
                    VerticalId = contact?.VerticalId,
                    VerticalName = vertical?.Data?.VerticalName,
                    Status = contact?.Status,
                };
                dtoList.Add(dto);
            }

            return BaseResult<List<LeadContactDto>>.Ok(dtoList);
        }


    }
}
