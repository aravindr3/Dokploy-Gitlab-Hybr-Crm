//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using HyBrForex.Application.Repositories.Repositories;
//using HyBrForex.Domain.CurrencyBoard.Entities;
//using LoginApi.Application.Features.Products.Commands.CreateProduct;
//using LoginApi.Application.Interfaces;
//using LoginApi.Application.Interfaces.Repositories;
//using HyBrForex.Application.Wrappers
//using LoginApi.Domain.Products.Entities;
//using MediatR;

//namespace HyBrForex.Application.Features.CurrencyBoard.Commands.CreateNotification
//{
//    public  class CreateNotificationCommandHandler(ICurrencyBoardRepository currencyBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateNotificationCommand, BaseResult<Ulid>>
//    {
//        public async Task<BaseResult<Ulid>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
//        {
//            var allNot = await  currencyBoardRepository.GetAllAsync();
//            var index = allNot.Count == 0 ? 1 : allNot.Max(a => a.Index);
//            var notificationDtl   = new NotificationDtl(index, request.Notification);
//            await currencyBoardRepository.AddAsync(notificationDtl);
//            await unitOfWork.SaveChangesAsync();

//            return notificationDtl.Id;
//        }
//    }
//}

