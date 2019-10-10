using Google.Apis.Books.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Model
{
    public class ResVal
    {
        public ResVal()
        {
            items = new List<Volume>();
        }
        public string kind { get; set; }
        public string totalItems { get; set; }
        public List<Volume> items { get; set; }
    }
}
