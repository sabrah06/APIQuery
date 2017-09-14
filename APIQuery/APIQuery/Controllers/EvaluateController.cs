using APIQuery.Models;
using APIQuery.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIQuery.Controllers
{
    public class EvaluateController : Controller
    {
        // GET: Evaluate
        public ActionResult Index()
        {
            RecipeRepo recipeDb = new RecipeRepo();
            if (HttpContext != null)
            {
                if (HttpContext.Session["UserId"] == null)
                    ViewBag.UserId = GetLoggedCookie().ToString();
                else
                    ViewBag.UserId = HttpContext.Session["UserId"].ToString();
            }
            else
                ViewBag.UserId = 1;
            IList<RecipeRecommendaton> ListofRecipes = recipeDb.ViewRecipeForEvaluation(Convert.ToInt32(ViewBag.UserId));
            Evaluation eval = new Evaluation();
            eval.UserId = Convert.ToInt32(ViewBag.UserId);
            eval.ListofRecipes = new List<RecipeEvaluation>();

            foreach (RecipeRecommendaton rr in ListofRecipes)
            {
                eval.ListofRecipes.Add(new RecipeEvaluation
                {
                    RecipeId = rr.RecipeId,
                    Title = rr.Title,
                    CookingEffort = rr.Title,
                    Cuisine = rr.CuisineMealType,
                    ImageLink = rr.Image_link,
                    External_Recipe_Id = rr.External_Recipe_Id,
                    PredictedRating = string.IsNullOrEmpty(rr.PredictedRating) ? 0: System.Convert.ToInt32(rr.PredictedRating),
                    PredictedScore = string.IsNullOrEmpty(rr.PredictedRatingValue) ? 0 : Convert.ToDecimal(rr.PredictedRatingValue),
                    Rating = rr.Rating
                });
            }
            return View(ListofRecipes);
        }

        public ActionResult ErrorMetrics()
        {
            RecipeRepo recipeDb = new RecipeRepo();
            ErrorMetrics em = new ErrorMetrics();
            em = recipeDb.GetErrorMetrics();
            return View(em);
        }

        public int GetLoggedCookie()
        {
            if (Request.Cookies["RLog"] != null)
            {
                var userid = Request.Cookies["RLog"].Value;
                HttpContext.Session["UserId"] = userid;
                return Convert.ToInt32(userid);
            }
            return 0;
        }
    }
}