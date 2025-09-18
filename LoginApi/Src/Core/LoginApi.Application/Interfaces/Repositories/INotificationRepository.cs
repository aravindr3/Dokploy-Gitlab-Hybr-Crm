using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.CurrencyBoard.DTO.Properties;
using HyBrForex.Domain.CurrencyBoard.DTO.Request;
using HyBrForex.Domain.CurrencyBoard.DTO.Response;
using HyBrForex.Domain.CurrencyBoard.Entities;

namespace HyBrForex.Application.Interfaces.Repositories;

public interface INotificationRepository : IGenericRepository<NotificationDtl>
{
    Task<BaseResult<string>> AddNotifications(NotificationAddRequest request);

    Task<BaseResult<string>> UpdateNotifications(NotificationUpdateRequest request);

    Task<BaseResult<NotificationResponse>> GetNotifications();

    Task<BaseResult<NotificationProperties>> GetNotificationById(string NotificationId);
}