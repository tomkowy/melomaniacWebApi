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
    public class RatesController : ApiController
    {
        //GET api/rates/GetAllTrackRatess/5
        [HttpGet]
        public IEnumerable<Rate> GetAllTrackRates(int data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.GetTrackRates(data);
            return res;
        }
        //POST api/rates/EditRate/
        [HttpPost]
        public bool EditRate([FromBody]Rate data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.EditRate(data);
            return res;
        }
        //POST api/rates/DeleteRate/
        [HttpPost]
        public bool DeleteRate(long data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.DeleteRate(data);
            return res;
        }
        //POST api/rates/PostRate/
        [HttpPost]
        public bool PostRate([FromBody]Rate data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.PostRate(data);
            return res;
        }
    }
}
