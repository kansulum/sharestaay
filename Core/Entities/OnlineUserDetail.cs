using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class OnlineUserDetail
    {
        public string UserId { get; set; }
        public List<string> ConnectionId { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public int UnReadMessageCount { get; set; }
        public DateTime LastUpdationTime { get; set; }
    }
    
}