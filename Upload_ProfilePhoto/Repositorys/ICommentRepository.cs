using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload_ProfilePhoto.Models;

namespace Upload_ProfilePhoto.Repositorys
{
   public interface ICommentRepository
    {
        PictureComments SentComment(PictureComments comments);
        List<CommentDTO> GetComment();
        UserNotification CommentNotification(PictureComments comments);
        PictureComments DeleteComments(int commentid);
        PictureComments GetCommentsbyId(int commentId);
        UserNotification UpdateNotification(PictureComments comments);
        PictureCommentReplay PictureCommentReplay(PictureCommentReplay pictureCommentReplay);
        PictureCommentReplay GetCommentReplaybyId(int ReplayComId);
        List<CommentDTO> GetLlCommentReplays();
        PictureCommentsLike CommentsLike(int commentId, bool like);
        List<CommentLikeDTO> GetAllCommentLikes();
        int GetCommentLikeCountCommentIdWise(int CommentId);
        PictureCommentReplay DeleteCommentReplay(int ReplayCommentId);



    }
}
