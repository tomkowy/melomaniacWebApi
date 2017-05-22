using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MelomaniacWebApi.Logic;
using MelomaniacWebApi.Models;
using System.Collections.Generic;
using MelomaniacWebApi.Controllers;

namespace WebApiTests
{
    [TestClass]
    public class UnitTestRateDBConnection
    {
        [TestMethod]
        public void AddRate()
        {
            DBConnection dbconn = new DBConnection();
            Rate rate1 = new Rate() { date = DateTime.Now, fb = "asdasd123", mark = 1, soundcloud = 123123123 };
            bool res = dbconn.PostRate(rate1);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void GetRatesByTrack()
        {
            DBConnection dbconn = new DBConnection();
            List<Rate> rates = dbconn.GetTrackRates(123123123) as List<Rate>;
            bool res = rates.Exists(x => x.fb == "asdasd123" && x.soundcloud == 123123123);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void GetRatesByUser()
        {
            DBConnection dbconn = new DBConnection();
            List<Rate> rates = dbconn.GetUserRates("asdasd123") as List<Rate>;
            bool res = rates.Exists(x => x.fb == "asdasd123" && x.soundcloud == 123123123);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void EditRate()
        {
            DBConnection dbconn = new DBConnection();
            Rate rate1 = new Rate() { mark=1, date = DateTime.Now, fb = "asdasd123", soundcloud = 123123123 };
            dbconn.PostRate(rate1);
            Rate rate2 = (dbconn.GetUserRates("asdasd123") as List<Rate>)[0];
            rate2.mark = 2;
            bool res = dbconn.EditRate(rate2);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void DeleteRate()
        {
            DBConnection dbconn = new DBConnection();
            long rateID = (dbconn.GetUserRates("asdasd123") as List<Rate>)[0]._id;
            bool res = dbconn.DeleteRate(rateID);
            Assert.IsTrue(res);
        }
        [TestMethod]
        public void CalculateTrackRate() {
            DBConnection dbconn = new DBConnection();
            Rate rate1 = new Rate() { date = DateTime.Now, fb = "asdasd123", mark = 1, soundcloud = 321321 };
            dbconn.PostRate(rate1);
            Rate rate2 = new Rate() { date = DateTime.Now, fb = "asdasd123", mark = 3, soundcloud = 321321 };
            dbconn.PostRate(rate2);
            Rate rate3 = new Rate() { date = DateTime.Now, fb = "asdasd123", mark = 3, soundcloud = 321321 };
            dbconn.PostRate(rate3);

            RatesController rC = new RatesController();
            string asd = rC.GetTrackAverageRate(321321);

            Assert.IsTrue(!string.IsNullOrEmpty(asd)&& asd.Contains("avg") && asd.Contains("sum") && asd.Contains("numberOfElements"));
        }
    }
}
