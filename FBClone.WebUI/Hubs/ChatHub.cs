using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FBClone.DataAccess.Concrete;
using Microsoft.AspNet.SignalR;

namespace FBClone.WebUI.Hubs
{
    public class ChatHub : Hub
    {

        public void SendMessage(int FriendId, string FriendDisplayName, string Message, int SessionID)
        {
            FBContext _context = new FBContext();
            _context.Messages.Add(new Message()
            {
                UserId = SessionID,
                FriendId = FriendId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                isDelete = false,
                MessageContent = Message

            });
            if (_context.SaveChanges() > 0)
            {
                Clients.Others.GetMessageOther(FriendId, FriendDisplayName, Message);

                Clients.Caller.GetMessageCaller(Message);
            }
        }
        
    }
}