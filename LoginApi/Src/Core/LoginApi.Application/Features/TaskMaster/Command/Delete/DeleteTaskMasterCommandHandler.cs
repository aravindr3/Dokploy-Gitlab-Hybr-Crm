using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Delete;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.TaskMaster.Command.Delete
{
    public class DeleteTaskMasterCommandHandler(
    ITaskMasterServices taskMasterServices,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteTaskMasterCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteTaskMasterCommand request, CancellationToken cancellationToken)
        {
            var master = await taskMasterServices.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Task  is not found", nameof(request.Id));
            master.Status = 0;
            taskMasterServices.Update(master);
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
   
}
