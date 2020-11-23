using System;

namespace Core.Entities
{
    public class UserNotificationList
    {
        public string NotificationType { get; set; }
        public int NotificationID { get; set; }
        public AppUser User { get; set; }
        public DateTime CreatedOn { get; set; }
        public string NotificationStatus { get; set; }
        public int TotalNotifications { get; set; }
    }
}