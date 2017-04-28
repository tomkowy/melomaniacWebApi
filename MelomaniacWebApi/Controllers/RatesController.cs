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
        //GET api/rates/GetAllTrackRates/5
        [HttpGet]
        public IEnumerable<Rate> GetAllTrackRates(int data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.GetTrackRates(data);
            return res;
        }

        //GET api/rates/GetTrackAverageRate/5
        [HttpGet]
        public string GetTrackAverageRate(int data)
        {
            DBConnection dbconn = new DBConnection();
            var res = dbconn.GetTrackRates(data);
            int sum = 0;
            foreach (var item in res)
            {
                sum += item.mark;
            }
            //można to policzyć w widoku
            int numberOfElements = ((List<Rate>)res).Count;
            double avg = sum / numberOfElements;

            string jsonRes = "{'avg':'" + avg 
                + "','sum':'" + sum 
                + "','numberOfElements':" + numberOfElements
                +"'}";

            return jsonRes;
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
