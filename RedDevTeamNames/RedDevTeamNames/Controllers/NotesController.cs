using RedDevTeamNames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Configuration;

namespace RedDevTeamNames.Controllers
{
    public class NotesController : ApiController
    {
        MongoDatabase mongoDatabase;

        private MongoDatabase RetreiveMongohqDb()
        {
            MongoUrl myMongoURL = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoHQ"].ConnectionString);
            //MongoUrl myMongoURL = new MongoUrl(ConfigurationManager.ConnectionStrings["KurtsMongoHQ"].ConnectionString);
            MongoClient mongoClient = new MongoClient(myMongoURL);
            MongoServer server = mongoClient.GetServer();
            //return mongoClient.GetServer().GetDatabase("kurtmd");
            return mongoClient.GetServer().GetDatabase("reddevteam");
        }

        public IEnumerable<Note> GetAllNotes()
        {
            mongoDatabase = RetreiveMongohqDb();
            List<Note> noteList = new List<Note>();
            try {
                var mongoList = mongoDatabase.GetCollection("Notes").FindAll().AsEnumerable();
                noteList = (from note in mongoList
                            select new Note                    
                    {
                        Id = note["_id"].AsObjectId,                        
                        Subject = note["Subject"].AsString,                        
                        Details = note["Details"].AsString,                        
                        Priority = note["Priority"].AsInt32                    
                    }).ToList();
            }
            catch (Exception ex) {
                throw new ApplicationException("failed to get data from Mongo");
            }
            //noteList.Sort();
            return noteList;

            //return notes;
        }

        public IHttpActionResult GetNote(string id)
        {
            mongoDatabase = RetreiveMongohqDb();

            List<Note> noteList = new List<Note>();
            try
            {
                var mongoList = mongoDatabase.GetCollection("Notes").FindAll().AsEnumerable();
                noteList = (from nextNote in mongoList
                            select new Note
                            {
                                Id = nextNote["_id"].AsObjectId,
                                Subject = nextNote["Subject"].AsString,
                                Details = nextNote["Details"].AsString,
                                Priority = nextNote["Priority"].AsInt32,
                            }).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            var note = noteList.FirstOrDefault((p) => p.Subject == id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            bool found = true;
            string subject = id;
            try
            {
                mongoDatabase = RetreiveMongohqDb();
                var mongoCollection = mongoDatabase.GetCollection("Notes");
                var query = Query.EQ("Subject", subject);
                WriteConcernResult results = mongoCollection.Remove(query);

                if(results.DocumentsAffected < 1)
                {
                    found = false;
                }
            }
            catch(Exception ex)
            {
                found = false;
            }
            HttpResponseMessage response = new HttpResponseMessage();
            if(!found)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }
    }
}

