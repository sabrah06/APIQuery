using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class SpoonRecipe
    {
        public int totalResults { get; set; }
        public string baseUri { get; set; }
        public string Cuisine { get; set; }
        public string Diet { get; set; }
        public IList<SpoonRecipeinfo> Results { get; set; }
    }

    public class SpoonRecipeinfo
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string readyInMinutes { get; set; }
        public string image { get; set; }
        public IList<string> imageUrls { get; set; }
        public int RecipeId { get; internal set; }
    }
}