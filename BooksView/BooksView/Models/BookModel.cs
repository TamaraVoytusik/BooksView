using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksView.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
    }
}