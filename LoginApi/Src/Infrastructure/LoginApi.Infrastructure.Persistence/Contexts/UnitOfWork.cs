using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Infrastructure.Persistence.Contexts;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync()
    {
        dbContext.ChangeTracker.DetectChanges();
        return await dbContext.SaveChangesAsync() > 0;
    }

    public bool SaveChanges()
    {
        dbContext.ChangeTracker.DetectChanges();

        return dbContext.SaveChanges() > 0;
    }

   
}