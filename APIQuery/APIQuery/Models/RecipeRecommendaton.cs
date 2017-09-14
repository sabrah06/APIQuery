using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class RecipeRecommendaton
    {
        public int RecipeId { get; set; }
        public string External_Recipe_Id { get; set; }
        public string Image_link { get; set; }
        public string Title { get; set; }
        public string CookingEffort { get; set; }
        public string CuisineMealType { get; set; }
        public string PredictedRating { get; set; }
        public string PredictedRatingValue { get; set; }
        public string Rating { get; set; }
    }
}