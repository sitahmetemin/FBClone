using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBClone.DataAccess.Abstract;

namespace FBClone.DataAccess.Concrete
{
    public class Like : BaseEntity
    {
        public int Id { get; set; }

        
        public int UserId { get; set; }
        public User User { get; set; }

        
        public int SharedId { get; set; }
        public Shared Shared { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool isDelete { get; set; } = false;
    }
}
