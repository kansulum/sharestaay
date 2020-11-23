using System;

namespace Api.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public bool ConfirmEmail { get; set; }
        public string Role { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public string Error { get; set; }
        public string LoginError { get; set; }
        public string FormType { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Bio { get; set; }
        public string Age { get; set; }
        public string FriendRequestStatus { get; set; }
        public int FriendRequestorID { get; set; }
        public int FriendEndUserID { get; set; }
        public bool IsRequestReceived { get; set; }
        public int FriendMappingID { get; set; }
        public bool IsOnline { get; set; }
        public string UnReadMessages { get; set; }
    }
}