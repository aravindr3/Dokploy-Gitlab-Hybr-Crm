using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperyDefinition.Create
{
    public class CreateLeadProperyDefinitionCommand : IRequest<BaseResult<string>>
    {
        public string? Domain { get; set; }
        public string? FieldName { get; set; }
        public string? DisplayName { get; set; }
        public string? DataType { get; set; }
        public bool? IsRequired { get; set; }
    }
}
