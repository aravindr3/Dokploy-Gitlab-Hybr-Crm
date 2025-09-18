using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;

namespace HyBrForex.Infrastructure.Persistence.Repositories
{
    public class IStateMasterRepository(ApplicationDbContext dbContext)
    : GenericRepository<StateMaster>(dbContext), IStateMaster
    {
    }
}
