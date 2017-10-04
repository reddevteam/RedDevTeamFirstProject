using RedDevTeamNames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedDevTeamNames.Controllers
{
    public class NotesController : ApiController
    {
        //Note[] notes = new Note[]
        //{
        //    new Note { Id = 1, Priority = 3, Subject = "Wake up", Details = "Set alarm of 7:00 am and get out of bed."},
        //    new Note { Id = 2, Priority = 2, Subject = "Eat breakfast", Details = "Eat a healthy breakfast."},
        //    new Note { Id = 3, Priority = 5, Subject = "Go to work", Details = "Get to work before 9:00 am."}

        //};

        public IEnumerable<Note> GetAllNotes()
        {
            mongoDatabase = RetreiveMongohqDb();
            List<Note> noteList = newList<Note>();
            try { var mongoList = mongoDatabase.GetCollection("Notes").FindAll().AsEnumerable();
                noteList = (from note in mongoListselectnewNote                    
                            { Id = note["_id"].AsString,                        
                    Subject = note["Subject"].AsString,                        
                    Details = note["Details"].AsString,                        
                    Priority = note["Priority"].AsInt32                    
                        }).ToList(); }

            catch (Exception) {
                thrownewApplicationException("failed to get data from Mongo");
            }
            noteList.Sort();
            return noteList;

            //return notes;
        }

        public IHttpActionResult GetNote(int id)
        {
            var note = notes.FirstOrDefault((p) => p.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }
    }
}
