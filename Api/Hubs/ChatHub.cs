using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly INotificationsRepository _notificationsRepository;
        private readonly RoomContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(IUserRepository userRepository, IChatRepository chatRepository,
        INotificationsRepository notificationsRepository,
        RoomContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _notificationsRepository = notificationsRepository;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        public override  Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var email = httpContext.Request.Query["user"].ToString();
            var userId = _userRepository.GetUserId(email);
            _userRepository.SaveUserOnlineStatus(new OnlineUser { UserID = userId, ConnectionID = Context.ConnectionId, IsOnline = true });
            return  base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // var userEmail = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var userId = _userRepository.GetUserId(userEmail);
            // if (userId != null)
            // {
            //     _userRepository.SaveUserOnlineStatus(new OnlineUser { UserID = userId, ConnectionID = Context.ConnectionId, IsOnline = false });
            //     _context.SaveChanges();
            // }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotificationAsync(string fromUserId, string toUserId, string notificationType)
        {
            int notificationId = _notificationsRepository.SaveUserNotification(notificationType, fromUserId, toUserId);
            var connectedId = _userRepository.GetUserConnectionId(toUserId);
            if (connectedId != null && connectedId.Count > 0)
            {
                // var userInfo = CommonFunctions.GetUserModel(fromUserID);
                int notificationCount = _notificationsRepository.GetUseNNotificationCount(toUserId);
                var notification = new DtoNotification {
                    NotificationType=notificationType,
                    NotificationId = notificationId,
                    NotificationCount = notificationCount,
                    UserId = fromUserId
                };
              await  Clients.Clients(connectedId).SendAsync("ReceiveNotification",notification);
            }

        }
        public async Task Send(string content)
        {
            // await Clients.All.SendAsync("Send", msg);
            await Clients.All.SendAsync("NewMessage", content);
        }

        public async Task RefreshOnlineUsersAsync(string userID)
        {
            var users = _userRepository.GetOnlineFriends(userID);
           await RefreshOnlineUsersByConnectionIdsAsync(users.SelectMany(m => m.ConnectionId).ToList(), userID);
        }
        public async Task RefreshOnlineUsersByConnectionIdsAsync(List<string> connectionIds, string userID ="")
        {
           await Clients.Clients(connectionIds).SendAsync("RefreshOnlineUsers");
            if (!string.IsNullOrEmpty(userID))
            {
                var onlineStatus = _userRepository.GetUserOnlineStatus(userID);
                if (onlineStatus != null)
                {
                   await Clients.Clients(connectionIds)
                    .SendAsync("RefreshOnlineUserByUserID",userID, onlineStatus.IsOnline, Convert.ToString(onlineStatus.LastUpdationTime));
                }
            }
        }
        public async Task SendRequestAsync(SendRequestDto SendRequestDto)
        {
            _userRepository.SendFriendRequest(SendRequestDto.UserID, SendRequestDto.LoggedInUserID);
            await SendNotificationAsync(SendRequestDto.LoggedInUserID, SendRequestDto.UserID, "FriendRequest");
        }
        
        public async Task SendResponseToRequestAsync(string requestorID, string requestResponse, string endUserID)
        {
            var notificationID = _userRepository.ResponseToFriendRequest(requestorID, requestResponse, endUserID);
            if (notificationID > 0)
            {
                var connectionId = _userRepository.GetUserConnectionId(endUserID);
                if (connectionId != null && connectionId.Count() > 0)
                {
                    await Clients.Clients(connectionId).SendAsync("RemoveNotification",notificationID);
                }
            }
            if (requestResponse == "Accepted")
            {
                await SendNotificationAsync(endUserID, requestorID, "FriendRequestAccepted");
                List<string> connectionIds = _userRepository.GetUserConnectionId(new string[] { endUserID, requestorID });
                await RefreshOnlineUsersByConnectionIdsAsync(connectionIds);
            }
        }
        public async Task RefreshNotificationCountsAsync(string toUserID)
        {
            var connectionId = _userRepository.GetUserConnectionId(toUserID);
            if (connectionId != null && connectionId.Count() > 0)
            {
                int notificationCounts = _userRepository.GetUserNotificationCounts(toUserID);
               await Clients.Clients(connectionId).SendAsync("RefreshNotificationCounts",notificationCounts);
            }
        }
        public async Task ChangeNotitficationStatusAsync(string notificationIds, string toUserID)
        {
            if (!string.IsNullOrEmpty(notificationIds))
            {
                string[] arrNotificationIds = notificationIds.Split(',');
                int[] ids = arrNotificationIds.Select(m => Convert.ToInt32(m)).ToArray();
                _userRepository.ChangeNotificationStatus(ids);
              await  RefreshNotificationCountsAsync(toUserID);
            }
        }
        public async Task UnfriendUserAsync(int friendMappingID)
        {
            var friendMapping = _userRepository.RemoveFriendMapping(friendMappingID);
            if (friendMapping != null)
            {
                List<string> connectionIds = _userRepository.GetUserConnectionId(new string[] { friendMapping.EndUserID, friendMapping.RequestorUserID });
                await RefreshOnlineUsersByConnectionIdsAsync(connectionIds);
            }
        }
        public async Task SendMessageAsync(MessageDto messageDto)
        {
            ChatMessage objentity = new ChatMessage();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.FromUserID = messageDto.FromUserId;
            objentity.IsActive = true;
            objentity.Message = messageDto.Message;
            objentity.ViewedOn = System.DateTime.Now;
            objentity.Status = "Sent";
            objentity.ToUserID = messageDto.ToUserId;
            objentity.UpdatedOn = System.DateTime.Now;
            var obj = _chatRepository.SaveChatMessage(objentity);
            messageDto.MessageModel= CommonFunctions.GetMessageModel(obj);
            List<string> connectionIds = _userRepository.GetUserConnectionId(new string[] { messageDto.FromUserId, messageDto.ToUserId });
           await Clients.Clients(connectionIds).SendAsync("AddNewChatMessage",messageDto);
        }
        public async Task SendUserTypingStatusAsync(string toUserID, string fromUserID)
        {
            List<string> connectionIds = _userRepository.GetUserConnectionId(new string[]{toUserID});
            if (connectionIds.Count > 0)
            {
               await Clients.Clients(connectionIds).SendAsync("UserIsTyping",fromUserID);
            }
        }
        public async Task UpdateMessageStatusAsync(int messageID, string currentUserID, string fromUserID)
        {
            if (messageID > 0)
            {
                _chatRepository.UpdateMessageStatusByMessageID(messageID);
            }
            else
            {
                _chatRepository.UpdateMessageStatusByUserID(fromUserID, currentUserID);
            }
            List<string> connectionIds = _userRepository.GetUserConnectionId(new string[] { currentUserID, fromUserID });
           await Clients.Clients(connectionIds).SendAsync("UpdateMessageStatusInChatWindow",messageID, currentUserID, fromUserID);
        }
    }
}