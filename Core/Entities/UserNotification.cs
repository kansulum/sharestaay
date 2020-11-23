using System;

namespace Core.Entities
{
    public class UserNotification
    {
        public int Id { get; set; }
        public string ToUserID { get; set; }
        public string FromUserID { get; set; }
        public string NotificationType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}