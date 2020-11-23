using System;

namespace Core.Entities
{
    public class FriendMapping
    {
        public int Id { get; set; }
        public string RequestorUserID { get; set; }
        public string EndUserID { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}