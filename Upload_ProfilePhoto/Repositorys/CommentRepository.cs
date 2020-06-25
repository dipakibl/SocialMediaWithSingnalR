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
        public CommentRepository(ProfiteDbContext context, IHttpContextAccessor httpContextAccessor)
        {

        }
        //public PictureComments SentComment(string Comment, int picureid)
        //{
        //    try
        //    {
        //        var _comment = new PictureComments();
        //        string UserId = 
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
