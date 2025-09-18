using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Notifications.Request;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Infrastructure.Identity.Services;
using LoginApi.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class NotificationsController(INotificationsService notificationsService) : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await notificationsService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
                return BadRequest("Message cannot be empty.");

            var notification = await notificationsService.CreateNotificationAsync(request);
            return Ok(notification);
        }
    }
}
