using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiosk.Model;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;


namespace Kiosk.Api
{
    public class BooksApi
    {
        private static readonly string API_KEY = "AIzaSyCjvWhUyRzuH_9cUXw6qZ7Ys0e4nOGAB1o";
        private static readonly string APP_NAME = "Google Books API";

        private static readonly BooksService bookService = new BooksService(new BaseClientService.Initializer
        {
            ApiKey = API_KEY,
            ApplicationName = APP_NAME
        });

        public static async Task<Volume> SearchBooks(string searchString)
        {
            Console.WriteLine("Executing a book search request for ISBN: {0} ...", searchString);
            var result = await bookService.Volumes.List(searchString).ExecuteAsync();

            if (result != null && result.Items != null)
            {
                var item = result.Items.FirstOrDefault();
                return item;
            }
            return null;

        }

        public static async Task<Volume> SearchBooks(string searchString, int offset, int count)
        {
            var listquery = bookService.Volumes.List(searchString);
            listquery.MaxResults = count;
            listquery.StartIndex = offset;
            Console.WriteLine("Executing a book search request for ISBN: {0} ...", searchString);
            var result = await listquery.ExecuteAsync();

            if (result != null && result.Items != null)
            {
                var item = result.Items.FirstOrDefault();
                return item;
            }
            return null;

        }



        public static Tuple<int?, List<Book>> Search(string query, int offset, int count)
        {
            var listquery = bookService.Volumes.List(query);
            listquery.MaxResults = count;
            listquery.StartIndex = offset;
            var res = listquery.Execute();
            var books = res.Items.Select(b => new Book
            {
                Id = b.Id,
                Title = b.VolumeInfo.Title,
                Subtitle = b.VolumeInfo.Subtitle,
                Description = b.VolumeInfo.Description,
                PageCount = b.VolumeInfo.PageCount,
            }).ToList();
            return new Tuple<int?, List<Book>>(res.TotalItems, books);
        }
    }

}

