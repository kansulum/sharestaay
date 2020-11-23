namespace Api.Dtos
{
    public class DtoNotification
    {
        public string NotificationType { get; set; }
        public int  NotificationId { get; set; }

        public string RequestResponse { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }
        public int NotificationCount { get; set; }
    }
}