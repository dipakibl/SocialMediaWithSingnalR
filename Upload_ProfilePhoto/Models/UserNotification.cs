using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upload_ProfilePhoto.Models
{
    public class UserNotification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public int PictureId { get; set; }
        public string Type { get; set; }
        public int CommentedId { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
