using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class Evaluation
    {
        public int UserId { get; set; }
        public IList<RecipeEvaluation> ListofRecipes { get; set; }
    }
    public class RecipeEvaluation
    {
        public int RecipeId { get; set; }
        public string baseUri { get; set; }
        public string Cuisine { get; set; }
        public string Diet { get; set; }
        public string External_Recipe_Id { get; set; }
        public string Title { get; set; }
        public string CookingEffort { get; set; }
        public string ImageLink { get; set; }
        public string Rating { get; set; }
        public decimal PredictedScore { get; set; }
        public int PredictedRating { get; set; }
        public int ActualRating { get; set; }
        public bool LikeDislike { get; set; }
    }
}