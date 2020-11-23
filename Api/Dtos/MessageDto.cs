using Core.Entities;

namespace Api.Dtos
{
    public class MessageDto
    {
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public string FromUserId { get; set; }
        public string FromUserName { get; set; }
        public string FromUserProfilePic { get; set; }
        public string ToUserName { get; set; }
        public string ToUserProfilePic { get; set; }
        public MessageModel MessageModel { get; set; }
        
        
    }
}