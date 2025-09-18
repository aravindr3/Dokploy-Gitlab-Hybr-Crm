using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.DTOs.Domain.Request;
using HyBrCRM.Application.DTOs.Verticals.Data;
using HyBrCRM.Application.DTOs.Verticals.Request;
using HyBrCRM.Application.DTOs.Verticals.Response;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Infrastructure.Identity.Models;
using HyBrForex.Application.DTOs.Branch.Data;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.DTOs.Branch.Response;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using LoginApi.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HyBrCRM.Infrastructure.Identity.Services
{
    public class VerticalServices(IdentityContext identityContext , ITenantService tenantService , IDomainRepository domainRepository):IVerticalServices
    {
        public async Task<BaseResult<string>> CreateVertical(VerticalRequest model)
        {



            var newVertical = new Vertical
            {
                TenantId = model.TenantId,
                DomainId = model.DomainId,
                VerticalName = model.VerticalName,

            };
            newVertical.Id = Ulid.NewUlid().ToString();

            await identityContext.AddAsync(newVertical);
            await identityContext.SaveChangesAsync();
            return newVertical.Id;

            return BaseResult<string>.Ok("Succsessfully Inserted");
        }


        public async Task<BaseResult> DeleteVertical(string id)
        {
            var vertical = await identityContext.Vertical.FindAsync(id);

            if (vertical == null) return new Error(ErrorCode.NotFound, "Vertical is not found");
            vertical.Status = 0;
            identityContext.Update(vertical);

            await identityContext.SaveChangesAsync();

            return BaseResult.Ok();
        }



        public async Task<BaseResult> UpdateVertical(string id, VerticalRequest model)
        {
            var vertical = await identityContext.Vertical.FindAsync(id);

            if (vertical == null || vertical.Status == 0) return new Error(ErrorCode.NotFound, "Vertical is not found");
            vertical.TenantId = model.TenantId;
            vertical.DomainId = model.DomainId;
            vertical.VerticalName = model.VerticalName;

            await identityContext.SaveChangesAsync();
            return BaseResult.Ok();
        }


        public async Task<BaseResult<VerticalData>> GetVerticalById(string id)
        {
            var vertical = await identityContext.Vertical.Where(a=>a.Id==id).Include(a=> a.Tenant).Include(b=>b.Domain).FirstOrDefaultAsync();

            if (vertical == null || vertical.Status == 0) return new Error(ErrorCode.NotFound, "Branch is not found");
            var verticalresponse = new VerticalData
            {
                Id = vertical.Id,
                VerticalName = vertical.VerticalName,
                DomainId = vertical.DomainId,
                DomainName = vertical?.Domain?.DomaninName,
                TenantId = vertical.TenantId,
                TenantName = vertical?.Tenant?.Name,
               
                Status = vertical.Status
            };

            return BaseResult<VerticalData>.Ok(verticalresponse);
        }


        public async Task<IReadOnlyList<VerticalResponse>> GetAllAsync()
        {
            return await identityContext
                .Set<VerticalResponse>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BaseResult<VerticalResponse>> GetAllVerticals()
        {
            var verticaldata = await identityContext.Vertical.Include(a=>a.Tenant).Include(b=> b.Domain)
                .Where(b => b.Status == 1)
                .ToListAsync();

            if (verticaldata is null || !verticaldata.Any()) return new Error(ErrorCode.NotFound, "Vertical data not found");


            var verticalDtos = verticaldata
                .Select(b => new VerticalData
                {
                    Id = b.Id,
                    DomainId = b.DomainId,
                    DomainName = b?.Domain?.DomaninName,
                    TenantId = b.TenantId,
                    TenantName = b?.Tenant?.Name,
                    VerticalName = b.VerticalName,
                  
                    Status = b.Status
                }).Where(a => a.Status == 1)
                .ToList();
            // Convert BranchMaster to BranchResponse DTO

            var vertical = new VerticalResponse { verticals= verticalDtos };
            return BaseResult<VerticalResponse>.Ok(vertical);
        }

    }
}
