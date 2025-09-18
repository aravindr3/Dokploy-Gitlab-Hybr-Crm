using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrForex.Application.Features.StateMaster.Command
{
    public class CreateStateMasterCommandHandler(
    IStateMaster stateMaster,
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateStateMastercommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateStateMastercommand request,
            CancellationToken cancellationToken)
        {
            var state = new Domain.Exchange.Entities.StateMaster(request.StateCode , request.StateName , request.GSTCode);
            state.Id = Ulid.NewUlid().ToString();
            await stateMaster.AddAsync(state);
            await unitOfWork.SaveChangesAsync();
            return state.Id;
        }
    }



    
}
