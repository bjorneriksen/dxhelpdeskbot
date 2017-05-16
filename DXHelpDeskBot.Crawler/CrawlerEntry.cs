using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXHelpDeskBot.Crawler
{
    public class CrawlerEntry
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Service { get; set; }
        public DateTime LastUpdated  { get; set; }
}
}
