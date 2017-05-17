using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DXHelpDeskBot.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class SearchResult
    {
        public string id { get; set; }

        [Key]
        public string rid { get; set; }

        public string URL { get; set; }

        [IsSearchable, IsSortable]
        public string Title { get; set; }

        [IsSearchable]
        public string Description { get; set; }

        [IsSearchable]
        public string Content { get; set; }

        [IsSearchable, IsFilterable, IsSortable]
        public string Service { get; set; }

        [IsFilterable, IsSortable]
        public DateTime? LastUpdated { get; set; }
    }
}