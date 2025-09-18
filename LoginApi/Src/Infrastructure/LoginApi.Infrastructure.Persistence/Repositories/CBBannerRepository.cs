//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using HyBrForex.Application.Interfaces;
//using HyBrForex.Application.Interfaces.Repositories;
//using HyBrForex.Application.Wrappers;
//using HyBrForex.Domain.CurrencyBoard.DTO.Properties;
//using HyBrForex.Domain.CurrencyBoard.DTO.Request;
//using HyBrForex.Domain.CurrencyBoard.DTO.Response;
//using HyBrForex.Domain.CurrencyBoard.Entities;
//using HyBrForex.Infrastructure.Persistence.Contexts;
//using Microsoft.EntityFrameworkCore;

//namespace HyBrForex.Infrastructure.Persistence.Repositories;

//public class CBBannerRepository(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
//    : GenericRepository<BannerDtl>(applicationDbContext), ICBBannerRepository
//{
//    public async Task<BaseResult> AddBanner(BannerAddRequest request)
//    {
//        var bannerDtl = new BannerDtl();
//        var _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "BannerFiles");
//        if (!Directory.Exists(_uploadPath)) Directory.CreateDirectory(_uploadPath);

//        if (request.File == null || request.File.Length == 0)
//        {
//            var error = new Error(ErrorCode.NotFound, "Banner not found.", "File");

//            return BaseResult<Error>.Failure(error);
//        }
//        // var _file = request.FileName.Split('.');

//        var filePath = Path.Combine(_uploadPath, request.File.FileName);
//        if (!File.Exists(filePath))
//        {
//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await request.File.CopyToAsync(stream);
//            }
//            var expDate = request.ExpiryDate != null ? Convert.ToDateTime(request.ExpiryDate) : DateTime.Now.AddDays(30);
//            var ExpiryDate = new DateTime(expDate.Year, expDate.Month, expDate.Day, expDate.Hour, expDate.Minute, 0, DateTimeKind.Utc);

//            var allbanner = await GetAllAsync();
//            var index = allbanner.Count == 0 ? 1 : allbanner.Max(a => a.Index) + 1;
//            bannerDtl = new BannerDtl(index, request.File.FileName, request.FileName, request.VideoDuration??0, ExpiryDate);
//            bannerDtl.Id = Ulid.NewUlid().ToString();
//            await AddAsync(bannerDtl);
//            await unitOfWork.SaveChangesAsync();
//            return BaseResult<string>.Ok(bannerDtl.Id);
//        }

//        {
//            var error = new Error(ErrorCode.AlreadyExists,
//                "Banner Already Exists With Same File VendorName-" + request.File.FileName, "File");
//            return BaseResult<Error>.Failure(error);
//        }
//    }

//    public async Task<BaseResult> UpdateBanner(BannerUpdateRequest request)
//    {
//        var bannerDtl = new BannerDtl();
//        if (request.Type == 1)
//        {
//            if (request.File != null || request.File.Length > 0)
//            {
//                var _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "BannerFiles");
//                var filePath = Path.Combine(_uploadPath, request.FileName);
//                await using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await request.File.CopyToAsync(stream);
//                }

//                bannerDtl = await GetByIdAsync(request.BannerId);
//                if (bannerDtl != null)
//                {
//                    bannerDtl.FileName = request.File.FileName;
//                    bannerDtl.FileLocation = request.FileName;
//                    bannerDtl.VideoDuration = request.VideoDuration??0;

//                    var expDate = request.ExpiryDate != null ? Convert.ToDateTime(request.ExpiryDate) : DateTime.Now.AddDays(30);
//                    bannerDtl.Status = request.Status;
//                    bannerDtl.ExpiryDate = new DateTime(expDate.Year, expDate.Month, expDate.Day, expDate.Hour, expDate.Minute, 0, DateTimeKind.Utc);
//                    Update(bannerDtl);
//                    await unitOfWork.SaveChangesAsync();
//                }
//                else
//                {
//                    var error = new Error(ErrorCode.NotFound, "Banner not found.", "BannerId:" + request.BannerId);
//                    return BaseResult<Error>.Failure(error);
//                }
//            }
//            else
//            {
//                bannerDtl = await GetByIdAsync(request.BannerId);
//                if (bannerDtl != null)
//                {
//                    bannerDtl.FileLocation = request.FileName;
//                    bannerDtl.VideoDuration = request.VideoDuration??0;
//                    var expDate = request.ExpiryDate != null ? Convert.ToDateTime(request.ExpiryDate) : DateTime.Now.AddDays(30);
//                    bannerDtl.Status = request.Status;
//                    bannerDtl.ExpiryDate = new DateTime(expDate.Year, expDate.Month, expDate.Day, expDate.Hour, expDate.Minute, 0, DateTimeKind.Utc);
//                    Update(bannerDtl);
//                    await unitOfWork.SaveChangesAsync();
//                }
//                else
//                {
//                    var error = new Error(ErrorCode.NotFound, "Banner not found.", "BannerId:" + request.BannerId);

//                    return BaseResult<Error>.Failure(error);
//                }
//            }
//        }
//        else if (request.Type == 2)
//        {
//            bannerDtl = await GetByIdAsync(request.BannerId);
//            if (bannerDtl != null)
//            {
//                var expDate =  DateTime.Now.AddDays(30);
//                bannerDtl.Status = request.Status;
//                bannerDtl.ExpiryDate =  new DateTime(expDate.Year, expDate.Month, expDate.Day, expDate.Hour, expDate.Minute, 0, DateTimeKind.Utc);
//                Update(bannerDtl);
//                await unitOfWork.SaveChangesAsync();
//            }
//            else
//            {
//                var error = new Error(ErrorCode.NotFound, "Banner not found.", "BannerId:" + request.BannerId);

//                return BaseResult<Error>.Failure(error);
//            }
//        }
//        else
//        {
//            var error = new Error(ErrorCode.BadRequest, "Not a valid request.", "Type:" + request.Type);

//            return BaseResult<Error>.Failure(error);
//        }

//        return BaseResult<string>.Ok(bannerDtl.Id);
//    }

//    public async Task<BaseResult<BannerResponse>> GetBanneres()
//    {
//        var response = new BannerResponse();
//        var banners = new List<BannerProperties>();
//        var allBanner = await GetAllAsync();
//        if (allBanner.Count > 0)
//        {
//            banners =
//            [
//                .. allBanner.Select(a => new BannerProperties
//                {
//                    Index = a.Index, FileType = a.FileLocation, Status = a.Status, BannerId = a.Id.ToString(),
//                    FileName = a.FileName,ExpiryDate = a.ExpiryDate,
//                    VideoDuration = a.VideoDuration
//                }).OrderBy(a => a.Index)
//            ];
//            response.Banners = banners;
//        }

//        return BaseResult<BannerResponse>.Ok(response);
//    }

//    public async Task<BaseResult<BannerProperties>> GetBannerById(string NotificationId)
//    {
//        var bannerDtls = await applicationDbContext.BannerDtl.Select(a => new BannerProperties
//        {
//            BannerId = a.Id.ToString(),
//            FileType = a.FileLocation,
//            FileName = a.FileName,
//            Status = a.Status,
//            Index = a.Index,
//            ExpiryDate = a.ExpiryDate,
//            VideoDuration = a.VideoDuration
//        }).ToListAsync();
//        var banner = new BannerProperties();
//        if (bannerDtls.Count > 0) banner = bannerDtls.FirstOrDefault(a => a.BannerId == NotificationId);

//        return BaseResult<BannerProperties>.Ok(banner);
//    }
//}