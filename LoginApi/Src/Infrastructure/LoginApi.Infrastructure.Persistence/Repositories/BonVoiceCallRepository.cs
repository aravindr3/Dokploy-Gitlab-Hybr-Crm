using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Bonvoice.DTO.Request;
using HyBrCRM.Domain.Bonvoice.DTO.Response;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using HyBrForex.Infrastructure.Persistence.Contexts;
using HyBrForex.Infrastructure.Persistence.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using MediatR;

namespace HyBrCRM.Infrastructure.Persistence.Repositories
{
    public class BonVoiceCallRepository : GenericRepository<BonvoiceCallLog>, IBonVoiceCallServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBonVoiceCallServices _bonVoiceCallServices;
      


        public BonVoiceCallRepository(
     ApplicationDbContext dbContext,
     IHttpClientFactory httpClientFactory,
     IUnitOfWork unitOfWork
 ) : base(dbContext)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<BaseResult<AuthResponse>> AuthenticateBonvoiceAsync(AuthRequest request)
        {
            var client = _httpClientFactory.CreateClient("BonvoiceClient");

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("usermanagement/external-auth/", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var authData = JsonConvert.DeserializeObject<AuthResponse>(responseBody);
                var master = new BonvoiceCallLog(
                    request.LeadId,
                    authData.Data.Token,
                    authData.Data.TokenType,
                    authData.Data.HeaderKey,
                    authData.Data.HeaderValue,
                    authData.Status,
                    request?.TaskId,
                    ""
                )
                {
                    Id = Ulid.NewUlid().ToString()
                };

                await AddAsync(master); // ✅ FIXED
                await _unitOfWork.SaveChangesAsync(); // ✅ Still needed
                authData.AuthId = master.Id;
                return new BaseResult<AuthResponse>
                {
                    Success = true,
                    Data = authData
                };
            }
            catch (Exception)
            {
                return new BaseResult<AuthResponse>
                {
                    Success = false,
                };
            }
        }



       
        public async Task<BaseResult<string>> AutoCallBridgeAsync(AuthRequest authRequest, AutoCallRequestDto callRequest)
        {
            var authResult = await AuthenticateBonvoiceAsync(authRequest);

            if (!authResult.Success || authResult.Data?.Data == null)
            {
                return new BaseResult<string>
                {
                    Success = false,
                };
            }

            var token = authResult.Data.Data.Token;
            var authId = authResult?.Data?.AuthId;

            var client = _httpClientFactory.CreateClient("BonvoiceClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            var json = JsonConvert.SerializeObject(callRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                //var response = await client.PostAsync("autoDialManagement/autoCallBridging/", content);
                //var responseBody = await response.Content.ReadAsStringAsync();

                //if (!response.IsSuccessStatusCode)
                //{
                //    return new BaseResult<string>
                //    {
                //        Success = false,
                //    };
                //}

                //var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);
                //if (parsed != null && parsed.ContainsKey("error"))
                //{
                //    return new BaseResult<string>
                //    {
                //        Success = false,
                //    };
                //}
                var masterupdates = await GetByIdChildAsync(a => a.Id == authId);
                var masterupdate = masterupdates.FirstOrDefault();

                if (masterupdate != null)
                {
                    masterupdate.EventId = callRequest.EventID;
                    masterupdate.CallRequest = json;
                    //masterupdate.CallResponse = responseBody;
                    Update(masterupdate);
                    await _unitOfWork.SaveChangesAsync();
                }

                return new BaseResult<string>
                {
                    Success = true,
                    //Data = responseBody
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<string>
                {
                    Success = false,
                };
            }
        }


    }
}

