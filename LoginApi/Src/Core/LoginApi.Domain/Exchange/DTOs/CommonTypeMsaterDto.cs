using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class CommonTypeMsaterDto
    {
        public CommonTypeMsaterDto(CommonTypeMsater commonTypeMsater)
        {
            TypeName = commonTypeMsater.TypeName;
            Id = commonTypeMsater.Id;
            Status = commonTypeMsater.Status;
        }

        public CommonTypeMsaterDto(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
        public string Id { get; set; }
        public int Status { get; set; }
    }
}