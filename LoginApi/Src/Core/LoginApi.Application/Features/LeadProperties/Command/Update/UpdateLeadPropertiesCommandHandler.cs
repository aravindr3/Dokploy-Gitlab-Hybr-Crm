using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperties.Command.Update
{
    public class UpdateLeadPropertiesCommandHandler(
    ILeadPropertiesServices leadPropertiesServices,
    IleadPropertyDefinitionServices leadPropertyDefinitionServices,
    ILeadPropertiesValueServices leadPropertiesValueServices,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateLeadProperties, BaseResult>
    {
        //public async Task<BaseResult> Handle(UpdateLeadProperties request, CancellationToken cancellationToken)
        //{
        //    var master = await leadPropertiesServices.GetByIdAsync(request.Id);

        //    if (master is null || master.Status == 0) return new Error(ErrorCode.NotFound, "Lead properties not found", nameof(request.Id));

        //    master.Update(
        //                     request.OwnerId,
        //   request.LeadId,
        //   request.CountryInterestedIn,
        //   request.DocumentUploadStatus,
        //    request.UniversityPreferred,
        //   request.OfferLetterStatus,
        //   request.DepositPaidUniversity,
        //  request.VisaStatus,
        //  request.RefundStatus,
        //   request.FutureIntake
        //        );
        //    await unitOfWork.SaveChangesAsync();

        //    return BaseResult.Ok();
        //}
        public async Task<BaseResult> Handle(UpdateLeadProperties request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.LeadId) || string.IsNullOrWhiteSpace(request.Domain))
            {
                return BaseResult.Failure(new Error(ErrorCode.BadRequest, "LeadId and Domain are required."));
            }

            var definitions = (await leadPropertyDefinitionServices.GetAllAsync())
                .Where(d => d.Domain == request.Domain)
                .ToList();

            if (!definitions.Any())
            {
                return BaseResult.Failure(new Error(ErrorCode.BadRequest, $"No property definitions found for domain '{request.Domain}'."));
            }

            var existingValues = (await leadPropertiesValueServices.GetAllAsync()) // MUST be tracked
                .Where(v => v.LeadId == request.LeadId)
                .ToList();

            foreach (var def in definitions)
            {
                request.Properties.TryGetValue(def.Id, out var newValue);

                if (!IsValidType(def.DataType, newValue))
                {
                    return BaseResult.Failure(new Error(ErrorCode.BadRequest, $"Invalid value for field '{def.DisplayName}'."));
                }

                var existing = existingValues.FirstOrDefault(v => v.PropertyDefinitionId == def.Id);

                if (existing != null)
                {
                    // Optional: Only update if value changed
                    if (existing.Value != newValue)
                    {
                       
                        existing.Value = newValue;
                        leadPropertiesValueServices.Update(existing);
                        await unitOfWork.SaveChangesAsync();
                           
                       //existing.Update(request.LeadId, request.OwnerId, def.Id, newValue);
                        //await leadPropertiesValueServices.AddAsync(newEntry);

                    }
                }
                else
                {
                    var newEntry = new LeadProperyValue(request.LeadId, request.OwnerId, def.Id, newValue);
                    await leadPropertiesValueServices.AddAsync(newEntry);
                }
            }

            await unitOfWork.SaveChangesAsync();
            return BaseResult.Ok();
        }

        private bool IsValidType(string type, string? value)
        {
            if (value == null) return true;

            return type.ToLower() switch
            {
                "string" => true,
                "bool" => bool.TryParse(value, out _),
                "int" => int.TryParse(value, out _),
                "datetime" => DateTime.TryParse(value, out _),
                _ => false
            };
        }


    }

}
