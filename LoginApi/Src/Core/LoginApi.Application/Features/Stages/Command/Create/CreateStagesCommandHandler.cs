using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Features.StateMaster.Command;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrCRM.Application.Features.Stages.Command.Create
{
    public class CreateStagesCommandHandler(IStagesServices stagesServices, IUnitOfWork unitOfWork) : IRequestHandler<CreateStagesCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateStagesCommand request,
          CancellationToken cancellationToken)
        {
            var state = new Domain.Exchange.Entities.Stages
                (request.Name);
            state.Id = Ulid.NewUlid().ToString();
            await stagesServices.AddAsync(state);
            await unitOfWork.SaveChangesAsync();
            return state.Id;
        }
    }
}
