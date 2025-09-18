using System;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.CurrencyBoard.Entities
{
    public class BannerDtl : AuditableBaseEntity
    {
        public BannerDtl(int index, string fileName, string fileLocation,int? videoDuration, DateTime? expiryDate)
        {
            Index = index;
            FileName = fileName;
            FileLocation = fileLocation;
            VideoDuration = videoDuration;
            ExpiryDate = expiryDate;
        }

        public BannerDtl()
        {
        }

        public int Index { get; set; }
        public string? FileName { get; set; }
        public string? FileLocation { get; set; }
        public int? VideoDuration { get; set; }
        public DateTime? ExpiryDate { get; set; } = DateTime.Now;
    }
}