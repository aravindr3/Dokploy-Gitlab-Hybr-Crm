using System.Threading.Tasks;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Application.Interfaces;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}