using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBClone.DataAccess.Abstract;

namespace FBClone.DataAccess.Concrete
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool isDelete { get; set; }


        public ICollection<Friend> Friends { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Shared> Shareds { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Message> MessagesFriend { get; set; }
        public ICollection<Page> Pages { get; set; }

    }
}
