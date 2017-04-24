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
        public long _id { get; set; }
        [DataMember, BsonElement]
        public string fb { get; set; }
        [DataMember, BsonElement]
        public long soundcloud { get; set; }
        [DataMember, BsonElement]
        public string content { get; set; }
        [DataMember, BsonElement]
        public DateTime? date { get; set; }
    }
}