using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using Newtonsoft.Json;

namespace HyBrCRM.Application.Features.LeadProperyDefinition.Create
{
    public class CreateLeadPropertyDefinitionCommandHandler(
    IleadPropertyDefinitionServices leadPropertyDefinitionServices,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateLeadProperyDefinitionCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateLeadProperyDefinitionCommand request,
       CancellationToken cancellationToken)
        {
            var master = new Domain.Exchange.Entities.LeadProperyDefinition(

                request?.Domain,
                request?.FieldName,
                request?.DisplayName,
                request?.DataType,
                request?.IsRequired

            


                );
            master.Id = Ulid.NewUlid().ToString();
            await leadPropertyDefinitionServices.AddAsync(master);
            await unitOfWork.SaveChangesAsync();

            return master.Id;
        }
    
    
    }
}
