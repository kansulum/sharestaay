using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IChatRepository
    {
        ChatMessage SaveChatMessage(ChatMessage objentity);
        MessageRecords GetChatMessagesByUserID(string currentUserID, string toUserID, int lastMessageID = 0);
        void UpdateMessageStatusByUserID(string fromUserID, string currentUserID);
        void UpdateMessageStatusByMessageID(int messageID);
    }
}