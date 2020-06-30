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
        int PictureWiseCommentCount();
        UserNotification UpdateNotification(PictureComments comments);
        PictureCommentReplay PictureCommentReplay(PictureCommentReplay pictureCommentReplay);
        PictureCommentReplay GetCommentReplaybyId(int ReplayComId);
        List<CommentDTO> GetLlCommentReplays();



    }
}
