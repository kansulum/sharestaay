using System;
using System.Collections.Generic;
using System.Security.Claims;
using Api.Dtos;
using Api.Extentions;
using Api.Hubs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers
{
    [Authorize]
    public class RequestsController : BaseApiController
    {
        private readonly IChatRepository _chatRepository;
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public RequestsController(
            IHubContext<ChatHub> hubContext,
            IChatRepository chatRepository,
            INotificationsRepository notificationsRepository,
            IUserRepository userRepository
            )
        {
            _hubContext = hubContext;
            _chatRepository = chatRepository;
            _notificationsRepository = notificationsRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Post(RequestDto requestDto)
        {
            var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();
            var userId = _userRepository.GetUserId(userEmail);
            _userRepository.SaveFriendRequest(requestDto.EndUserID, userId);
            SendNotification(userId, requestDto.EndUserID, "FriendRequest");
            return Ok();
        }

        //TDOD (Refactor)
        [HttpPut("Response")]
        public IActionResult Update(DtoNotification dtoNotification)
        {
            var userEmail =  HttpContext.User.RetrieveEmailFromPrincipal();
            var userId = _userRepository.GetUserId(userEmail);
            int notificationId = _userRepository.ResponseToFriendRequest(dtoNotification.UserId, dtoNotification.RequestResponse, userId);
            if (notificationId > 0)
            {
                var connectionId = _userRepository.GetUserConnectionId(userId);
                if (connectionId != null && connectionId.Count > 0)
                {
                    _hubContext.Clients.Clients(connectionId).SendAsync("RemoveNotification", notificationId);
                }

            }
            if (dtoNotification.RequestResponse == "Accepted")
            {
                SendNotification(userId, dtoNotification.UserId, "FriendRequestAccepted");
                List<string> connectionIds = _userRepository.GetUserConnectionId(new string[] { userId, dtoNotification.UserId });

            }
            return Ok();
        }

        public void SendNotification(string fromUserId, string toUserId, string notificationType)
        {
            int notificationId = _notificationsRepository.SaveUserNotification(notificationType, fromUserId, toUserId);
            var connectedId = _userRepository.GetUserConnectionId(toUserId);
            var user = _userRepository.GetUserById(fromUserId);
            if (connectedId != null && connectedId.Count > 0)
            {
                int notificationCount = _notificationsRepository.GetUseNNotificationCount(toUserId);

                DtoNotification notificationViewModel = new DtoNotification();
                notificationViewModel.NotificationType = notificationType;
                notificationViewModel.NotificationId = notificationId;
                notificationViewModel.NotificationCount = notificationCount;
                notificationViewModel.UserId = fromUserId;
                // notificationViewModel.Name = user.FirstName + " " + user.LastName;
                _hubContext.Clients.Clients(connectedId).SendAsync("SendNotification", notificationViewModel);
            }

        }

        public void RefreshOnLineUsersByConnectionIds(List<string> connectionIds, string userId = "")
        {
            _hubContext.Clients.Clients(connectionIds).SendAsync("RefreshOnlineUsers");
            if (!string.IsNullOrEmpty(userId))
            {
                var onlineStatus = _userRepository.GetUserOnlineStatus(userId);
                if (onlineStatus != null)
                {
                    _hubContext.Clients.Clients(connectionIds).SendAsync("RefreshOnlineUserByConnectionIds",
                        userId, onlineStatus.IsOnline, Convert.ToString(onlineStatus.LastUpdationTime));
                }

            }
        }
    }
}