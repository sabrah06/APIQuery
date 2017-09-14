using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class RecipeDetail
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
        public bool Vegan { get; set; }
        public bool Vegetarian { get; set; }
        public bool DiaryFree { get; set; }
        public bool GlutenFree { get; set; }
        public bool VeryHeathy { get; set; }
        public bool VeryPopular { get; set; }
        public string CookingInstr1 { get; set; }
        public string CookingInstr2 { get; set; }
        public string CookingInstr3 { get; set; }
        public string CookingInstr4 { get; set; }
        public string CookingInstr5 { get; set; }
    }
}