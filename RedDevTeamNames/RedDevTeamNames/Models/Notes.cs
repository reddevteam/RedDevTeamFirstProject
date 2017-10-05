using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedDevTeamNames.Models
{
    public class Note
    {
        [BsonId]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public int Priority { get; set; }
    }
}