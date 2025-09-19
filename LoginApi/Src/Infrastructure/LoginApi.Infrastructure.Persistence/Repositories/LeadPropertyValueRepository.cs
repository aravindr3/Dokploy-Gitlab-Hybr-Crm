using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;
using HyBrForex.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HyBrCRM.Infrastructure.Persistence.Repositories
{
    //public class LeadPropertyValueRepository (ApplicationDbContext dbContext) :GenericRepository<LeadProperyValue>(dbContext),ILeadPropertiesValueServices
    //{
    //    public async Task<LeadProperyValue?> GetByLeadAndPropertyAsync(string leadId, string propertyDefinitionId)
    //    {
    //        return await _dbContext.LeadPropertyValues
    //            .FirstOrDefaultAsync(x => x.LeadId == leadId && x.PropertyDefinitionId == propertyDefinitionId);
    //    }
    //}
    public class LeadPropertyValueRepository
    : GenericRepository<LeadProperyValue>, ILeadPropertiesValueServices
    {
        private readonly ApplicationDbContext _dbContext;

        public LeadPropertyValueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeadProperyValue?> GetByLeadAndPropertyAsync(string leadId, string propertyDefinitionId)
        {
            return await _dbContext.LeadProperyValues
                .FirstOrDefaultAsync(x => x.LeadId == leadId && x.PropertyDefinitionId == propertyDefinitionId);
        }
    }

}
