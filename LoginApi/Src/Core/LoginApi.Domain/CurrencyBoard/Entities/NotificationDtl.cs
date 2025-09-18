using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.CurrencyBoard.Entities
{
    public class NotificationDtl : AuditableBaseEntity
    {
        public NotificationDtl(int index = 0, string notificationtext = "")
        {
            Notificationtext = notificationtext;
            Index = index;
        }

        public int Index { get; set; }

        public string Notificationtext { get; set; }
    }
}