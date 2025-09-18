using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Domain.Bonvoice.DTO.Request;
using HyBrCRM.Domain.Bonvoice.DTO.Response;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;

namespace HyBrCRM.Application.Interfaces.Repositories
{
    public interface IBonVoiceCallServices
        : IGenericRepository<BonvoiceCallLog>
    {
        Task<BaseResult<AuthResponse>> AuthenticateBonvoiceAsync(AuthRequest request);
        Task<BaseResult<string>> AutoCallBridgeAsync(AuthRequest authRequest, AutoCallRequestDto callRequest);

    }
}
