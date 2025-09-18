using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.DTOs.Domain.Data;
using HyBrCRM.Application.DTOs.Domain.Request;
using HyBrCRM.Application.DTOs.Domain.Response;
using HyBrCRM.Application.DTOs.Verticals.Data;
using HyBrCRM.Application.DTOs.Verticals.Request;
using HyBrCRM.Application.DTOs.Verticals.Response;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Infrastructure.Identity.Models;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Migrations;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HyBrCRM.Infrastructure.Identity.Services
{
    public class DomainService(
    IdentityContext identityContext
    ) : IDomainRepository
    {
        public async Task<BaseResult<string>> CreateDomain(DomainRequest model)
        {
           


            var newDomain = new ApplicationDomain
            {
                DomaninName = model.DomainName,
               
            };
            newDomain.Id = Ulid.NewUlid().ToString();

            await identityContext.AddAsync(newDomain);
            await identityContext.SaveChangesAsync();
            return newDomain.Id;


            return BaseResult<string>.Ok("Succsessfully Inserted");
        }


        public async Task<BaseResult> DeleteDomain(string id)
        {
            var vertical = await identityContext.Domain.FindAsync(id);

            if (vertical == null) return new Error(ErrorCode.NotFound, "Domain is not found");
            vertical.Status = 0;
            identityContext.Update(vertical);

            await identityContext.SaveChangesAsync();

            return BaseResult.Ok();
        }



        public async Task<BaseResult> UpdateDomain(string id, DomainRequest model)
        {
            var vertical = await identityContext.Domain.FindAsync(id);

            if (vertical == null || vertical.Status == 0) return new Error(ErrorCode.NotFound, "Domain is not found");
            vertical.DomaninName = model.DomainName;
           
            await identityContext.SaveChangesAsync();
            return BaseResult.Ok();
        }


        public async Task<BaseResult<DomainData>> GetDomainById(string id)
        {
            var vertical = await identityContext.Domain.FindAsync(id);

            if (vertical == null || vertical.Status == 0) return new Error(ErrorCode.NotFound, "Domain is not found");
            var verticalresponse = new DomainData
            {
                Id = vertical.Id,
                DomainName = vertical.DomaninName,
               

                Status = vertical.Status
            };

            return BaseResult<DomainData>.Ok(verticalresponse);
        }


        public async Task<IReadOnlyList<DomainResponse>> GetAllAsync()
        {
            return await identityContext
                .Set<DomainResponse>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BaseResult<DomainResponse>> GetAllDomain()
        {
            var verticaldata = await identityContext.Domain
                .Where(b => b.Status == 1)
                .ToListAsync();

            if (verticaldata is null || !verticaldata.Any()) return new Error(ErrorCode.NotFound, "Domain data not found");


            var verticalDtos = verticaldata
                .Select(b => new DomainData
                {
                    Id = b.Id,
                    DomainName = b.DomaninName,
                   

                    Status = b.Status
                }).Where(a => a.Status == 1)
                .ToList();
            // Convert BranchMaster to BranchResponse DTO

            var vertical = new DomainResponse { domains = verticalDtos };
            return BaseResult<DomainResponse>.Ok(vertical);
        }


    }
}
