using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Upload_ProfilePhoto.Models
{
    public class PictureLike
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

