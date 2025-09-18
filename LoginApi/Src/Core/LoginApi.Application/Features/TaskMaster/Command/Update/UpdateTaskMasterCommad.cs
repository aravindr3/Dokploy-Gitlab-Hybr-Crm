using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.TaskMaster.Command.Update
{
    public class UpdateTaskMasterCommad :IRequest<BaseResult>
    {
        public string ? Id { get; set; }
        public string? LeadId { get; set; }
        public string? OwnerId { get; set; }
        public string? DomainStagesId { get; set; }
        public DateTime? TaskDate { get; set; }
        public string? TaskNote { get; set; }
        //public string? CountryInterestedIn { get; set; }
        public List<string>? CountriesInterestedIn { get; set; }

        public string? UniversityPreferred { get; set; }
        public string? SubDescription { get; set; }
        public string? DepositPaidUniversity { get; set; }
        public string ? TaskStatus { get; set; }
        public string ? CallType { get; set; }
        public string ? DomainId { get; set; }
        public List<string> IdTypes { get; set; }

    }
}
