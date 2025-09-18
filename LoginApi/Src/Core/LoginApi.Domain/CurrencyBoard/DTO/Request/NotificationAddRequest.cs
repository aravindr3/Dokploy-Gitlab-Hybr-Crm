namespace HyBrForex.Domain.CurrencyBoard.DTO.Request
{
    public class NotificationAddRequest
    {
        public string? Notification { get; set; }
    }

    public class NotificationUpdateRequest
    {
        public string? NotificationId { get; set; }
        public string? Notification { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
    }
}