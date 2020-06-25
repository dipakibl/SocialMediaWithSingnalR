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
        public void Save()
        {
            _context.SaveChanges();
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
                    Picture = profilepictureId
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
            ProfilesPictureGallery profilesPicture = _context.ProfilesPictureGalleries.Where(a => a.Id == user.ProfilePictureId).FirstOrDefault();
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
                List<ProfilesPictureGallery> profilesPictureGallery = _context.ProfilesPictureGalleries.Where(a => a.UserId == userid).ToList();
                List<User> users = _context.Users.Where(a => a.Id == userid).ToList();

                var query = from a in profilesPictureGallery
                            join b in users on a.UserId equals b.Id
                            select new { a.Id, a.Picture, a.UserId, b.First_Name, b.Last_Name, b.Email, b.ProfilePictureId };
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
                    picureid = x.Id


                }).OrderByDescending(a => a.picureid).ToList();

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
                List<ProfilesPictureGallery> profilesPictureGallery = _context.ProfilesPictureGalleries.ToList();
                List<User> users = _context.Users.ToList();

                var query = from a in profilesPictureGallery
                            join b in users on a.UserId equals b.Id
                            select new { a.Id, a.Picture, a.UserId, b.First_Name, b.Last_Name, b.Email, b.ProfilePictureId };
                string currentprofile = profilesPictureGallery.Where(a => a.Id == users[0].ProfilePictureId).Select(a => a.Picture).FirstOrDefault();

                List<Comman> commen = query.Select(x => new Comman
                {
                    Id = x.UserId,
                    First_Name = x.First_Name,
                    Last_Name = x.Last_Name,
                    Email = x.Email,
                    PictureName = x.Picture,
                    ProfilePictureId = x.ProfilePictureId,
                    currentProfile =GetPictureById(x.ProfilePictureId),
                    picureid = x.Id


                }).OrderByDescending(a => a.picureid).ToList();

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

            string currentprofile = _context.ProfilesPictureGalleries.Where(a => a.Id == user.ProfilePictureId).Select(a => a.Picture).FirstOrDefault();
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
                ProfilesPictureGallery picture = _context.ProfilesPictureGalleries.Where(a => a.Id == id).FirstOrDefault();
                if (picture == null)
                    return fileid;


                var userLikeExist = _context.pictureLikes.Where(a => a.UserId == currentuser && a.FileId == picture.Id).FirstOrDefault();

                if (like)
                {
                    if (userLikeExist != null)
                    {

                        return fileid;
                    }

                    _context.pictureLikes.Add(new PictureLike { FileId = picture.Id, UserId = currentuser, TimeStamp = DateTime.UtcNow });
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


            throw new NotImplementedException();
        }
        public List<SetLike> GetLikes()
        {
            try
            {
                List<SetLike> setLikes = new List<SetLike>();
                string id = _session.GetString("UserId");
                int currUser = Convert.ToInt32(id);

                var _AllPicture = _context.ProfilesPictureGalleries.ToList();
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
                var _notify = _context.UserNotifications.Where(a => a.PictureId == fileId && a.FriendId == currUser).FirstOrDefault();
                UserNotification notif = new UserNotification();
                if (_File == null)
                    return notif;
                if (like)
                {
                    if (_notify == null)
                    {
                        notif.UserId = _File.UserId;
                        notif.FriendId = currUser;
                        notif.Type = "Liked your Photo";
                        notif.PictureId = _File.Id;
                        notif.IsRead = false;
                        notif.DateCreated = DateTime.UtcNow;
                        _context.UserNotifications.Add(notif);
                        
                    }
                    else
                    {
                        notif = _notify;
                        notif.DateModified = DateTime.UtcNow;
                        notif.IsRead = false;
                        notif.Type = "Liked your Photo";
                        notif.DateDeleted = null;
                        _context.UserNotifications.Update(notif);
                    }

                }
                else
                {
                    if (_notify != null)
                    {
                        notif = _notify;
                        notif.DateDeleted = DateTime.UtcNow;
                        notif.IsRead = false;
                        notif.Type = "Unliked your Photo";
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
                return _context.ProfilesPictureGalleries.Where(a => a.Id == pictureid).Select(a => a.Picture).FirstOrDefault();
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
                    notification.UserName = user.First_Name + user.Last_Name;
                    notification.Type = allnotifi[i].Type;
                    notification.Picture = picture;
                    notification.IsRead = allnotifi[i].IsRead;
                    notification.CreatedDate = allnotifi[i].DateCreated;
                    notification.Count = count;

                    SetNotification.Add(notification);

                };
                return SetNotification;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetNotificaitonCount(int ?id)
        {
            string ids = _session.GetString("UserId");
            int userid = int.Parse(ids);

            return _context.UserNotifications.Where(a => a.UserId == id && a.IsRead == false && a.DateDeleted == null).Count();
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
    }
}
