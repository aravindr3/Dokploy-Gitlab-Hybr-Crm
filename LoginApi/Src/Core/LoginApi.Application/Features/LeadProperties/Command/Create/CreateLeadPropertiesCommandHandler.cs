using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using Newtonsoft.Json.Linq;

namespace HyBrCRM.Application.Features.LeadProperties.Command.Create
{
    public class CreateLeadPropertiesCommandHandler(
    ILeadPropertiesServices leadPropertiesServices,
    ILeadPropertiesValueServices leadPropertiesValueServices,
    IleadPropertyDefinitionServices leadPropertyDefinitionServices,

    IUnitOfWork unitOfWork) : IRequestHandler<CreateLeadPropertiesCommand, BaseResult<string>>
    {
        // public async Task<BaseResult<string>> Handle(CreateLeadPropertiesCommand request,
        //CancellationToken cancellationToken)
        // {
        //     var master = new Domain.Exchange.Entities.LeadProperties(
        //      request. OwnerId ,
        //    request.LeadId ,
        //    request.CountryInterestedIn,
        //    request.DocumentUploadStatus,
        //     request. UniversityPreferred,
        //    request.OfferLetterStatus ,
        //    request.DepositPaidUniversity ,
        //   request.VisaStatus ,
        //   request.RefundStatus ,
        //    request.FutureIntake


        //         );
        //     master.Id = Ulid.NewUlid().ToString();
        //     await leadPropertiesServices.AddAsync(master);
        //     await unitOfWork.SaveChangesAsync();

        //     return master.Id;
        // }

        public async Task<BaseResult<string>> Handle(CreateLeadPropertiesCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.OwnerId) ||
                string.IsNullOrWhiteSpace(request.LeadId) ||
                string.IsNullOrWhiteSpace(request.Domain))
            {
                return BaseResult<string>.Failure("OwnerId, LeadId, and Domain are required.");
            }

            var definitions = (await leadPropertyDefinitionServices
    .GetAllWithChildAsync()) // load all with children
    .Where(x => x.Domain == request.Domain)
    .ToList();

            if (!definitions.Any())
            {
                return BaseResult<string>.Failure($"No property definitions found for domain '{request.Domain}'.");
            }

            var propertyValues = new List<LeadProperyValue>();

            foreach (var def in definitions)
            {
                request.Properties.TryGetValue(def.Id, out var rawValue);


                if (!IsValidType(def.DataType, rawValue))
                {
                    return BaseResult<string>.Failure($"Invalid value for field '{def.DisplayName}'.");
                }

                propertyValues.Add(new LeadProperyValue
                {
                    Id = Ulid.NewUlid().ToString(),
                    OwnerId = request?.OwnerId,
                    LeadId = request.LeadId!,
                    PropertyDefinitionId = def.Id,
                    Value = rawValue
                });
            }

            foreach (var value in propertyValues)
            {
                await leadPropertiesValueServices.AddAsync(value);
            }

            await unitOfWork.SaveChangesAsync();

            return BaseResult<string>.Ok(request.LeadId!); // ✅ Correct return
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


