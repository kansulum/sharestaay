using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;

namespace Infrastructure.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly RoomContext _context;
        private readonly AppIdentityDbContext _identityContext;
        public NotificationsRepository(RoomContext context, AppIdentityDbContext identityContext)
        {
            _identityContext = identityContext;
            _context = context;
        }

        public int GetUseNNotificationCount(string userId)
        {
            return _context.UserNotifications
                    .Where(n => n.ToUserID == userId
                     && n.IsActive == true && n.Status == "New")
                     .Count();
        }

        public List<UserNotificationList> GetUserNotifications(string toUserID)
        {
            var listQuery = (from u in _context.UserNotifications
                             join v in _identityContext.AppUsers on u.FromUserID equals v.Id
                             where u.ToUserID == toUserID && u.IsActive == true
                             select new UserNotificationList()
                             {
                                 NotificationID = u.Id,
                                 NotificationType = u.NotificationType,
                                 User = v,
                                 NotificationStatus = u.Status,
                                 CreatedOn = u.CreatedOn
                             }).OrderByDescending(m => m.CreatedOn);
            int totalNotifications = listQuery.Count();
            var list = listQuery.Take(3).ToList();
            list.ForEach(m => m.TotalNotifications = totalNotifications);
            return list;
        }

        public int SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            UserNotification notification = new UserNotification();
            notification.CreatedOn = DateTime.Now;
            notification.IsActive = true;
            notification.NotificationType = notificationType;
            notification.FromUserID = fromUserId;
            notification.Status = "New";
            notification.UpdatedOn = DateTime.Now;
            notification.ToUserID = toUserId;
            _context.UserNotifications.Add(notification);
            _context.SaveChanges();

            return notification.Id;
        }
    }
}