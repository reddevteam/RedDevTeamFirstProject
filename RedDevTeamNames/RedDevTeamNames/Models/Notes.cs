using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;

namespace RedDevTeamNames.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public int Priority { get; set; }
    }

}

}