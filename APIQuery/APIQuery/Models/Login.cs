using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Requred")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Requred")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Requred")]
        public string VerfiedPin { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }
        public IList<RecipeRecommendaton> ListofRecipes { get; set; }
        public IList<RecipeRecommendaton> RatedRecipes { get; set; }
    }
}