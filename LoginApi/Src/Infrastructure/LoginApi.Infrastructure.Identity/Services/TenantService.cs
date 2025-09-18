using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.DTOs.Tenant.Requests;
using HyBrForex.Application.DTOs.Tenant.Responses;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Migrations;
using LoginApi.Infrastructure.Identity.Contexts;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class TenantService(IdentityContext identityContext,IDomainRepository domainRepository,IStateMaster stateMaster,ICountryRepository countryRepository, IMapper mapper) : ITenantService
    {
        public async Task<BaseResult<TenantResponse>> CreateAsync(CreateRequest request)
        {
            try
            {
                // Check if the AuthorizedDealerId exists in the AuthorizedDealerCategory table
                //var exists = await identityContext.AuthorizedDealerCategory
                //    .AnyAsync(adc => adc.Id == request.AuthorizedDealerId);

                //if (!exists)
                //{
                //    return new Error(ErrorCode.NotFound, "Authorized Dealer ID does not exist in the AuthorizedDealerCategory table.");
                //}


                // Proceed with creating the tenant
                var tenant = mapper.Map<ApplicationTenant>(request);
                tenant.Id = Ulid.NewUlid().ToString();

                identityContext.Tenants.Add(tenant);
                await identityContext.SaveChangesAsync();

                return mapper.Map<TenantResponse>(tenant);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.ErrorInIdentity, "Error creating tenant.");
            }
        }




        public async Task<BaseResult<TenantResponse>> UpdateAsync(UpdateRequest request)
        {
            try
            {
                var tenant = await identityContext.Tenants.FindAsync(request.id);
                if (tenant == null) throw new KeyNotFoundException("Tenant not found");

                mapper.Map(request, tenant);
                identityContext.Tenants.Update(tenant);
                await identityContext.SaveChangesAsync();
                return mapper.Map<TenantResponse>(tenant);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "Error updating tenant..");
            }
        }

        public async Task<BaseResult<bool>> DeleteAsync(string id)
        {
            try
            {
                var tenant = await identityContext.Tenants.FindAsync(id);
                if (tenant == null) return false;

                tenant.Status = 0; // Set status to inactive instead of deleting
                await identityContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "Error deleting tenant..");
            }
        }

        public async Task<BaseResult<GetApplicationTenantResponse>> GetById(string id)
        {
            try
            {
                var tenant = await identityContext.Tenants.FindAsync(id);
                if (tenant == null || tenant.Status != 1)
                    return new Error(ErrorCode.NotFound, "Tenant not found or inactive.");

                var tenantResponse = mapper.Map<GetApplicationTenantResponse>(tenant);

                // Enrich with DomainName
                var domainResult = await domainRepository.GetDomainById(tenant.DomainId);
                tenantResponse.DomainName = domainResult?.Data?.DomainName;

                var stateResult = await stateMaster.GetByIdChildAsync(a=> a.Id ==tenant.State);
                tenantResponse.StateName = stateResult?.FirstOrDefault()?.StateName;
                var countryResult = await countryRepository.GetByIdChildAsync(a => a.Id == tenant.Country);
                tenantResponse.CountryName = countryResult?.FirstOrDefault()?.CountryName;

                // Optional: Replace AuthorizedDealerId with actual DealerCategory name
                // var dealerCategory = await identityContext.AuthorizedDealerCategory
                //     .Where(dc => dc.Id == tenant.AuthorizedDealerId)
                //     .Select(dc => dc.DealerCategory)
                //     .FirstOrDefaultAsync();
                // tenantResponse.AuthorizedDealerId = dealerCategory ?? "Unknown";

                return tenantResponse;
            }
            catch (Exception)
            {
                return new Error(ErrorCode.NotFound, "Error retrieving tenant by ID.");
            }
        }




        public async Task<BaseResult<IEnumerable<GetApplicationTenantResponse>>> GetAllAsync()
        {
            try
            {
                var tenants = await identityContext.Tenants
                    .Where(t => t.Status == 1)
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        t.CompanyName,
                        t.LegalName,
                        t.TaxId,
                        t.Address1,
                        t.Address2,
                        t.DomainId,
                        t.City,
                        t.Phone,
                        t.State,
                        t.PostalCode,
                        t.Email,
                        t.ConnectionString,
                        t.Country
                    })
                    .ToListAsync();

                var responseList = new List<GetApplicationTenantResponse>();

                foreach (var t in tenants)
                {
                    var domainResult = await domainRepository.GetDomainById(t.DomainId);
                    var domainName = domainResult?.Data?.DomainName;

                    var stateResult = await stateMaster.GetByIdChildAsync(a => a.Id == t.State);
                    var stateName = stateResult?.FirstOrDefault()?.StateName;
                    var countryResult = await countryRepository.GetByIdChildAsync(a => a.Id == t.Country);
                    var countryName = countryResult?.FirstOrDefault()?.CountryName;

                    responseList.Add(new GetApplicationTenantResponse
                    {
                        id = t.Id,
                        Name = t.Name,
                        CompanyName = t.CompanyName,
                        LegalName = t.LegalName,
                        TaxId = t.TaxId,
                        Address1 = t.Address1,
                        Address2 = t.Address2,
                        DomainId = t.DomainId,
                        DomainName = domainName,
                        City = t.City,
                        Phone = t.Phone,
                        State = t.State,
                        StateName = stateName,
                        PostalCode = t.PostalCode,
                        Email = t.Email,
                        ConnectionString = t.ConnectionString,
                        Country = t.Country,
                        CountryName = countryName,
                    });
                }

                return BaseResult<IEnumerable<GetApplicationTenantResponse>>.Ok(responseList);
            }
            catch (Exception)
            {
                return new Error(ErrorCode.NotFound, "Error retrieving tenants.");
            }
        }

        public async Task<BaseResult<IEnumerable<GetApplicationResponseGS>>> GetAllAsyncGS()
        {
            try
            {
                var tenants = await identityContext.Tenants
                    .Where(t => t.Status == 1) // Filter by status
                    .Select(t => new GetApplicationResponseGS
                    {
                        id = t.Id,
                        Name = t.Name,
                        
                    })
                    .ToListAsync();

                var mappedTenant = mapper.Map<IEnumerable<GetApplicationResponseGS>>(tenants);
                return BaseResult<IEnumerable<GetApplicationResponseGS>>.Ok(mappedTenant);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "Error retrieving tenants..");
            }
        }

    }
}
