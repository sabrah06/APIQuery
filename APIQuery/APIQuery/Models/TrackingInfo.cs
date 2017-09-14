using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class TrackingInfo
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string SessionId { get; set; }
        public string BrowserAgent { get; set; }
        public string CuisineType { get; set; }
        public string LoggedInfo { get; set; }
        public int TrakingId { get; set; }
    }
}