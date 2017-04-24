using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MelomaniacWebApi.Models
{
    public class Rate
    {
        [BsonId]
        long _id { get; set; }
        string fbid { get; set; }
        long soundcloudid { get; set; }
        int mark { get; set; }
        DateTime date { get; set; }
    }
}