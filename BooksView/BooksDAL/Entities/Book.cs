using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
    }
}
