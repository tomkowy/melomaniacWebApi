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
        private const string INDEXESTABLENAME = "indexes";
        IMongoClient client;
        IMongoDatabase database;

        public DBConnection()
        {
            string uri ;
            try
            {
                uri = ConfigurationManager.ConnectionStrings["CommentRateConnectionString"].ConnectionString;
            }
            catch (Exception ex)
            {
                uri = @"mongodb://admin:passw0rd123@ds060749.mlab.com:60749/melomandb";
            }           
            string dbname = "melomandb";

            client = new MongoClient(uri);
            database = client.GetDatabase(dbname);
        }

        /// <summary>
        /// Method searching for comments of a song specified in the parameter
        /// </summary>
        /// <param name="trackID">ID of a track</param>
        /// <returns>Comments on this song</returns>
        public IEnumerable<Comment> GetTrackComments(long trackID)
        {
            var collection = database.GetCollection<BsonDocument>(COMMENTSTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("soundcloud", trackID);
            var sort = Builders<BsonDocument>.Sort.Descending("date");

            var foundEntries = collection.Find(filter).Sort(sort).ToList();
            List<Comment> comments = new List<Comment>();

            foreach (var item in foundEntries)
            {
                comments.Add(BsonSerializer.Deserialize<Comment>(item));
            }

            return comments;
        }

        public IEnumerable<Comment> GetUserComments(string userID)
        {
            var collection = database.GetCollection<BsonDocument>(COMMENTSTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("fb", userID);
            var sort = Builders<BsonDocument>.Sort.Descending("date");

            var foundEntries = collection.Find(filter).Sort(sort).ToList();
            List<Comment> comments = new List<Comment>();

            foreach (var item in foundEntries)
            {
                comments.Add(BsonSerializer.Deserialize<Comment>(item));
            }

            return comments;
        }

        //prawdopodobnie metoda nie jest potrzebna
        public Comment GetComment(int commentID)
        {
            throw new NotImplementedException();
        }

        public bool PostComment(Comment commentToPost)
        {

            commentToPost.date = DateTime.Now;
            BsonDocument bsonComment = new BsonDocument();

            try
            {
                commentToPost._id = GetNextObjectID("comments");
                bsonComment = commentToPost.ToBsonDocument();

                var collection = database.GetCollection<BsonDocument>(COMMENTSTABLENAME);
                collection.InsertOne(bsonComment);
                return true;
            }
            catch (Exception e)
            {
#if (DEBUG)
                throw e;
#endif
                return false;
            }
        }

        public bool DeleteComment(long commentID)
        {
            var collection = database.GetCollection<BsonDocument>(COMMENTSTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", commentID);
            var result = collection.DeleteOne(filter);

            return result.DeletedCount > 0;
        }

        public bool EditComment(Comment commentToEdit)
        {
            var collection = database.GetCollection<BsonDocument>(COMMENTSTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", commentToEdit._id);
            var update = Builders<BsonDocument>.Update
                .Set("content", commentToEdit.content)
                .Set("date", DateTime.Now);
            var result = collection.UpdateOne(filter, update);

            return result.ModifiedCount > 0;
        }



        public IEnumerable<Rate> GetTrackRates(long trackID)
        {
            var collection = database.GetCollection<BsonDocument>(RATESTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("soundcloud", trackID);
            var sort = Builders<BsonDocument>.Sort.Descending("date");

            var foundEntries = collection.Find(filter).Sort(sort).ToList();
            List<Rate> rates = new List<Rate>();

            foreach (var item in foundEntries)
            {
                rates.Add(BsonSerializer.Deserialize<Rate>(item));
            }

            return rates;
        }

        public IEnumerable<Rate> GetUserRates(string userID)
        {
            var collection = database.GetCollection<BsonDocument>(RATESTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("fb", userID);
            var sort = Builders<BsonDocument>.Sort.Descending("date");

            var foundEntries = collection.Find(filter).Sort(sort).ToList();
            List<Rate> rates = new List<Rate>();

            foreach (var item in foundEntries)
            {
                rates.Add(BsonSerializer.Deserialize<Rate>(item));
            }

            return rates;
        }

        //ta metoda także może okazać się nieprzydatna
        public Rate GetRate(int rateID)
        {
            throw new NotImplementedException();
        }

        public bool PostRate(Rate rateToPost)
        {
            rateToPost.date = DateTime.Now;
            BsonDocument bsonRate = new BsonDocument();

            try
            {
                rateToPost._id = GetNextObjectID("rates");
                bsonRate = rateToPost.ToBsonDocument();

                var collection = database.GetCollection<BsonDocument>(RATESTABLENAME);
                var con1 = Builders<BsonDocument>.Filter.Eq("fb", rateToPost.fb);
                var con2 = Builders<BsonDocument>.Filter.Eq("soundcloud", rateToPost.soundcloud);
                var conArr = (new List<FilterDefinition<BsonDocument>>() { con1, con2 }).ToArray();
                var filter = Builders<BsonDocument>.Filter.And(conArr);

                var sort = Builders<BsonDocument>.Sort.Descending("date");
                var conflictedRates = collection.Find(filter).Sort(sort).ToList();
                foreach (var rate in conflictedRates)
                {
                    this.DeleteRate(BsonSerializer.Deserialize<Rate>(rate)._id);
                }

                collection.InsertOne(bsonRate);
                return true;
            }
            catch (Exception e)
            {
#if (DEBUG)
                throw e;
#endif
                return false;
            }
        }

        public bool DeleteRate(long rateID)
        {
            var collection = database.GetCollection<BsonDocument>(RATESTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", rateID);
            var result = collection.DeleteOne(filter);

            return result.DeletedCount > 0;
        }

        public bool EditRate(Rate rateToEdit)
        {
            var collection = database.GetCollection<BsonDocument>(RATESTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", rateToEdit._id);
            var update = Builders<BsonDocument>.Update
                .Set("mark", rateToEdit.mark)
                .Set("date", DateTime.Now);
            var result = collection.UpdateOne(filter, update);

            return result.ModifiedCount > 0;
        }


        private int GetNextObjectID(string indexName)
        {
            var collection = database.GetCollection<BsonDocument>(INDEXESTABLENAME);
            var filter = Builders<BsonDocument>.Filter.Eq("name", indexName);

            var foundEntries = collection.Find(filter).ToList();

            int value = BsonSerializer.Deserialize<Index>(foundEntries[0]).value;


            var update = Builders<BsonDocument>.Update
                .Set("value", value + 1);

            collection.UpdateOne(filter, update);

            return value;
        }
    }
}