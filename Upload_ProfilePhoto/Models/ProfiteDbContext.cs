using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upload_ProfilePhoto.Models
{
    public class ProfiteDbContext:DbContext
    {
        public ProfiteDbContext(DbContextOptions<ProfiteDbContext> options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ProfilesPictureGallery> ProfilesPictureGalleries { get; set; }
        public DbSet<PictureLike> pictureLikes { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<PictureComments> PictureComments { get; set; }
        public DbSet<PictureCommentReplay> PictureCommentReplays { get; set; }
        public DbSet<PictureCommentsLike> PictureCommentsLikes { get; set; }

    }
}
