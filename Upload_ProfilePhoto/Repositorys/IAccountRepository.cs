using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload_ProfilePhoto.Models;

namespace Upload_ProfilePhoto.Repositorys
{
   public interface IAccountRepository
    {
        void Save();
        int InsertUser(User user);
        User Login(string email, string password);
        User GetUserById(int id);
        User UpdateProfile(string profilepictureId);
        Comman GetProfile(int id);
        List<Comman> MyAllPrifiles();
        List<Comman> GetAllPicture();
        Comman CurrentUserPicture();
        int Like(int id, bool like);
        List<SetLike> GetLikes();
        UserNotification Notification(int fileId,bool like);
        string GetPictureById(int pictureid);
        List<SetNotification> GetUserNotification();
        int GetNotificaitonCount(int ?id);
        int UpdateNotification(int notifyid);
        List<CommentCount> CommentCountwisePicture();



    }
}
