using Google.Apis.Books.v1.Data;
using System.Collections.Generic;

namespace Kiosk.Models
{
    public class BookVolumeInfo
    {
        public BookVolumeInfo()
        {
            items = new List<Volume>();
        }
        public string kind { get; set; }
        public string totalItems { get; set; }
        public List<Volume> items { get; set; }
    }
}
