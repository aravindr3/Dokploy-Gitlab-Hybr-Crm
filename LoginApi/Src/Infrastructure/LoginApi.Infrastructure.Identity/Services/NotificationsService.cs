using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Notifications.Request;
using HyBrForex.Application.DTOs.Notifications.Response;
using HyBrForex.Application.Hubs;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Contexts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class NotificationsService(IdentityContext identityContext, IHubContext<NotificationHub> hubContext) : INotificationsService
    {
        public async Task<List<NotificationResponse>> GetAllNotificationsAsync()
        {
            var notifications = await identityContext.notifications
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return notifications.Select(n => new NotificationResponse
            {
                Id = n.Id,
                Message = n.Message,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        public async Task<NotificationResponse> CreateNotificationAsync(NotificationRequest request)
        {
            var notification = new Notifications
            {
                Id = Ulid.NewUlid().ToString(),
                Message = request.Message
            };

            await identityContext.notifications.AddAsync(notification);
            await identityContext.SaveChangesAsync();

            var response = new NotificationResponse
            {
                Id = notification.Id,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt
            };

            // Send real-time notification via SignalR
            await hubContext.Clients.All.SendAsync("ReceiveNotification", response);

            return response;
        }
    }
}
