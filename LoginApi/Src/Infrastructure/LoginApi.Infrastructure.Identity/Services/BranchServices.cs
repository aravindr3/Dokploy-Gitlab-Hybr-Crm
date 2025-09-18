using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Branch.Data;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.DTOs.Branch.Response;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Identity.Services;

public class BranchServices(
    IdentityContext identityContext,
    IEmployeeRepository employeeRepository) : IBranchServices
{
    public async Task<BaseResult<string>> CreateBranch(BranchRequest model)
    {// Check if branch name already exists
        bool branchExists = await identityContext.BranchMaster
            .AnyAsync(b => b.BranchName.ToLower() == model.BranchName.ToLower());

        if (branchExists)
        {
            return BaseResult<string>.Failure("Branch name already exists.");
        }


        var newBranch = new BranchMaster
        {
            VerticalId = model.VerticalId,
            CorporateCode = model.CorporateCode,
            CorporateName = model.CorporateName,
            ROCode = model.ROCode,
            ROName = model.ROName,
            BranchCode = model.BranchCode,
            BranchName = model.BranchName,
            FACode = model.FACode,
            RBILicNo = model.RBILicNo,
            BranchHeadsId = model.BranchHeadsId,
            Place = model.Place,
            Street = model.Street,
            State = model.State,
            City = model.City,
            Pin = model.Pin,
            Email = model.Email,
            Zone = model.Zone,
            Phone = model.Phone,
            Fax = model.Fax,
            Mobile = model.Mobile,
            Website = model.Website,
            ServiceTaxNo = model.ServiceTaxNo,
            CIN_No = model.CIN_No,
            PAN = model.PAN,
            GSTIN = model.GSTIN,
            ISDNo = model.ISDNo
        };
        newBranch.Id = Ulid.NewUlid().ToString();

        await identityContext.AddAsync(newBranch);
        await identityContext.SaveChangesAsync();

        return BaseResult<string>.Ok("Succsessfully Inserted");
    }

    public async Task<BaseResult> DeleteBranch(string id)
    {
        var branch = await identityContext.BranchMaster.FindAsync(id);

        if (branch == null) return new Error(ErrorCode.NotFound, "Branch is not found");
        branch.Status = 0;
        identityContext.Update(branch);

        await identityContext.SaveChangesAsync();

        return BaseResult.Ok();
    }

    public async Task<BaseResult> UpdateBranch(string id, BranchRequest model)
    {
        var branch = await identityContext.BranchMaster.FindAsync(id);

        if (branch == null || branch.Status == 0) return new Error(ErrorCode.NotFound, "Branch is not found");
        branch.VerticalId = model.VerticalId;
        branch.CorporateCode = model.CorporateCode;
        branch.CorporateName = model.CorporateName;
        branch.ROCode = model.ROCode;
        branch.ROName = model.ROName;
        branch.BranchCode = model.BranchCode;
        branch.BranchName = model.BranchName;
        branch.FACode = model.FACode;
        branch.RBILicNo = model.RBILicNo;
        branch.BranchHeadsId = model.BranchHeadsId;
        branch.Place = model.Place;
        branch.Street = model.Street;
        branch.State = model.Street;
        branch.City = model.City;
        branch.Pin = model.Pin;
        branch.Email = model.Email;
        branch.Zone = model.Zone;
        branch.Phone = model.Phone;
        branch.Fax = model.Fax;
        branch.Mobile = model.Mobile;
        branch.Website = model.Website;
        branch.ServiceTaxNo = model.ServiceTaxNo;
        branch.CIN_No = model.CIN_No;
        branch.PAN = model.PAN;
        branch.GSTIN = model.GSTIN;
        branch.ISDNo = model.ISDNo;

        await identityContext.SaveChangesAsync();
        return BaseResult.Ok();
    }

    public async Task<BaseResult<BranchData>> GetBranchbyId(string id)
    {
        var branch = await identityContext.BranchMaster.FindAsync(id);

        if (branch == null || branch.Status == 0) return new Error(ErrorCode.NotFound, "Branch is not found");
        var branchresponse = new BranchData
        {
            Id = branch.Id,
            VerticalId = branch.VerticalId,
            CorporateCode = branch.CorporateCode,
            CorporateName = branch.CorporateName,
            ROCode = branch.ROCode,
            ROName = branch.ROName,
            BranchCode = branch.BranchCode,
            BranchName = branch.BranchName,
            FACode = branch.FACode,
            RBILicNo = branch.RBILicNo,
            BranchHeadsId = branch.BranchHeadsId,
            BranchHead = employeeRepository.GetByIdAsync(branch.BranchHeadsId)?.Result?.Name,
            Place = branch.Place,
            Street = branch.Street,
            State = branch.State,
            City = branch.City,
            Pin = branch.Pin,
            Email = branch.Email,
            Zone = branch.Zone,
            Phone = branch.Phone,
            Fax = branch.Fax,
            Mobile = branch.Mobile,
            Website = branch.Website,
            ServiceTaxNo = branch.ServiceTaxNo,
            CIN_No = branch.CIN_No,
            PAN = branch.PAN,
            GSTIN = branch.GSTIN,
            ISDNo = branch.ISDNo,
            Status = branch.Status
        };

        return BaseResult<BranchData>.Ok(branchresponse);
    }

    public async Task<IReadOnlyList<BranchResponse>> GetAllAsync()
    {
        return await identityContext
            .Set<BranchResponse>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<BaseResult<BranchResponse>> GetAllBranch()
    {
        var branches = await identityContext.BranchMaster
            .Where(b => b.Status == 1)
            .ToListAsync();

        if (branches is null || !branches.Any()) return new Error(ErrorCode.NotFound, "Branch data not found");


        var branchDtos = branches
            .Select(b => new BranchData
            {
                Id = b.Id,
                VerticalId = b.VerticalId,
                CorporateCode = b.CorporateCode,
                CorporateName = b.CorporateName,
                ROCode = b.ROCode,
                ROName = b.ROName,
                BranchCode = b.BranchCode,
                BranchName = b.BranchName,
                FACode = b.FACode,
                RBILicNo = b.RBILicNo,
                BranchHeadsId = b.BranchHeadsId,
                BranchHead = employeeRepository.GetByIdAsync(b.BranchHeadsId)?.Result?.Name,
                Place = b.Place,
                Street = b.Street,
                State = b.State,
                City = b.City,
                Pin = b.Pin,
                Email = b.Email,
                Zone = b.Zone,
                Phone = b.Phone,
                Fax = b.Fax,
                Mobile = b.Mobile,
                Website = b.Website,
                ServiceTaxNo = b.ServiceTaxNo,
                CIN_No = b.CIN_No,
                PAN = b.PAN,
                GSTIN = b.GSTIN,
                ISDNo = b.ISDNo,
                Status = b.Status
            }).Where(a => a.Status == 1).OrderBy(a=>Convert.ToInt32(a.BranchCode))
            .ToList();
        // Convert BranchMaster to BranchResponse DTO

        var branch = new BranchResponse { branches = branchDtos };
        return BaseResult<BranchResponse>.Ok(branch);
    }



    public async Task<BaseResult<BranchResponse>> GetAllBrachByTenantId(string verticalId)
    {
        var branches = await identityContext.BranchMaster
            .Where(b => b.Status == 1 && b.VerticalId == verticalId) 
            .ToListAsync();

        if (branches is null || !branches.Any())
            return new Error(ErrorCode.NotFound, "Branch data not found");

        var branchDtos = branches
            .Select(b => new BranchData
            {
                Id = b.Id,
                VerticalId = b.VerticalId,
                CorporateCode = b.CorporateCode,
                CorporateName = b.CorporateName,
                ROCode = b.ROCode,
                ROName = b.ROName,
                BranchCode = b.BranchCode,
                BranchName = b.BranchName,
                FACode = b.FACode,
                RBILicNo = b.RBILicNo,
                BranchHeadsId = b.BranchHeadsId,
                BranchHead = employeeRepository.GetByIdAsync(b.BranchHeadsId)?.Result?.Name, 
                Place = b.Place,
                Street = b.Street,
                State = b.State,
                City = b.City,
                Pin = b.Pin,
                Email = b.Email,
                Zone = b.Zone,
                Phone = b.Phone,
                Fax = b.Fax,
                Mobile = b.Mobile,
                Website = b.Website,
                ServiceTaxNo = b.ServiceTaxNo,
                CIN_No = b.CIN_No,
                PAN = b.PAN,
                GSTIN = b.GSTIN,
                ISDNo = b.ISDNo,
                Status = b.Status
            })
            .OrderBy(a => Convert.ToInt32(a.BranchCode)) 
            .ToList();

        var branch = new BranchResponse { branches = branchDtos };
        return BaseResult<BranchResponse>.Ok(branch);
    }


}