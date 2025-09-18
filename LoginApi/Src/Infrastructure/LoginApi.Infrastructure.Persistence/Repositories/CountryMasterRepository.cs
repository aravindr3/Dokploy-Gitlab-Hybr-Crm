using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;

namespace HyBrForex.Infrastructure.Persistence.Repositories;

public class CountryMasterRepository(ApplicationDbContext dbContext)
    : GenericRepository<CountryMaster>(dbContext), ICountryRepository
{
}