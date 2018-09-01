using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBClone.DataAccess.Concrete;

namespace FBClone.WebUI.Models
{
    public class UserViewModels
    {
        public virtual List<User> Users { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Shared> Shareds { get; set; }
        public virtual List<Page> Pages { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}