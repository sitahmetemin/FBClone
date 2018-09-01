using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBClone.DataAccess.Concrete
{
    public class FBContext : DbContext
    {
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Shared> Shareds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.User)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Friend)
                .WithMany(t => t.MessagesFriend)
                .HasForeignKey(m => m.FriendId)
                .WillCascadeOnDelete(false);
        }

    }
}
