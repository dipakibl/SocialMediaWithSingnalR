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
        public DateTime DateCreated { get; set; }
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
        public int FriendId { get; set; }
        public string UserImage { get; set; }
        public string Picture { get; set; }
        public NotificationTypes Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentId { get; set; }
        public string CommentMessage { get; set; }
    }
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int ReplyCommentId { get; set; }
        public int Userid { get; set; }
        public int CommentPitcureId { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalComment { get; set; }
    }
    public class CommentCount
    {
        public int PicutreId { get; set; }
        public int TotalCount { get; set; }
    }
    public class CommentLikeDTO
    {
        public int CommentId { get; set; }
        public bool CurrentUserLike { get; set; }
        public int TotalLike { get; set; }

    }
    public class SuggesteFriendDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Profile { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
    }
    public enum ConnectionStatus:short
    {
        NotYetConnection,
        PendingConnection,
        AlreadyInvited,
        AlreadyConnection   
    }
    public enum NotificationTypes : short
    {
        Liked,
        Commented,
        FriendRequested,
        AcceptFriendRequest
    }
    public class BasicUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Avtart { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public int Count { get; set; }
    }
    public class FriendDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public BasicUserDTO FriendUser { get; set; }
    }
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public BasicUserDTO FriendUser { get; set; }
        public bool IsRightMessage { get; set; }
    }
    public class LastMessageDTO
    {
        public int Id { get; set; }
        public int LastMessageId { get; set; }
        public string LastMessage { get; set; }
        public bool IsRead { get; set; }
        public bool IsOnline { get; set; }
        public DateTime? MessageDatetime { get; set; }
        public int LastSender { get; set; }
        public BasicUserDTO Friend { get; set; }
        public int Count { get; set; }

    }
    public class Result<T>
    {
        public Result()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// The outout if the operation
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        /// Total count of data
        /// </summary>
        public long Count { get; set; }
    }


}
