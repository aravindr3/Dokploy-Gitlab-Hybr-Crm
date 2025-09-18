using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Commands.CreateCommonMaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using Microsoft.VisualBasic;

namespace HyBrCRM.Application.Features.Bonvoice.Command.Create
{
    public class CreateBonvoiceCommandHandler(
        IInBondCallServices inbondCallServices ,
   
    IUnitOfWork unitOfWork) : IRequestHandler<CreateBonvoiceCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateBonvoiceCommand request,
            CancellationToken cancellationToken)
        {
            var masters = new InBondCall(
              request.Direction ,
            request.SourceNumber,
            request.DestinationNumber,
            request.DisplayNumber,
            request.StartTime,
            request.EndTime,
            request.CallDuration,
            request.Status,
            request.DataSource,
            request.CallType,
            request.DTMF,
            request.AccountID,
            request.ResourceURL
                )
            {
                Id = Ulid.NewUlid().ToString()
            };

            await inbondCallServices.AddAsync(masters);
            await unitOfWork.SaveChangesAsync();

            return masters.Id;

        }
    
    }
}
