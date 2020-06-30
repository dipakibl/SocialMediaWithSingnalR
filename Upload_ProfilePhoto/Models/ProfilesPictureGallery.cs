using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upload_ProfilePhoto.Models
{
    public class ProfilesPictureGallery
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Picture { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
