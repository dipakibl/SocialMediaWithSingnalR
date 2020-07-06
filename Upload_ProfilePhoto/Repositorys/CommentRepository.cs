using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload_ProfilePhoto.Models;

namespace Upload_ProfilePhoto.Repositorys
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly ProfiteDbContext _context;
        private readonly IAccountRepository _accountRepository;
        public CommentRepository(ProfiteDbContext context, IHttpContextAccessor httpContextAccessor, IAccountRepository accountRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _accountRepository = accountRepository;
        }
        public PictureComments SentComment(PictureComments comments)
        {
            try
            {
                var _comment = new PictureComments();
                if (comments.Id != 0)
                {
                    _comment = _context.PictureComments.Where(a => a.Id == comments.Id).FirstOrDefault();
                    _comment.Message = comments.Message;
                    _comment.DateModified = DateTime.Now;
                    _comment.DateDeleted = null;
                    _context.PictureComments.Update(_comment);
                }
                else
                {
                    string UserId = _session.GetString("UserId");
                    _comment.Message = comments.Message;
                    _comment.UserId = int.Parse(UserId);
                    _comment.PictureId = comments.PictureId;
                    _comment.DateCreated = DateTime.Now;
                    _context.PictureComments.Add(_comment);
                }
                _accountRepository.Save();
                return _comment;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CommentDTO> GetComment()
        {
            List<CommentDTO> ALLComments = new List<CommentDTO>();
            try
            {
                var _allComement = _context.PictureComments.Where(a => a.DateDeleted == null).ToList();
                for (int i = 0; i < _allComement.Count; i++)
                {

                    var User = _accountRepository.GetUserById(_allComement[i].UserId);
                    CommentDTO comment = new CommentDTO();
                    comment.CommentId = _allComement[i].Id;
                    comment.Userid = User.Id;
                    comment.CommentPitcureId = _allComement[i].PictureId;
                    comment.UserName = User.First_Name + " " + User.Last_Name;
                    comment.UserPicture = _accountRepository.GetPictureById(User.ProfilePictureId);
                    comment.Message = _allComement[i].Message;
                    comment.CreatedDate = _allComement[i].DateCreated;
                    comment.TotalComment = _accountRepository.CommentCountwisePicture().Where(a => a.PicutreId == comment.CommentPitcureId).Select(a => a.TotalCount).FirstOrDefault();
                    ALLComments.Add(comment);
                }
                return ALLComments;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserNotification CommentNotification(PictureComments comments)
        {
            string id = _session.GetString("UserId");
            int currUser = Convert.ToInt32(id);
            var _File = _context.ProfilesPictureGalleries.Where(a => a.Id == comments.PictureId).FirstOrDefault();
            var _notify = _context.UserNotifications.Where(a => a.PictureId == comments.PictureId && a.FriendId == currUser && a.CommentedId == comments.Id && a.Type == "Commented on your photo").FirstOrDefault();
            UserNotification notif = new UserNotification();
            if (_notify == null)
            {
                notif.CommentedId = comments.Id;
                notif.UserId = _File.UserId;
                notif.FriendId = currUser;
                notif.Type = "Commented on your photo";
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
                notif.Type = "Commented on your photo";
                notif.DateDeleted = null;
                _context.UserNotifications.Update(notif);
            }
            _accountRepository.Save();
            return notif;
        }

        public PictureComments DeleteComments(int commentid)
        {
            try
            {
                PictureComments pictureComments = _context.PictureComments.Where(a => a.Id == commentid).FirstOrDefault();
                pictureComments.DateDeleted = DateTime.Now;
                _context.PictureComments.Update(pictureComments);

                //Delete Comment Replay
                var pictureCommentReplay = _context.PictureCommentReplays.Where(a => a.PictureCommentId == commentid).ToList();

                foreach (var item in pictureCommentReplay)
                {
                    item.DateDeleted = DateTime.Now;
                    _context.PictureCommentReplays.Update(item);

                }
                _accountRepository.Save();
                return pictureComments;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public PictureComments GetCommentsbyId(int commentId)
        {
            return _context.PictureComments.Where(a => a.Id == commentId).FirstOrDefault();
        }

        public UserNotification UpdateNotification(PictureComments comments)
        {
            var _notification = _context.UserNotifications.Where(a => a.CommentedId == comments.Id).FirstOrDefault();
            _notification.DateDeleted = DateTime.Now;
            _notification.IsRead = false;
            _context.UserNotifications.Update(_notification);
            _accountRepository.Save();
            return _notification;
        }

        public PictureCommentReplay PictureCommentReplay(PictureCommentReplay pictureCommentReplay)
        {
            try
            {
                var _replaycomments = new PictureCommentReplay();
                string UserId = _session.GetString("UserId");
                var _getComment = GetCommentsbyId(pictureCommentReplay.PictureCommentId);
                if (_getComment != null)
                {
                    if (pictureCommentReplay.Id == 0)
                    {
                        _replaycomments.Message = pictureCommentReplay.Message;
                        _replaycomments.PictureCommentId = pictureCommentReplay.PictureCommentId;
                        _replaycomments.FriendId = _getComment.UserId;
                        _replaycomments.UserId = int.Parse(UserId);
                        _replaycomments.DateCreated = DateTime.Now;
                        _context.PictureCommentReplays.Add(_replaycomments);
                    }
                    else
                    {
                        _replaycomments = GetCommentReplaybyId(pictureCommentReplay.Id);
                        if (_replaycomments != null)
                        {
                            _replaycomments.Message = pictureCommentReplay.Message;
                            _replaycomments.DateModified = DateTime.Now;
                            _replaycomments.DateDeleted = null;
                            _context.PictureCommentReplays.Update(_replaycomments);
                        }
                    }
                }
                _accountRepository.Save();
                return _replaycomments;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public PictureCommentReplay GetCommentReplaybyId(int ReplayComId)
        {

            return _context.PictureCommentReplays.Where(a => a.Id == ReplayComId).FirstOrDefault();
        }

        public List<CommentDTO> GetLlCommentReplays()
        {
            List<CommentDTO> commentDTO = new List<CommentDTO>();
            try
            {
                var _AllCommentsReply = _context.PictureCommentReplays.Where(a => a.DateDeleted == null).ToList();
                for (int i = 0; i < _AllCommentsReply.Count; i++)
                {
                    var User = _accountRepository.GetUserById(_AllCommentsReply[i].UserId);
                    CommentDTO ReplyComment = new CommentDTO();
                    ReplyComment.ReplyCommentId = _AllCommentsReply[i].Id;
                    ReplyComment.CommentId = _AllCommentsReply[i].PictureCommentId;
                    ReplyComment.CommentPitcureId = GetCommentsbyId(ReplyComment.CommentId).PictureId;
                    ReplyComment.Message = _AllCommentsReply[i].Message;
                    ReplyComment.Userid = User.Id;
                    ReplyComment.UserName = User.First_Name + " " + User.Last_Name;
                    ReplyComment.UserPicture = _accountRepository.GetPictureById(User.ProfilePictureId);
                    ReplyComment.CreatedDate = _AllCommentsReply[i].DateCreated;
                    ReplyComment.TotalComment = _context.PictureCommentReplays.Where(a => a.PictureCommentId == _AllCommentsReply[i].PictureCommentId && a.DateDeleted == null).ToList().Count();
                    commentDTO.Add(ReplyComment);
                }
                return commentDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PictureCommentsLike CommentsLike(int commentId, bool like)
        {
            PictureCommentsLike commentsLike = new PictureCommentsLike();
            try
            {
                string userid = _session.GetString("UserId");
                var _Comment = GetCommentsbyId(commentId);
                if (_Comment == null)
                    return commentsLike;

                var _CommentLikeExist = _context.PictureCommentsLikes.Where(a => a.UserId == int.Parse(userid) && a.CommentId == _Comment.Id).FirstOrDefault();
                if (like)
                {
                    if (_CommentLikeExist != null)
                    {
                        commentsLike = _CommentLikeExist;
                        return commentsLike;
                    }
                    commentsLike.CommentId = _Comment.Id;
                    commentsLike.UserId = int.Parse(userid);
                    commentsLike.TimeStamp = DateTime.Now;
                    _context.PictureCommentsLikes.Add(commentsLike);
                }
                else
                {
                    if (_CommentLikeExist != null)
                    {
                        commentsLike = _CommentLikeExist;
                        _context.PictureCommentsLikes.Remove(commentsLike);
                    }
                }
                _accountRepository.Save();
                return commentsLike;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CommentLikeDTO> GetAllCommentLikes()
        {
            var _CommentLikeList = new List<CommentLikeDTO>();
            try
            {
                string CurrUserId = _session.GetString("UserId");
                var _CommentLikes = _context.PictureCommentsLikes.ToList();
                for (int i = 0; i < _CommentLikes.Count; i++)
                {
                    var Commlikes = new CommentLikeDTO();
                    Commlikes.CommentId = _CommentLikes[i].CommentId;
                    if (_CommentLikes[i].UserId == int.Parse(CurrUserId))
                    {
                        Commlikes.CurrentUserLike = true;
                    }
                    Commlikes.TotalLike = _CommentLikes.Where(a => a.CommentId == _CommentLikes[i].CommentId).ToList().Count;
                    _CommentLikeList.Add(Commlikes);
                }
                return _CommentLikeList;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetCommentLikeCountCommentIdWise(int CommentId)
        {
            if (CommentId != 0)
            {
                return _context.PictureCommentsLikes.Where(a => a.CommentId == CommentId).ToList().Count;
            }
            return 0;
        }

        public PictureCommentReplay DeleteCommentReplay(int ReplayCommentId)
        {
            try
            {
                var pictureCommentReplay = _context.PictureCommentReplays.Where(a => a.Id  == ReplayCommentId).FirstOrDefault();
                pictureCommentReplay.DateDeleted = DateTime.Now;
                _context.PictureCommentReplays.Update(pictureCommentReplay);
                _accountRepository.Save();
                return pictureCommentReplay;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
