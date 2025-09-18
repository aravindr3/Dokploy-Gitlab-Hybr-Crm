using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;
using HyBrForex.Infrastructure.Persistence.Repositories;

namespace HyBrCRM.Infrastructure.Persistence.Repositories
{
    public class ActivityLogRepository (ApplicationDbContext dbContext): GenericRepository<ActivityLog>(dbContext),IActivityLogServices
    {
    }
}
