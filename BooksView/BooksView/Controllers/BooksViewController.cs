using BooksDAL.Retrieval;
using BooksView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksView.Controllers
{
    public class BooksViewController : Controller
    {
        public ActionResult Index()
        {
            BooksViewModel booksViewModel = new BooksViewModel();
            return View("Main", booksViewModel);
        }
       
        [HttpGet]
        public ActionResult GetBooks(string unparsedISBN)
        {
           BookRetrievalService booksRep = new BookRetrievalService();
           BooksViewModel booksViewModel = new BooksViewModel();
           booksViewModel.FillInModelWithBooks(booksRep.GetBooksByISBN(unparsedISBN));
           return PartialView("BooksView/ShowBooks", booksViewModel);
        }

    }
}
