using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBClone.DataAccess.Abstract;

namespace FBClone.DataAccess.Concrete
{
    public class Message 
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int FriendId { get; set; }
        public virtual User Friend { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool isDelete { get; set; }
    }
}
