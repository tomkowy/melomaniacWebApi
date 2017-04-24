using MelomaniacWebApi.Logic;
using MelomaniacWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MelomaniacWebApi.Controllers
{
    public class CommentsController : ApiController
    {
        //GET api/comments/GetAllTrackComments/5
        [HttpGet]
        public IEnumerable<Comment> GetAllTrackComments(int trackID)
        {
            DBConnection dbconn = new DBConnection();
            return dbconn.GetTrackComments(trackID);
        }
        //POST api/comments/EditComment/id=5&comment=commentcontent
        [HttpPost]
        public bool EditComment(int id, string content)
        {
            DBConnection dbconn = new DBConnection();
            return dbconn.EditComment(new Comment());
        }
        //POST api/comments/DeleteComment/5
        [HttpPost]
        public bool DeleteComment(int id)
        {
            throw new NotImplementedException();
        }
        //POST api/comments/PostComment/comment
        [HttpPost]
        public bool PostComment(Comment commentToPost)
        {
            throw new NotImplementedException();
        }
    }
}
