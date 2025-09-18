using System;
using Microsoft.AspNetCore.Http;

namespace HyBrForex.Domain.CurrencyBoard.DTO.Request
{
    public class BannerAddRequest
    {
        public string? FileName { get; set; }
        public IFormFile? File { get; set; }
        public int? VideoDuration { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class BannerUpdateRequest
    {
        public string? FileName { get; set; }
        public string? BannerId { get; set; }
        public string? FileLocation { get; set; }
        public IFormFile? File { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int? VideoDuration { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}