using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Extentions;
using Api.Helpers;
using Api.Hubs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers
{
    [Authorize]
    public class ChatController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public ChatController(IUserRepository userRepository, IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public  IActionResult Message(string id)
        {
            var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();
            var userId = _userRepository.GetUserId(userEmail);

            var messages = _chatRepository.GetChatMessagesByUserID(userId,id);
            // var userModel = CommonFunctions.GetUserModel(Id);
            var objmodel = new ChatMessageModel();
            // objmodel.UserDetail = userModel;
            objmodel.ChatMessages = messages.Messages.Select(m => CommonFunctions.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            var onlineStatus = _userRepository.GetUserOnlineStatus(id);
            if (onlineStatus != null)
            {
                objmodel.IsOnline = onlineStatus.IsOnline;
                objmodel.LastSeen = Convert.ToString(onlineStatus.LastUpdationTime);
            }
            return Ok(objmodel);
        }
    }
}