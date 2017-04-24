using MelomaniacWebApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MelomaniacWebApi.Logic
{
    public class DBConnection
    {
        private const string RATESTABLENAME = "rates";
        private const string COMMENTSTABLENAME = "comments";
        IMongoClient client;
        IMongoDatabase database;

        public DBConnection()
        {
            string uri = ConfigurationManager.ConnectionStrings["CommentRateConnectionString"].ConnectionString;
            string dbname = "melomandb";

            client = new MongoClient(uri);
            database = client.GetDatabase(dbname);
        }

        public IEnumerable<Comment> GetTrackComments(long trackID)
        {
            var rates = database.GetCollection<BsonDocument>(COMMENTSTABLENAME);

            var filter = Builders<BsonDocument>.Filter.Eq("soundcloudid", trackID);
            var sort = Builders<BsonDocument>.Sort.Descending("date");

            var foundEntries = rates.Find(filter).Sort(sort).ToList();

            List<Comment> comments = new List<Comment>();

            foreach (var item in foundEntries)
            {
                comments.Add(BsonSerializer.Deserialize<Comment>(item));
            }

            return comments;
        }

        public IEnumerable<Comment> GetUserComments(string userID)
        {
            throw new NotImplementedException();
        }

        public Comment GetComment(int commentID)
        {
            throw new NotImplementedException();
        }

        public bool PostComment(Comment commentToPost)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(int commentID)
        {
            throw new NotImplementedException();
        }

        public bool EditComment(Comment commentToEdit)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<Rate> GetTrackRates(long trackID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rate> GetUserRates(string userID)
        {
            throw new NotImplementedException();
        }

        public Rate GetRate(int rateID)
        {
            throw new NotImplementedException();
        }

        public bool PostRate(Rate rateToPost)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRate(int rateID)
        {
            throw new NotImplementedException();
        }

        public bool EditRate(Rate rateToEdit)
        {
            throw new NotImplementedException();
        }
    }
}