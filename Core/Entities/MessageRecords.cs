using System.Collections.Generic;

namespace Core.Entities
{
    public class MessageRecords
    {
        public List<ChatMessage> Messages { get; set; }
        public int TotalMessages { get; set; }
        public int LastChatMessageId { get; set; }
    }
}