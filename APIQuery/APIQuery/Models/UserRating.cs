using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class UserRating
    {
        public int UserRatingId {get; set;}
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public int Rating { get; set; }
        public bool LikeDislike { get; set; }
        public string PredictedRating { get; set; }
        public string PredictedRatingValue { get; set; }
    }
}