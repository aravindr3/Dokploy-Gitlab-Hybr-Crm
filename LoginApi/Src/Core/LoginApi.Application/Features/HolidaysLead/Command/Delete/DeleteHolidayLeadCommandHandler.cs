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

namespace HyBrCRM.Application.Features.HolidaysLead.Command.Delete
{
    public class DeleteHolidayLeadCommandHandler(
    IHolidaysLeadServices holidays,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteHolidayLeadCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteHolidayLeadCommand request, CancellationToken cancellationToken)
        {
            var master = await holidays.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Lead  is not found", nameof(request.Id));
            master.Status = 0;
            holidays.Update(master);
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
  
}
