using BooksDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BooksDAL.Retrieval
{
   public class BookWebRetrieval
    {
        private const int MAX_ISBN_AMOUNT = 100;

        public List<Book> GetListOfBooks(IEnumerable<string> listOfISBNToRequest)
        {
            List<Book> listOfBooks = new List<Book>();
            IEnumerable<StringBuilder> listOfISBNSParts = FormListOfISBNRequests(listOfISBNToRequest);
            foreach (StringBuilder isbnsStringPart in listOfISBNSParts)
            {
                string bookStringToParse = GetBooksFromWebAPI(isbnsStringPart.ToString());
                listOfBooks.AddRange(ParseBooksFromString(bookStringToParse));
            }
            return listOfBooks;
        }

        private List<Book> ParseBooksFromString(string strToParse)
        {
            List<Book> listOfBooks = new List<Book>();
            dynamic parsedObj = Json.Decode(strToParse);
            for (int i = 0; i < parsedObj.products.Length; i++)
            {
                Book book = new Book();
                book.ImageUrl = parsedObj.products[i].imageurl;
                book.ISBN13 = parsedObj.products[i].isbn13;
                book.ISBN10 = parsedObj.products[i].isbn10;
                book.Title = parsedObj.products[i].title;
                listOfBooks.Add(book);
            }
            return listOfBooks;
        }

        private string GetBooksFromWebAPI(string isbns)
        {
            //   string webRequest = @"http://api.saxo.com/v1/products/products.json?key=08964e27966e4ca99eb0029ac4c4c217&isbn=" + isbns;
            string responseString = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.saxo.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("v1/products/products.json?key=08964e27966e4ca99eb0029ac4c4c217&isbn=" + isbns).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                }
            }
            return responseString;
        }

        private List<StringBuilder> FormListOfISBNRequests(IEnumerable<string> collectionOfISBNToRequest)
        {
            List<StringBuilder> isbnsPart = new List<StringBuilder>();
            int k = 0;
            int nextStep = 0;
            int i = -1;
            foreach (var isbnNumber in collectionOfISBNToRequest)
            {
                if (isbnNumber.Length < 14)
                {
                    if (k == nextStep)
                    {
                        if (i > -1)
                        {
                            isbnsPart[i] = isbnsPart[i].Remove(isbnsPart[i].Length - 1, 1);
                        }
                        isbnsPart.Add(new StringBuilder());
                        i++;
                        nextStep = nextStep + MAX_ISBN_AMOUNT;
                    }
                    isbnsPart[i].AppendFormat("{0},", isbnNumber);
                    k++;
                }
            }
            var lastItem = isbnsPart.Last();
            lastItem = lastItem.Remove(lastItem.Length - 1, 1);
            return isbnsPart;
        }
    }
}
