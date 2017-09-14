using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class Homeuser
    {
        public string FullName { get; set; }
        public int UserId { get; set; }
        public IList<RecipeRecommendaton> ListofRecipes { get; set; }
        public IList<RecipeRecommendaton> RatedRecipes { get; set; }
    }
}