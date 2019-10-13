using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiosk.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using System.Net.Http;
using Newtonsoft.Json;

namespace Kiosk.ServiceLayer
{
    public class BooksApi
    {
        private static readonly string API_KEY = "Your API key";
        private static readonly string APP_NAME = "Your App Name";

        private static readonly BooksService bookService = new BooksService(new BaseClientService.Initializer
        {
            ApiKey = API_KEY,
            ApplicationName = APP_NAME
        });


        public static async Task<Tuple<bool, List<Book>, string>> Search(string searchString, string searchByType)
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
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var pReturn = JsonConvert.DeserializeObject<BookVolumeInfo>(result);
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
                            PublishedYear = b.VolumeInfo.PublishedDate ?? "Not Available",
                            Publisher = b.VolumeInfo.Publisher ?? "Not Available",
                            Description = b.VolumeInfo.Description ?? "Not Available",
                            ISBN = ((b.VolumeInfo != null) && (b.VolumeInfo.IndustryIdentifiers != null) && (b.VolumeInfo.IndustryIdentifiers.Count > 0)) ? b.VolumeInfo.IndustryIdentifiers.FirstOrDefault().Identifier : "Not Available"
                        }).ToList();
                        errorMessage = ""; 
                        return Tuple.Create(true, books, errorMessage);
                    }
                }
                else
                {
                    errorMessage = "There is problem getting data. Please check your connection.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " Please check your internet connection.";
            }
            return Tuple.Create( false, new List<Book>(), errorMessage);
        }
    }

}

