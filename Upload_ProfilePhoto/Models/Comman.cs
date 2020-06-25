using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upload_ProfilePhoto.Models
{
    public class Comman
    {


        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ProfilePictureId { get; set; }
        public string PictureName { get; set; }
        public string currentProfile { get; set; }
        public int picureid { get; set; }
    }
    public class SetLike
    {
        public bool CurrUserLike { get; set; }
        public int PictureId { get; set; }
        public int TotalLIkes { get; set; }
    }
    public class SetNotification
    {
        public int NotfID { get; set; }
        public int Count { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
