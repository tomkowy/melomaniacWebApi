using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MelomaniacWebApi.Models
{
    public class Index
    {
        [BsonId]
        public long _id { get; set; }
        [DataMember, BsonElement]
        public string name { get; set; }
        [DataMember, BsonElement]
        public int value { get; set; }
    }
}