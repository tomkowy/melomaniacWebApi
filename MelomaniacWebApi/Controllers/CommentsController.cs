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
        public IEnumerable<Comment> GetAllTrackComments(int data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.GetTrackComments(data);
            return res;
        }
        //POST api/comments/EditComment/
        [HttpPost]
        public bool EditComment([FromBody]Comment data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.EditComment(data);
            return res;
        }
        //POST api/comments/DeleteComment/
        [HttpPost]
        public bool DeleteComment(long data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.DeleteComment(data);
            return res;
        }
        //POST api/comments/PostComment/
        [HttpPost]
        public bool PostComment([FromBody]Comment data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.PostComment(data);
            return res;
        }
        //GET api/comments/GetUserComments/
        [HttpGet]
        public IEnumerable<Comment> GetUserComments(string data) {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.GetUserComments(data);
            return res;
        }
    }
}
