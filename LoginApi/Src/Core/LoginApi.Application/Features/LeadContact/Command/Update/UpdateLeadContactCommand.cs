using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadContact.Command.Update
{
    public class UpdateLeadContactCommand : IRequest<BaseResult>
    {
        public string ? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string? Email { get; set; }
        public string? GenderId { get; set; }
        public string? ParentsName { get; set; }
        public string? ParentsPhoneNumber { get; set; }
        public string? CountryId { get; set; }
        public string? StateId { get; set; }
        public string ? StateOutsideIndia { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Locality { get; set; }
        public string? VerticalId { get; set; }
    }
}
