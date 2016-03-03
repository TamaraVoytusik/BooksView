using BooksDAL.Context;
using BooksDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BooksDAL.Retrieval
{
    public class BookRetrievalService
    {
        private BookContext myDbContext;
        private BookWebRetrieval webRetrieval;
        public BookRetrievalService()
            : this(new BookContext(), new BookWebRetrieval())
        {

        }
        public BookRetrievalService(BookContext context, BookWebRetrieval webRetrieval)
        {
            this.myDbContext = context;
            this.webRetrieval = webRetrieval;
        }
        public IEnumerable<Book> GetBooksByISBN(List<string> isbnList)
        {
            IEnumerable<Book> listOfBooks = myDbContext.Books.Where(book => isbnList.Contains(book.ISBN13) || isbnList.Contains(book.ISBN10)).ToList();
            return listOfBooks;
        }
        public IEnumerable<Book> GetBooksByISBN(string isbnStrings)
        {
            List<string> isbnList = ParseISBNNumbers(isbnStrings);
            IEnumerable<Book> result = GetBooksByISBN(isbnList);
            Dictionary<string, bool> dictionaryOfFoundData = new Dictionary<string, bool>();
            if (isbnList.Count > result.Count())
            {
                foreach (var book in result)
                {
                    if (!dictionaryOfFoundData.ContainsKey(book.ISBN13))
                    {
                        dictionaryOfFoundData.Add(book.ISBN13, true);
                    }
                }
                foreach (var item in isbnList)
                {
                    if (!dictionaryOfFoundData.ContainsKey(item))
                    {
                        dictionaryOfFoundData.Add(item, false);
                    }
                }
            }

            IEnumerable<string> listOfNotFound = dictionaryOfFoundData.Where(val => !val.Value).Select(val => val.Key);
            if (listOfNotFound.Any())
            {
                List<Book> listOfFoundBooks = webRetrieval.GetListOfBooks(listOfNotFound);
                foreach (var book in listOfFoundBooks)
                {
                    myDbContext.Books.Add(book);
                }
                myDbContext.SaveChanges();
                result = result.Concat(listOfFoundBooks);
            }

            return result;
        }

        private List<string> ParseISBNNumbers(string isbnStrings)
        {
            string[] arrayOfISBNS = isbnStrings.Split(',');
            List<string> isbnList = new List<string>(arrayOfISBNS.Length);
            string pattern = "\\D+";
            Regex rgx = new Regex(pattern);
            for (int i = 0; i < arrayOfISBNS.Length; i++)
            {
                string result = rgx.Replace(arrayOfISBNS[i], string.Empty);
                if (!string.IsNullOrEmpty(result))
                {
                    isbnList.Add(result);
                }
            }
            return isbnList;
        }
    }
}
