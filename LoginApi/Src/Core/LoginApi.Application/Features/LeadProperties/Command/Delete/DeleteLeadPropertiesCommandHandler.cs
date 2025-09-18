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

namespace HyBrCRM.Application.Features.LeadProperties.Command.Delete
{
    public class DeleteLeadPropertiesCommandHandler(
    ILeadPropertiesServices leadPropertiesServices,
    ILeadPropertiesValueServices leadPropertiesValueServices,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteleadPropertiesCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteleadPropertiesCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.LeadId))
            {
                return BaseResult.Failure(new Error(ErrorCode.BadRequest, "LeadId is required", nameof(request.LeadId)));
            }

            // Fetch all lead property values for the given LeadId
            var propertyValues = (await leadPropertiesValueServices.GetAllAsync())
                .Where(x => x.LeadId == request.LeadId && x.Status == 1)
                .ToList();

            if (!propertyValues.Any())
            {
                return BaseResult.Failure(new Error(ErrorCode.NotFound, "No lead property values found for the given LeadId.", nameof(request.LeadId)));
            }

            // Soft-delete all values (set Status = 0)
            foreach (var value in propertyValues)
            {
                value.Status = 0;
                leadPropertiesValueServices.Update(value);
            }

            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }

    }

}
