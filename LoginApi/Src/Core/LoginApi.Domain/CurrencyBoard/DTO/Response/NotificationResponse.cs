using System.Collections.Generic;
using HyBrForex.Domain.CurrencyBoard.DTO.Properties;

namespace HyBrForex.Domain.CurrencyBoard.DTO.Response
{
    public class NotificationResponse
    {
        public List<NotificationProperties>? notifications { get; set; }
    }
}