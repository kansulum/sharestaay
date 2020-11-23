using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;

namespace Api.Helpers
{
    public static class CommonFunctions
    {
        public static MessageModel GetMessageModel(ChatMessage objentity)
        {
            MessageModel objmodel = new MessageModel();
            objmodel.ChatMessageID = objentity. Id;
            objmodel.FromUserID = objentity.FromUserID;
            objmodel.ToUserID = objentity.ToUserID;
            objmodel.Message = objentity.Message;
            objmodel.Status = objentity.Status;
            objmodel.CreatedOn =Convert.ToString(objentity.CreatedOn);
            objmodel.UpdatedOn = Convert.ToString(objentity.UpdatedOn);
            objmodel.ViewedOn = Convert.ToString(objentity.ViewedOn);
            objmodel.IsActive = objentity.IsActive;
            return objmodel;
        }

    }
}