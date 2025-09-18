using System;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class CommonMasterDto
    {
        public CommonMasterDto()
        {
        }

#pragma warning restore

        public CommonMasterDto(CommonMsater commonMaster , string typeName)
        {
            CommonTypeId = commonMaster.CommonTypeId;
            CommonName = commonMaster.CommonName;
            CommonType = typeName;
            Id = commonMaster.Id;
            CreatedDateTime = Convert.ToDateTime(commonMaster.Created);
            Status = commonMaster.Status;
        }

        public string? CommonTypeId { get; set; }
        public string? CommonType { get; set; }
        public string? Id { get; set; }
        public string? CommonName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int Status { get; set; }
    }
}