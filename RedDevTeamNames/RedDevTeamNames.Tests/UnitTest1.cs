using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RedDevTeamNames.Models;
using RedDevTeamNames.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net.Http;
using System.Net;

namespace RedDevTeamNames.Tests
{
    [TestClass]
    public class TestNotesController
    {
        List<Note> noteList = new List<Note>();
        private List<Note> GenerateFakeDataList()
        {
            List<Note> workingList = new List<Note>();
            for (int i = 0; i < 3; i++)
            {
                Note nextNote = new Note();
                nextNote.Id = i.ToString();
                nextNote.Subject = "Test" + i.ToString();
                nextNote.Details = "Test" + i.ToString() + " Details";
                nextNote.Priority = i;
                workingList.Add(nextNote);
            }
            return workingList;
        }
        //=======================================================================
        // test first API   GetAllNotes()
        [TestMethod]
        // first test local logic, using fake data
        public void GetAllFakeNotes_ShouldReturnAllNotes()
        {
            List<Note> testNotes = GenerateFakeDataList();
            var controller = new NotesController(testNotes); // use 1 of 2 constructors

            var result = controller.GetAllNotes() as List<Note>;
            Assert.AreEqual(testNotes.Count, result.Count);
        }
        [TestMethod]
        // now test against test data in mongo
        public void GetAllMongoNotes_ShouldReturnAllNotes()
        {
            // need to modify Controller to point to NotesTest
            List<Note> testNotes = GenerateFakeDataList();
            var controller = new NotesController(); // use the other constructor

            var result = controller.GetAllNotes() as List<Note>;
            Assert.AreEqual(testNotes.Count, result.Count);
        }
        //=======================================================================
        // test 2nd API   GetNote(string id)
        [TestMethod]
        // first test local logic, using fake data
        public void GetFakeNote_ShouldReturnParticularNote()
        {
            List<Note> testNotes = GenerateFakeDataList();
            var controller = new NotesController(testNotes); // use 1 of 2 constructors

            IHttpActionResult result = controller.GetNote("Test2");
            var contentResult = result as OkNegotiatedContentResult<Note>;

            Assert.AreEqual(testNotes[2].Subject, contentResult.Content.Subject);

        }
        //=======================================================================
        [TestMethod]
        //test delete return ok
        public void DeleteRetunsOk()
        {
            var controller = new NotesController();

            var testNote = "Test2";
            var response = controller.Delete(testNote);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
        //=======================================================================
        [TestMethod]
        // now test against test data in mongo
        public void GetMongoNote_ShouldReturnParticularNote()
        {
            List<Note> testNotes = GenerateFakeDataList();
            var controller = new NotesController(); // use other constructors

            IHttpActionResult result = controller.GetNote("Test2");
            var contentResult = result as OkNegotiatedContentResult<Note>; ;

            Assert.AreEqual(testNotes[2].Subject, contentResult.Content.Subject);

        }
    }
}
