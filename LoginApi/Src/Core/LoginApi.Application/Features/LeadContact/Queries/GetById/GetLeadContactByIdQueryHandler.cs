using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Features.CommonMaster.Queries.GetCommonMasterById;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrCRM.Application.Features.LeadContact.Queries.GetById
{
    public class GetLeadContactByIdQueryHandler(
    ILeadContactRepository leadContactRepository,
    IVerticalServices verticalServices
    ) : IRequestHandler<GetLeadCotactByIdQuery, BaseResult<LeadContactDto>>
    {
        public async Task<BaseResult<LeadContactDto>> Handle(GetLeadCotactByIdQuery request,
            CancellationToken cancellationToken)
        {
            var masters = await leadContactRepository.GetByIdChildAsync(a=> a.Id == request.Id , o => o.Gender, c => c.Country, x => x.State);
            var master = masters.Where(a => a.Status == 1).FirstOrDefault();
            if (master is null) return new Error(ErrorCode.NotFound, "Lead Contact not found");
            var verticals = await verticalServices?.GetVerticalById(master?.VerticalId);
            

            var dtoTasks =  new LeadContactDto
            {
                Id = master.Id,
                FirstName = master.FirstName,
                LastName = master.LastName,
                PhoneNumber1 = master.PhoneNumber1,
                PhoneNumber2 = master.PhoneNumber2,
                WhatsAppNumber = master.WhatsAppNumber,
                Email = master.Email,
                GenderId = master.GenderId,
                GenderName = master?.GenderId == "01JXVQH4EANY2GCTS8PDR5PWKS1"? "" : master?.Gender?.CommonName,
                ParentsName = master?.ParentsName,
                ParentsPhoneNumber = master?.ParentsPhoneNumber,
                CountryId = master?.CountryId == "01JJ6JTZPM2N1RQBHKSGCNVE010" ? "" : master?.CountryId,
                CountryName = master?.CountryId == "01JJ6JTZPM2N1RQBHKSGCNVE010" ? "" : master?.Country?.CountryName,
                StateId = master?.StateId,
                StateOutsideIndia = master?.StateOutsideIndia,
                StateName = master?.StateId == "01JJ6JTZPM2N1RQBHKSGCNVE0" ? "" : master?.State?.StateName,
                District = master?.District,
                City = master?.City,
                Locality = master?.Locality,
                VerticalId = master?.VerticalId,
                VerticalName = verticals?.Data?.VerticalName,

                Status = master?.Status,
            };
            return BaseResult<LeadContactDto>.Ok(dtoTasks);

        }
    }
    }
   

