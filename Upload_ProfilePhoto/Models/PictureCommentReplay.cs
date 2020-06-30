using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upload_ProfilePhoto.Models
{
    public class PictureCommentReplay
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int PictureCommentId { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
