using System.Threading.Tasks;

namespace HyBrForex.Application.Interfaces;

public interface IAccountsUnitOfWork
{
    Task<bool> SaveChangesAsync();
}