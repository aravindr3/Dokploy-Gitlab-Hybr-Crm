using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.CurrencyBoard.DTO.Properties;
using HyBrForex.Domain.CurrencyBoard.DTO.Request;
using HyBrForex.Domain.CurrencyBoard.DTO.Response;
using HyBrForex.Domain.CurrencyBoard.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;

namespace HyBrForex.Infrastructure.Persistence.Repositories;

public class NotificationRepository(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
    : GenericRepository<NotificationDtl>(applicationDbContext), INotificationRepository
{
    public async Task<BaseResult<string>> AddNotifications(NotificationAddRequest request)
    {
        var notificationDtl = new NotificationDtl();

        var allNot = await GetAllAsync();
        var index = allNot.Count == 0 ? 1 : allNot.Max(a => a.Index) + 1;
        notificationDtl = new NotificationDtl(index, request.Notification);
        notificationDtl.Id = Ulid.NewUlid().ToString();
        await AddAsync(notificationDtl);
        await unitOfWork.SaveChangesAsync();
        return BaseResult<string>.Ok(notificationDtl.Id);
    }

    public async Task<BaseResult<string>> UpdateNotifications(NotificationUpdateRequest request)
    {
        var notificationDtl = new NotificationDtl();
        if (request.Type == 1)
        {
            notificationDtl = await GetByIdAsync(request.NotificationId);
            if (notificationDtl != null) notificationDtl.Notificationtext = request.Notification;
            Update(notificationDtl);
            await unitOfWork.SaveChangesAsync();
        }
        else
        {
            notificationDtl = await GetByIdAsync(request.NotificationId);
            if (notificationDtl != null) notificationDtl.Status = request.Status;
            Update(notificationDtl);
            await unitOfWork.SaveChangesAsync();
        }

        return BaseResult<string>.Ok(notificationDtl.Id);
    }

    public async Task<BaseResult<NotificationProperties>> GetNotificationById(string NotificationId)
    {
        var notificationDtl = await GetByIdAsync(NotificationId);
        var notification = new NotificationProperties();
        if (notificationDtl != null)
            notification = new NotificationProperties
            {
                Index = notificationDtl.Index,
                NotificationId = notificationDtl.Id,
                Notification = notificationDtl.Notificationtext,
                Status = notificationDtl.Status
            };

        return BaseResult<NotificationProperties>.Ok(notification);
    }

    public async Task<BaseResult<NotificationResponse>> GetNotifications()
    {
        var response = new NotificationResponse();
        var notifications = new List<NotificationProperties>();
        var allNot = await GetAllAsync();
        if (allNot.Count > 0)
        {
            notifications =
            [
                .. allNot.Select(a => new NotificationProperties
                    {
                        Index = a.Index, Notification = a.Notificationtext, Status = a.Status, NotificationId = a.Id
                    })
                    .OrderBy(a => a.Index)
            ];
            response.notifications = notifications;
        }

        return BaseResult<NotificationResponse>.Ok(response);
    }
}