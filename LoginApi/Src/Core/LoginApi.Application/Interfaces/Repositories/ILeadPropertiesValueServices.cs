using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.Repositories;

namespace HyBrCRM.Application.Interfaces.Repositories
{
    public interface ILeadPropertiesValueServices : IGenericRepository<LeadProperyValue>
    {
        Task<LeadProperyValue?> GetByLeadAndPropertyAsync(string leadId, string propertyDefinitionId);
    }
}
