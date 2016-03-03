using BooksDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksView.Models
{
    public class BooksViewModel
    {
        public List<BookModel> ListOfBooks { get; set; }
        public BooksViewModel()
        {
            ListOfBooks = new List<BookModel>();
        }
        public static BookModel ConvertEntityToModel(Book book)
        {
            BookModel bookModel = new BookModel() { Id = book.Id, Title = book.Title, ISBN10 = book.ISBN10, ISBN13 = book.ISBN13, ImageUrl = book.ImageUrl };
            return bookModel;
        }
        public void FillInModelWithBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                ListOfBooks.Add(ConvertEntityToModel(book));
            }

        }
    }
}