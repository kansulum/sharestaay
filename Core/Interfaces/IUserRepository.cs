using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        string GetUserId(string email);
        void SaveUserOnlineStatus(OnlineUser objentity);
        List<string> GetUserConnectionId(string UserID);
        List<string> GetUserConnectionId(string[] userIDs);
        List<AppUser> GetAllUsers();
        List<OnlineUserDetail> GetOnlineFriends(string userID);
        AppUser GetUserById(string userId);
        List<FriendRequests> GetSentFriendRequests(string userID);
        List<FriendRequests> GetReceivedFriendRequests(string userID);
        void SendFriendRequest(string endUserID, string loggedInUserID);
        int SaveUserNotification(string notificationType, string fromUserID, string toUserID);
        FriendMapping GetFriendRequestStatus(string userID);
        int ResponseToFriendRequest(string requestorID, string requestResponse, string endUserID);
        List<UserNotificationList> GetUserNotifications(string toUserID);
        int GetUserNotificationCounts(string toUserID);
        void ChangeNotificationStatus(int[] notificationIDs);
        FriendMapping RemoveFriendMapping(int friendMappingID);
        List<AppUser> GetUsersByLinqQuery(Expression<Func<AppUser, bool>> where);
        List<OnlineUserDetail> GetRecentChats(string currentUserID);
        OnlineUserDetail GetUserOnlineStatus(string userID);
     
        List<OnlineUserDetail> GetFriends(string userID);

        string[] GetFriendUserIds(string userID);

        void SaveFriendRequest(string endUserId, string loggedInUser);
    }
}