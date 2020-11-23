using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppIdentityDbContext _identityContext;
        private readonly RoomContext _roomContext;
        public UserRepository(AppIdentityDbContext identityContext, RoomContext roomContext)
        {
            _roomContext = roomContext;
            _identityContext = identityContext;
        }

        public AppUser GetUserByEmail(string email)
        {

            return _identityContext.AppUsers.Where(u => u.Email == email)
                .SingleOrDefault();

        }

        public AppUser GetUserById(string id)
        {
            return _identityContext.AppUsers.Where(u => u.Id == id)
                .SingleOrDefault();
        }

        public string GetUserId(string email)
        {
            return _identityContext.AppUsers.Where(u => u.Email == email).Select(u => u.Id).SingleOrDefault();
        }

        public List<string> GetUserConnectionId(string[] userIds)
        {
            return _roomContext.OnlineUsers.Where(m => userIds.Contains(m.UserID)
                       && m.IsActive == true && m.IsOnline == true)
                       .Select(m => m.ConnectionID).ToList();
        }

        public List<string> GetUserConnectionId(string toUserId)
        {
            var userId = GetUserId(toUserId);
            var userConnectionIds = _roomContext.OnlineUsers
                                      .Where(m => m.UserID == userId && m.IsActive
                                      == true && m.IsOnline == true).Select(m => m.ConnectionID)
                                      .ToList();
            return userConnectionIds;
        }

        public OnlineUserDetail GetUserOnlineStatus(string userId)
        {
            OnlineUserDetail onlineUserDetails = new OnlineUserDetail();

            onlineUserDetails.UserId = userId;
            var onlineUserslist = _roomContext.OnlineUsers
            .Where(m => m.UserID == userId && m.IsActive == true).ToList();

            if (onlineUserslist != null && onlineUserslist.Count > 0)
            {
                onlineUserDetails.IsOnline = false;

                var onlineConnections = onlineUserslist.Where(m => m.IsOnline).ToList();

                var offlineConnections = onlineUserslist.Where(m => !m.IsOnline).ToList();

                if (onlineConnections != null && onlineConnections.Count > 0)
                {
                    onlineUserDetails.IsOnline = true;
                }
                else if (offlineConnections != null && offlineConnections.Count > 0)
                {
                    onlineUserDetails.IsOnline = false;
                    onlineUserDetails.LastUpdationTime =
                    offlineConnections.OrderByDescending(m => m.UpdatedOn).Select(m => m.UpdatedOn).FirstOrDefault();
                }
            }
            return onlineUserDetails;
        }

        public int ResponseToFriendRequest(string requestorId, string response, string endUserId)
        {
            var request = _roomContext.FriendMappings.Where(r => r.RequestorUserID == requestorId && r.EndUserID == endUserId && r.IsActive == true).FirstOrDefault();
            if (request != null)
            {
                request.RequestStatus = response;
                request.UpdatedOn = DateTime.Now;
                _roomContext.SaveChanges();
            }

            var notification = _roomContext.UserNotifications
                                .Where(n => n.FromUserID == requestorId && n.ToUserID == endUserId
                                && n.IsActive == true && n.NotificationType == "FriendRequest")
                                .FirstOrDefault();

            if (notification != null)
            {
                notification.IsActive = false;
                notification.UpdatedOn = System.DateTime.Now;
                _roomContext.SaveChanges();
                return notification.Id;
            }
            return 0;
        }

        public void SaveFriendRequest(string endUserId, string loggedInUser)
        {
            FriendMapping friendMap = new FriendMapping();
            friendMap.CreatedOn = DateTime.Now;
            friendMap.EndUserID = endUserId;
            friendMap.IsActive = true;
            friendMap.RequestorUserID = loggedInUser;
            friendMap.RequestStatus = "Sent";
            _roomContext.FriendMappings.Add(friendMap);
        }

        public void SaveUserOnlineStatus(OnlineUser onlineUser)
        {
            var obj = _roomContext.OnlineUsers.Where(m => m.UserID == onlineUser.UserID && m.IsActive == true && m.ConnectionID == onlineUser.ConnectionID).FirstOrDefault();
            if (obj != null)
            {
                obj.IsOnline = onlineUser.IsOnline;
                obj.UpdatedOn = DateTime.Now;
                obj.ConnectionID = onlineUser.ConnectionID;
                _roomContext.SaveChanges();
            }
            else
            {
                onlineUser.UpdatedOn = DateTime.Now;
                onlineUser.CreatedOn = DateTime.Now;
                onlineUser.IsActive = true;
                _roomContext.OnlineUsers.Add(onlineUser);
                _roomContext.SaveChanges();
            }
        }

        public List<AppUser> GetAllUsers()
        {
              var objList = _identityContext.Users.ToList();
            return objList;
        }

        public List<OnlineUserDetail> GetOnlineFriends(string userID)
        {
            string[] friends = GetFriendUserIds(userID);  
            var friendOnlineDetails = _roomContext.OnlineUsers
            .Where(m => friends.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).ToList();
            
            var obj = (from v in _identityContext.Users
                       where friends.Contains(v.Id)
                       select new OnlineUserDetail
                       {
                           UserId = v.Id,
                           Name = v.Email,
                          
                       }).OrderBy(m => m.Name).ToList();

            var onlineUserIds = friendOnlineDetails.Select(m => m.UserID).ToArray();
            obj = obj.Where(m => onlineUserIds.Contains(m.UserId)).ToList();
            obj.ForEach(m =>
            {
                m.ConnectionId = friendOnlineDetails.Where(x => x.UserID == m.UserId).Select(x => x.ConnectionID).ToList();
            });

            return obj;
        }

        public List<FriendRequests> GetSentFriendRequests(string userID)
        {
           var list = (from u in _roomContext.FriendMappings
                        join v in _identityContext.Users on u.EndUserID equals v.Id
                        where u.RequestorUserID == userID && u.RequestStatus == "Sent" && u.IsActive == true
                        select new FriendRequests()
                        {
                            UserInfo = v,
                            RequestStatus = u.RequestStatus,
                            EndUserID = u.EndUserID,
                            RequestorUserID = u.RequestorUserID
                        }).ToList();
            return list;
        }

        public List<FriendRequests> GetReceivedFriendRequests(string userID)
        {
            var list = (from u in _roomContext.FriendMappings
                        join v in _identityContext.Users on u.RequestorUserID equals v.Id
                        where u.EndUserID == userID && u.RequestStatus == "Sent" && u.IsActive == true
                        select new FriendRequests()
                        {
                            UserInfo = v,
                            RequestStatus = u.RequestStatus,
                            EndUserID = u.EndUserID,
                            RequestorUserID = u.RequestorUserID
                        }).ToList();
            return list;
        }

        public void SendFriendRequest(string endUserID, string loggedInUserID)
        {
            FriendMapping objentity = new FriendMapping();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.EndUserID = endUserID;
            objentity.IsActive = true;
            objentity.RequestorUserID = loggedInUserID;
            objentity.RequestStatus = "Sent";
            objentity.UpdatedOn = System.DateTime.Now;
            _roomContext.FriendMappings.Add(objentity);
            _roomContext.SaveChanges();
        }

        public int SaveUserNotification(string notificationType, string fromUserID, string toUserID)
        {
             UserNotification notification = new UserNotification();
            notification.CreatedOn = System.DateTime.Now;
            notification.IsActive = true;
            notification.NotificationType = notificationType;
            notification.FromUserID = fromUserID;
            notification.Status = "New";
            notification.UpdatedOn = System.DateTime.Now;
            notification.ToUserID = toUserID;
            _roomContext.UserNotifications.Add(notification);
            _roomContext.SaveChanges();
            return notification.Id;
        }

        public FriendMapping GetFriendRequestStatus(string userID)
        {
            var obj = _roomContext.FriendMappings
            .Where(m => (m.EndUserID == userID || m.RequestorUserID == userID) 
            && m.IsActive == true).FirstOrDefault();
            return obj;
        }

        public List<UserNotificationList> GetUserNotifications(string toUserID)
        {
            var listQuery = (from u in _roomContext.UserNotifications
                             join v in _identityContext.Users on u.FromUserID equals v.Id
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

        public int GetUserNotificationCounts(string toUserID)
        {
             int count = _roomContext.UserNotifications
             .Where(m => m.Status == "New" && m.ToUserID == toUserID && m.IsActive == true).Count();
            return count;
        }

        public void ChangeNotificationStatus(int[] notificationIDs)
        {
             _roomContext.UserNotifications.Where(m => notificationIDs.Contains(m.Id)).ToList().ForEach(m => m.Status = "Read");
            _roomContext.SaveChanges();
        }

        public FriendMapping RemoveFriendMapping(int friendMappingID)
        {
            var obj = _roomContext.FriendMappings.Where(m => m.Id == friendMappingID).FirstOrDefault();
            if (obj != null)
            {
                obj.IsActive = false;
                _roomContext.SaveChanges();
            }
            return obj;
        }

        public List<AppUser> GetUsersByLinqQuery(Expression<Func<AppUser, bool>> where)
        {
             var objList = _identityContext.Users.Where(where).ToList();
            return objList;
        }

        public List<OnlineUserDetail> GetRecentChats(string currentUserID)
        {
            string[] friends = GetFriendUserIds(currentUserID);
            var recentMessages = _roomContext.ChatMessages.Where(m => m.IsActive == true && (m.ToUserID == currentUserID || m.FromUserID == currentUserID)).OrderByDescending(m => m.CreatedOn).ToList();
            var userIds = recentMessages.Select(m => (m.ToUserID == currentUserID ? m.FromUserID : m.ToUserID)).Distinct().ToArray();
            var userIdsList = userIds.ToList();
            var messagesByUserId = recentMessages.Where(m => m.ToUserID == currentUserID && m.Status == "Sent").ToList();
            var newMessagesCount = (from p in messagesByUserId
                                    group p by p.FromUserID into g
                                    select new { FromUserID = g.Key, Count = g.Count() }).ToList();
            var onlineUserIDs = _roomContext.OnlineUsers.Where(m => friends.Contains(m.UserID) && userIds.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserID).ToArray();
            var users = (from m in _identityContext.Users
                         join v in userIdsList on m.Id equals v
                         select new OnlineUserDetail
                         {
                             UserId = m.Id,
                             Name = m.Email,
                            
                             IsOnline = onlineUserIDs.Contains(m.Id) ? true : false
                         }).ToList();
            users.ForEach(m =>
            {
                m.UnReadMessageCount = newMessagesCount.Where(x => x.FromUserID == m.UserId).Select(x => x.Count).FirstOrDefault();
            });
            users = users.OrderBy(d => userIdsList.IndexOf(d.UserId)).ToList();
            return users;
        }

        public List<OnlineUserDetail> GetFriends(string userID)
        {
           var friendIds = GetFriendUserIds(userID);
            var onlineUserIDs = _roomContext.OnlineUsers.Where(m => friendIds.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserID).ToArray();
            var users = _identityContext.Users.Where(m => friendIds.Contains(m.Id)).Select(m => new OnlineUserDetail
                         {
                             UserId = m.Id,
                             Name = m.Email,
                             IsOnline = onlineUserIDs.Contains(m.Id) ? true : false
                         }).ToList();
            return users;
        }

        public string[] GetFriendUserIds(string userID)
        {
            var arr = _roomContext.FriendMappings
            .Where(m => (m.RequestorUserID == userID || m.EndUserID == userID) 
            && m.RequestStatus.ToUpper() == "Accepted".ToUpper() && m.IsActive == true)
            .Select(m => m.RequestorUserID == userID ? m.EndUserID : m.RequestorUserID).ToArray();
            return arr;
        }
    }
}