using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload_ProfilePhoto.Models;
using Microsoft.AspNetCore.Http;
using Upload_ProfilePhoto.Hubs;

namespace Upload_ProfilePhoto.Repositorys
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly ProfiteDbContext _context;
        public AccountRepository(ProfiteDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public int InsertUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                Save();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User Login(string email, string password)
        {
            try
            {
                var _user = _context.Users.Where(a => a.Email == email && a.Password == password).FirstOrDefault();
                if (_user != null)
                {
                    return _user;
                }
                else
                {
                    return _user;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Save()
        {
          return  _context.SaveChanges();
        }
        public User UpdateProfile(string profilepictureId)
        {
            User user = new User();
            try
            {
                string Id = _session.GetString("UserId");
                int userid = Convert.ToInt32(Id);
                //Save Profile in Gallery
                ProfilesPictureGallery pictureGallery = new ProfilesPictureGallery
                {
                    UserId = userid,
                    Picture = profilepictureId,
                    DateCreated = DateTime.Now
                };
                _context.ProfilesPictureGalleries.Add(pictureGallery);
                Save();
                //Set Profile in user Table

                user = _context.Users.Where(a => a.Id == userid).FirstOrDefault();
                if (user != null)
                {
                    user.ProfilePictureId = pictureGallery.Id;
                    _context.Users.Update(user);
                    Save();
                    return user;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
        public User GetUserById(int id)
        {
            return _context.Users.Where(a => a.Id == id).FirstOrDefault();
        }
        public Comman GetProfile(int id)
        {
            User user = _context.Users.Where(a => a.Id == id).FirstOrDefault();
            ProfilesPictureGallery profilesPicture = _context.ProfilesPictureGalleries.Where(a => a.Id == user.ProfilePictureId && a.DateDeleted == null).FirstOrDefault();
            Comman comman = new Comman();

            comman.Id = user.Id;
            comman.First_Name = user.First_Name;
            comman.Last_Name = user.Last_Name;
            comman.Email = user.Email;
            comman.ProfilePictureId = user.ProfilePictureId;
            if (profilesPicture != null)
            {
                comman.PictureName = profilesPicture.Picture;
            }
            else
            {
                comman.PictureName = "blank-profile-picture-973460_1280.png";
            }
            return comman;
        }
        public List<Comman> MyAllPrifiles()
        {
            try
            {
                string Id = _session.GetString("UserId");
                int userid = Convert.ToInt32(Id);
                List<ProfilesPictureGallery> profilesPictureGallery = _context.ProfilesPictureGalleries.Where(a => a.UserId == userid && a.DateDeleted == null).ToList();
                List<User> users = _context.Users.Where(a => a.Id == userid).ToList();

                var query = from a in profilesPictureGallery
                            join b in users on a.UserId equals b.Id
                            select new { a.Id, a.Picture, a.UserId, b.First_Name, b.Last_Name, b.Email, b.ProfilePictureId,a.DateCreated };
                string currentprofile = profilesPictureGallery.Where(a => a.Id == users[0].ProfilePictureId).Select(a => a.Picture).FirstOrDefault();

                List<Comman> commen = query.Select(x => new Comman
                {
                    Id = x.UserId,
                    First_Name = x.First_Name,
                    Last_Name = x.Last_Name,
                    Email = x.Email,
                    PictureName = x.Picture,
                    ProfilePictureId = x.ProfilePictureId,
                    currentProfile = currentprofile,
                    picureid = x.Id,
                    DateCreated=x.DateCreated


                }).OrderByDescending(a => a.DateCreated).ToList();

                return commen;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Comman> GetAllPicture()
        {
            try
            {
                List<ProfilesPictureGallery> profilesPictureGallery = _context.ProfilesPictureGalleries.Where(a=>a.DateDeleted == null).ToList();
                List<User> users = _context.Users.ToList();

                var query = from a in profilesPictureGallery
                            join b in users on a.UserId equals b.Id
                            select new { a.Id,a.DateCreated, a.Picture, a.UserId, b.First_Name, b.Last_Name, b.Email, b.ProfilePictureId};
                string currentprofile = profilesPictureGallery.Where(a => a.Id == users[0].ProfilePictureId).Select(a => a.Picture).FirstOrDefault();

                List<Comman> commen = query.Select(x => new Comman
                {
                    Id = x.UserId,
                    First_Name = x.First_Name,
                    Last_Name = x.Last_Name,
                    Email = x.Email,
                    PictureName = x.Picture,
                    ProfilePictureId = x.ProfilePictureId,
                    currentProfile = GetPictureById(x.ProfilePictureId),
                    picureid = x.Id,
                    DateCreated = x.DateCreated
                }).OrderByDescending(a => a.DateCreated).ToList();

                return commen;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public Comman CurrentUserPicture()
        {
            string Id = _session.GetString("UserId");
            int userid = Convert.ToInt32(Id);
            var user = _context.Users.Where(a => a.Id == userid).FirstOrDefault();

            string currentprofile = _context.ProfilesPictureGalleries.Where(a => a.Id == user.ProfilePictureId && a.DateDeleted == null).Select(a => a.Picture).FirstOrDefault();
            if (currentprofile == null)
            {
                currentprofile = "blank-profile-picture-973460_1280.png";
            }
            Comman comman = new Comman()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Id = user.Id,
                currentProfile = currentprofile,

            };

            return comman;
        }
        public int Like(int id, bool like)
        {
            try
            {
                int fileid = 0;
                string curuser = _session.GetString("UserId");
                int currentuser = Convert.ToInt32(curuser);
                ProfilesPictureGallery picture = _context.ProfilesPictureGalleries.Where(a => a.Id == id && a.DateDeleted == null).FirstOrDefault();
                if (picture == null)
                    return fileid;

                var userLikeExist = _context.pictureLikes.Where(a => a.UserId == currentuser && a.FileId == picture.Id).FirstOrDefault();

                if (like)
                {
                    if (userLikeExist != null)
                    {
                        return fileid;
                    }
                    _context.pictureLikes.Add(new PictureLike { FileId = picture.Id, UserId = currentuser, TimeStamp = DateTime.Now });
                    fileid = picture.Id;
                }
                else
                {
                    if (userLikeExist != null)
                    {
                        _context.pictureLikes.Remove(userLikeExist);
                    }
                }
                Save();
                return fileid;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<SetLike> GetLikes()
        {
            try
            {
                List<SetLike> setLikes = new List<SetLike>();
                string id = _session.GetString("UserId");
                int currUser = Convert.ToInt32(id);

                var _AllPicture = _context.ProfilesPictureGalleries.Where(a=>a.DateDeleted==null).ToList();
                for (int i = 0; i < _AllPicture.Count; i++)
                {
                    var data = new SetLike();
                    var _getlike = _context.pictureLikes.Where(a => a.FileId == _AllPicture[i].Id).ToList();
                    int count = 0;
                    if (_getlike.Count != 0)
                    {
                        data.PictureId = _AllPicture[i].Id;
                        for (int f = 0; f < _getlike.Count; f++)
                        {
                            if (_getlike[f].UserId == currUser)
                            {
                                data.CurrUserLike = true;
                            }
                        }

                        count = _getlike.Count;
                    }
                    else
                    {
                        data.PictureId = _AllPicture[i].Id;
                        data.CurrUserLike = false;
                        data.TotalLIkes = 0;
                    }
                    data.TotalLIkes = count;
                    setLikes.Add(data);

                }
                return setLikes;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserNotification Notification(int fileId, bool like)
        {
            try
            {
                string id = _session.GetString("UserId");
                int currUser = Convert.ToInt32(id);
                var _File = _context.ProfilesPictureGalleries.Where(a => a.Id == fileId).FirstOrDefault();
                var _notify = _context.UserNotifications.Where(a => a.PictureId == fileId && a.FriendId == currUser && a.Type == NotificationTypes.Liked).FirstOrDefault();
                UserNotification notif = new UserNotification();
                if (_File == null)
                    return notif;
                if (like)
                {
                    if (_notify == null)
                    {
                        notif.UserId = _File.UserId;
                        notif.FriendId = currUser;
                        notif.Type = NotificationTypes.Liked;
                        notif.PictureId = _File.Id;
                        notif.IsRead = false;
                        notif.DateCreated = DateTime.Now;
                        _context.UserNotifications.Add(notif);

                    }
                    else
                    {
                        notif = _notify;
                        notif.DateModified = DateTime.Now;
                        notif.IsRead = false;
                        notif.Type = NotificationTypes.Liked;
                        notif.DateDeleted = null;
                        _context.UserNotifications.Update(notif);
                    }

                }
                else
                {
                    if (_notify != null)
                    {
                        notif = _notify;
                        notif.DateDeleted = DateTime.Now;
                        notif.IsRead = false;
                        _context.UserNotifications.Update(notif);
                    }
                }
                Save();
                return notif;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public string GetPictureById(int pictureid)
        {
            try
            {
                string data = _context.ProfilesPictureGalleries.Where(a => a.Id == pictureid).Select(a => a.Picture).FirstOrDefault();
                if (data==null)
                {
                    data = "blank-profile-picture-973460_1280.png";
                }
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SetNotification> GetUserNotification()
        {
            List<SetNotification> SetNotification = new List<SetNotification>();
            string id = _session.GetString("UserId");
            int userid = int.Parse(id);
            int count = GetNotificaitonCount(userid);
            try
            {
                var allnotifi = _context.UserNotifications.Where(a => a.UserId == userid && a.DateDeleted == null).OrderByDescending(a => a.DateCreated).ToList();
                for (int i = 0; i < allnotifi.Count; i++)
                {
                    User user = GetUserById(allnotifi[i].FriendId);
                    string picture = GetPictureById(allnotifi[i].PictureId);
                    SetNotification notification = new SetNotification();
                    notification.NotfID = allnotifi[i].Id;
                    notification.UserName = user.First_Name +" "+ user.Last_Name;
                    notification.FriendId = allnotifi[i].FriendId;
                    notification.UserImage = GetPictureById(user.ProfilePictureId);
                    notification.Type = allnotifi[i].Type;
                    notification.Picture = picture;
                    notification.IsRead = allnotifi[i].IsRead;
                    notification.CreatedDate = allnotifi[i].DateCreated;
                    notification.Count = count;
                    notification.CommentId = allnotifi[i].CommentedId;
                    if (notification.CommentId != 0)
                    {
                        notification.CommentMessage = _context.PictureComments.Where(a => a.Id == notification.CommentId).Select(a => a.Message).FirstOrDefault();
                    }

                    SetNotification.Add(notification);

                };
                return SetNotification;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetNotificaitonCount(int? id)
        {
            string ids = _session.GetString("UserId");
            int userid = int.Parse(ids);
           return _context.UserNotifications.Where(a => a.UserId == id && a.IsRead == false && a.DateDeleted == null).ToList().Count;
        }
        public int UpdateNotification(int notifyid)
        {
            try
            {
                var notification = _context.UserNotifications.Where(a => a.Id == notifyid).FirstOrDefault();
                if (notification != null)
                {
                    notification.IsRead = true;
                    _context.UserNotifications.Update(notification);
                    Save();
                }
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CommentCount> CommentCountwisePicture()
        {
            try
            {
                List<CommentCount> commentCounts = new List<CommentCount>();
               var _Allpictures = _context.ProfilesPictureGalleries.ToList();
                for (int i = 0; i < _Allpictures.Count; i++)
                {
                    var Comment = new CommentCount();
                   var _getComment = _context.PictureComments.Where(a => a.PictureId == _Allpictures[i].Id && a.DateDeleted == null).ToList();

                    Comment.TotalCount = _getComment.Count;
                    Comment.PicutreId = _Allpictures[i].Id;
                    commentCounts.Add(Comment);
                }
                return commentCounts;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ProfilesPictureGallery DeletePost(int PictureId)
            {
            try
            {
                var _Picture = _context.ProfilesPictureGalleries.Where(a => a.Id == PictureId && a.DateDeleted == null).FirstOrDefault();
                if (_Picture != null)
                {
                    //Delete From ProfilesPictureGalleries Table
                    _Picture.DateDeleted = DateTime.Now;
                    _context.ProfilesPictureGalleries.Remove(_Picture);

                    //Delete From Picture Like Table
                    var _PictureLike = _context.pictureLikes.Where(a => a.FileId == _Picture.Id).ToList();
                    foreach (var item in _PictureLike)
                    {
                        _context.pictureLikes.Remove(item);
                    }

                    // Delete From Picture Comment Table
                    var _PictureComment = _context.PictureComments.Where(a => a.PictureId == _Picture.Id).ToList();
                    foreach (var comments in _PictureComment)
                    {
                        comments.DateDeleted = DateTime.Now;
                        _context.PictureComments.Remove(comments);

                        //Delete From CommentReplay
                        var _CommentReply = _context.PictureCommentReplays.Where(a => a.PictureCommentId == comments.Id).ToList();
                        foreach (var replay in _CommentReply)
                        {
                            replay.DateDeleted = DateTime.Now;
                            _context.PictureCommentReplays.Remove(replay);
                        }
                        //Delete From Comment Like
                        var _CommentLike = _context.PictureCommentsLikes.Where(a => a.CommentId == comments.Id).ToList();
                        foreach (var Commentlike in _CommentLike)
                        {
                            _context.PictureCommentsLikes.Remove(Commentlike);
                        }
                        //Delete From Notification
                        var _Notifiations = _context.UserNotifications.Where(a => a.PictureId == _Picture.Id).ToList();
                        foreach (var Notify in _Notifiations)
                        {
                            Notify.DateDeleted = DateTime.Now;
                            Notify.IsRead = false;
                            _context.UserNotifications.Remove(Notify);
                        }
                    }


                    Save();
                   
                }
                return _Picture;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BasicUserDTO GetUserByIdBasic(int id)
        {
            var user = GetUserById(id);
            BasicUserDTO basicUserDTO = new BasicUserDTO();
            if (user!=null)
            {
                basicUserDTO.Id = user.Id;
                basicUserDTO.UserName = user.First_Name + " " + user.Last_Name;
                basicUserDTO.Avtart = GetPictureById(user.ProfilePictureId);
            }
            return basicUserDTO;
        }

        public List<Comman> MyAllProfilesById(int userid)
        {
            try
            {
                List<ProfilesPictureGallery> profilesPictureGallery = _context.ProfilesPictureGalleries.Where(a => a.UserId == userid && a.DateDeleted == null).ToList();
                List<User> users = _context.Users.Where(a => a.Id == userid).ToList();

                var query = from a in profilesPictureGallery
                            join b in users on a.UserId equals b.Id
                            select new { a.Id, a.Picture, a.UserId, b.First_Name, b.Last_Name, b.Email, b.ProfilePictureId, a.DateCreated };
                string currentprofile = profilesPictureGallery.Where(a => a.Id == users[0].ProfilePictureId).Select(a => a.Picture).FirstOrDefault();

                List<Comman> commen = query.Select(x => new Comman
                {
                    Id = x.UserId,
                    First_Name = x.First_Name,
                    Last_Name = x.Last_Name,
                    Email = x.Email,
                    PictureName = x.Picture,
                    ProfilePictureId = x.ProfilePictureId,
                    currentProfile = currentprofile,
                    picureid = x.Id,
                    DateCreated = x.DateCreated
                }).OrderByDescending(a => a.DateCreated).ToList();

                return commen;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
