using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;

namespace HyBrForex.Infrastructure.Persistence.Repositories;

public class CurrencyRepository(ApplicationDbContext dbContext)
    : GenericRepository<CurrencyMaster>(dbContext), ICurrencyRepository
{
}