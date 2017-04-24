using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MelomaniacWebApi.Models
{ 
    public class Comment
    {
        [BsonId]
        long _id { get; set; }
        string fbid { get; set; }
        long soundcloudid { get; set; }
        string content { get; set; }
        DateTime date { get; set; }
    }
}