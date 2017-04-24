using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MelomaniacWebApi.Models
{
    public class Rate
    {
        [BsonId]
        long _id { get; set; }
        [DataMember, BsonElement]
        string fb { get; set; }
        [DataMember, BsonElement]
        long soundcloud { get; set; }
        [DataMember, BsonElement]
        int mark { get; set; }
        [DataMember, BsonElement]
        DateTime? date { get; set; }
    }
}