using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiosk.Model;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using System.Net.Http;
using Newtonsoft.Json;

using System.Net;
using System.Net.Http.Headers;


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


        public static async Task<Tuple<List<Book>, string>> Search(string searchString, string searchByType)
        {
            HttpClient client = new HttpClient();
            string errorMessage = "";
            int maxResults = 40;
            var queryUrl = bookService.BaseUri +
                "volumes?q=" +
                searchByType + ":" +
                searchString +
                "&maxResults=" +
                maxResults.ToString();
            try
            {
                HttpResponseMessage response = await client.GetAsync(queryUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var pReturn = JsonConvert.DeserializeObject<ResVal>(result);
                    if (pReturn == null)
                    {
                        errorMessage = "There is problem getting data. Please check your connection.";
                    }
                    else
                    {
                        var books = pReturn.items.Select(b => new Book
                        {
                            Id = b.Id,
                            Title = b.VolumeInfo.Title,
                            Author = ((b.VolumeInfo.Authors != null) && (b.VolumeInfo.Authors.ToArray().Length > 0)) ? string.Join(",", b.VolumeInfo.Authors.ToArray()) : "",
                            Thumbnail = ((b.VolumeInfo.ImageLinks != null) && (b.VolumeInfo.ImageLinks.SmallThumbnail != null)) ? b.VolumeInfo.ImageLinks.SmallThumbnail : "",
                            Subtitle = b.VolumeInfo.Subtitle ?? " ",
                            PublishedYear = b.VolumeInfo.PublishedDate ?? "Not Available",
                            Publisher = b.VolumeInfo.Publisher ?? "Not Available",
                            Description = b.VolumeInfo.Description ?? "Not Available",
                            PageCount = b.VolumeInfo.PageCount,
                        }).ToList();
                        errorMessage = "";
                        return Tuple.Create(books, errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "There is problem getting data. Please check your connection.";
                var msg = ex.Message;

            }

            return Tuple.Create( new List<Book>(), errorMessage) ;

        }


        //public static async Task<List<Book>> Search(string searchString, string searchByType)
        //{
        //    HttpClient client = new HttpClient();
        //    int maxResults = 40;
        //    var queryUrl = bookService.BaseUri +
        //        "volumes?q=" + 
        //        searchByType + ":" + 
        //        searchString + 
        //        "&maxResults=" + 
        //        maxResults.ToString(); 
        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync(queryUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            var pReturn = JsonConvert.DeserializeObject<ResVal>(result);
        //            if (pReturn == null)
        //            {

        //            }
        //            else
        //            {
        //                var books = pReturn.items.Select(b => new Book
        //                {
        //                    Id = b.Id,
        //                    Title = b.VolumeInfo.Title,
        //                    Author = ((b.VolumeInfo.Authors != null) && (b.VolumeInfo.Authors.ToArray().Length > 0)) ? string.Join(",", b.VolumeInfo.Authors.ToArray()) : "",
        //                    Thumbnail = ((b.VolumeInfo.ImageLinks != null) && (b.VolumeInfo.ImageLinks.SmallThumbnail != null)) ? b.VolumeInfo.ImageLinks.SmallThumbnail : "",
        //                    Subtitle = b.VolumeInfo.Subtitle ?? " ",
        //                    PublishedYear = b.VolumeInfo.PublishedDate ?? "Not Available",
        //                    Publisher = b.VolumeInfo.Publisher ?? "Not Available",
        //                    Description = b.VolumeInfo.Description ?? "Not Available",
        //                    PageCount = b.VolumeInfo.PageCount,
        //                }).ToList();
        //                return books;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
        //    }
            
        //    return new List<Book>();

        //}


        //public static async Task<Volume> SearchBooks(string searchString, int offset, int count)
        //{
        //    var listquery = bookService.Volumes.List(searchString);
        //    listquery.MaxResults = count;
        //    listquery.StartIndex = offset;
        //    Console.WriteLine("Executing a book search request for ISBN: {0} ...", searchString);
        //    var result = await listquery.ExecuteAsync();
        //    if (result != null && result.Items != null)
        //    {
        //        var item = result.Items.FirstOrDefault();
        //        return item;
        //    }
        //    return null;
        //}

        //public static Tuple<int?, List<Book>> Search(string query, int offset, int count)
        //{
        //    var listquery = bookService.Volumes.List(query);
        //    listquery.MaxResults = count;
        //    listquery.StartIndex = offset;
        //    var res = listquery.Execute();
        //    var books = res.Items.Select(b => new Book
        //    {
        //        Id = b.Id,
        //        Title = b.VolumeInfo.Title,
        //        Subtitle = b.VolumeInfo.Subtitle,
        //        Description = b.VolumeInfo.Description,
        //        PageCount = b.VolumeInfo.PageCount,
        //    }).ToList();
        //    return new Tuple<int?, List<Book>>(res.TotalItems, books);
        //}


        //public static List<Book> Search(string query)
        //{
        //    if( string.IsNullOrEmpty(query) || query.Trim() == "")
        //    {
        //        return new List<Book>();
        //    }
        //    // https://www.googleapis.com/books/v1/volumes?q=author:jack
        //    var listquery = bookService.Volumes.List(query);
        //    listquery.MaxResults = 40;
        //    listquery.StartIndex = 0;
        //    var res = listquery.Execute();
        //    try
        //    {
        //        var books = res.Items.Select(b => new Book
        //        {
        //            Id = b.Id,
        //            Title = b.VolumeInfo.Title,
        //            Author = ((b.VolumeInfo.Authors != null) && ( b.VolumeInfo.Authors.ToArray().Length > 0 ) )? string.Join(",", b.VolumeInfo.Authors.ToArray()) : "",
        //            Thumbnail = b.VolumeInfo.ImageLinks.SmallThumbnail,
        //            Subtitle = b.VolumeInfo.Subtitle,
        //            Description = b.VolumeInfo.Description,
        //            PageCount = b.VolumeInfo.PageCount,
        //        }).ToList();
        //        return books;
        //    }
        //    catch(Exception ex)
        //    {
        //        var msg = ex.Message;
        //       return new List<Book>();
        //    }            
        //}


    }

}

