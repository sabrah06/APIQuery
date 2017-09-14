using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class RecipeInfo
    {
        public int Count { get; set; }
        public string Cuisine { get; set; }
        public IList<Recipe> Recipes { get; set; }
        //"publisher": "Closet Cooking", "f2f_url": "http://food2fork.com/view/35171", "title": "Buffalo Chicken Grilled Cheese Sandwich", "source_url": "http://www.closetcooking.com/2011/08/buffalo-chicken-grilled-cheese-sandwich.html", "recipe_id": "35171", "image_url": "http://static.food2fork.com/Buffalo2BChicken2BGrilled2BCheese2BSandwich2B5002B4983f2702fe4.jpg", "social_rank": 100.0, "publisher_url": "http://closetcooking.com"}
    }
    public class Recipe
    {
        public string publisher { get; set; }
        public string f2f_url { get; set; }
        public string title { get; set; }
        public string source_url { get; set; }
        public string recipe_id { get; set; }
        public string image_url { get; set; }
        public string social_rank { get; set; }
        public string publisher_url { get; set; }
    }
}