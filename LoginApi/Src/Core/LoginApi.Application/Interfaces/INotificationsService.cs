using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Notifications.Request;
using HyBrForex.Application.DTOs.Notifications.Response;
using HyBrForex.Domain.CurrencyBoard.DTO.Request;

namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface INotificationsService
    {
        Task<List<NotificationResponse>> GetAllNotificationsAsync();
        Task<NotificationResponse> CreateNotificationAsync(NotificationRequest request);
    }
}

