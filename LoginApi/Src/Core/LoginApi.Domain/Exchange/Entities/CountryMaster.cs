using System.Collections.Generic;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class CountryMaster : AuditableBaseEntity
    {
        public string? CountryName { get; set; }
        public string? Nationality { get; set; }
        public string? ClassId { get; set; }
        public string? SwiftCode { get; set; }
        public int? FATFSanction { get; set; }
        public int? IBANLength { get; set; }
        public byte[]? CountryFlag { get; set; }

        public virtual ICollection<CurrencyMaster>? CurrencyMasters { get; set; }
    }
}