using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface INotificationsRepository
    {
        List<UserNotificationList> GetUserNotifications(string toUserID);
        int GetUseNNotificationCount(string userId);
        int SaveUserNotification(string notificationType,string fromUserId,string toUserId);
         
    }
}