using System;

namespace HyBrForex.Domain.CurrencyBoard.DTO.Properties
{
    public class BannerProperties
    {
        public string? BannerId { get; set; }
        public int Index { get; set; }
        public int Status { get; set; }
        public string? FileName { get; set; }
        public string? FileLocation { get; set; }
        public string? FileType { get; set; }
        public int? VideoDuration { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}