namespace Core.Entities
{
    public class FriendRequests
    {
        public AppUser UserInfo { get; set; }
        public string RequestStatus { get; set; }
        public string RequestorUserID { get; set; }
        public string EndUserID { get; set; }
    }
}