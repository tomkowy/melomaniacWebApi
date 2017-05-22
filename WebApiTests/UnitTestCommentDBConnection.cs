using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MelomaniacWebApi.Logic;
using MelomaniacWebApi.Models;
using System.Collections.Generic;

namespace WebApiTests
{
    [TestClass]
    public class UnitTestCommentDBConnection
    {
        [TestMethod]
        public void AddComment()
        {
            DBConnection dbconn = new DBConnection();
            Comment com1 = new Comment() { content = "test 123", date = DateTime.Now, fb = "dlugieIdUżytkownika", soundcloud = 1555698 };
            bool res = dbconn.PostComment(com1);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void GetCommentByTrack()
        {
            DBConnection dbconn = new DBConnection();
            List<Comment> coms = dbconn.GetTrackComments(1555698) as List<Comment>;
            bool res = coms.Exists(x => x.fb == "dlugieIdUżytkownika" && x.content == "test 123" && x.soundcloud == 1555698);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void GetCommentByUser()
        {
            DBConnection dbconn = new DBConnection();
            List<Comment> coms = dbconn.GetUserComments("dlugieIdUżytkownika") as List<Comment>;
            bool res = coms.Exists(x => x.fb == "dlugieIdUżytkownika" && x.content == "test 123" && x.soundcloud == 1555698);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void EditComment()
        {
            DBConnection dbconn = new DBConnection();
            Comment com1 = new Comment() { content = "test 123", date = DateTime.Now, fb = "dlugieIdUżytkownika", soundcloud = 1555698 };
            dbconn.PostComment(com1);
            Comment com2 = (dbconn.GetUserComments("dlugieIdUżytkownika") as List<Comment>)[0];
            com2.content = "inny content";
            bool res = dbconn.EditComment(com2);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void DeleteComment()
        {
            DBConnection dbconn = new DBConnection();
            long commID = (dbconn.GetUserComments("dlugieIdUżytkownika") as List<Comment>)[0]._id;
            bool res = dbconn.DeleteComment(commID);
            Assert.IsTrue(res);
        }
    }
}
