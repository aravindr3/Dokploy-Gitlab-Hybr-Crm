using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;

namespace HyBrForex.Infrastructure.Persistence.Repositories;

public class EmployeeRepository(ApplicationDbContext dbContext)
    : GenericRepository<EmployeeMaster>(dbContext), IEmployeeRepository
{
}