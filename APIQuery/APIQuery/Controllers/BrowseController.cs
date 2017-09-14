using APIQuery.Models;
using APIQuery.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace APIQuery.Controllers
{
    public class BrowseController : Controller
    {
        // GET: Browse
        public ActionResult Index()
        {
            RecipeRepo recipeDB = new RecipeRepo();
            IList<MenuNav> MenuItems = recipeDB.GetCuisineNav();

            return View(MenuItems);
        }

        public ActionResult LoadRecipes(string cuisine, string diet)
        {
            RecipeRepo recipeDB = new RecipeRepo();
            diet = diet.Replace(" ", "%2c+");
            if (HttpContext != null)
            {
                if (HttpContext.Session["UserId"] == null)
                    ViewBag.UserId = GetLoggedCookie().ToString();
                else
                    ViewBag.UserId = HttpContext.Session["UserId"].ToString();
            }
            else
                ViewBag.UserId = -1;
            IList<SpoonRecipeInfo> ListofRecipes = recipeDB.GetRecipeInfo(cuisine, diet);
            return View(ListofRecipes);
        }

        public ActionResult Detail(string extRecipeId)
        {
            RecipeRepo recipeDB = new RecipeRepo();
            string[] ext_recipeId = Regex.Split(extRecipeId, @"([0-9]+)");
            if (HttpContext != null)
            {
                if (HttpContext.Session["UserId"] == null)
                    ViewBag.UserId = GetLoggedCookie().ToString();
                else
                    ViewBag.UserId = HttpContext.Session["UserId"].ToString();
            }
            else
                ViewBag.UserId = -1;
            if (ext_recipeId.Length == 3)
            {
                RecipeDetail recipeDetail = recipeDB.GetRecipeDetail(ext_recipeId[1], Convert.ToInt32(ViewBag.UserId));       
                return View("detail", recipeDetail);
            }
            else
                return RedirectToAction("Noresults", "browse");
        }

        public ActionResult DetailRating(string RecipeId)
        {
            RecipeRepo recipeDB = new RecipeRepo();
            string[] ext_recipeId = Regex.Split(RecipeId, @"([0-9]+)");
            if (HttpContext != null)
            {
                if (HttpContext.Session["UserId"] == null)
                    ViewBag.UserId = GetLoggedCookie().ToString();
                else
                    ViewBag.UserId = HttpContext.Session["UserId"].ToString();
            }
            else
                ViewBag.UserId = -1;
            if (ext_recipeId.Length == 3)
            {
                RecipeDetail recipeDetail = recipeDB.GetRecipeDetail(ext_recipeId[1], Convert.ToInt32(ViewBag.UserId));
                return View("DetailRating", recipeDetail);
            }
            else
                return RedirectToAction("Noresults", "browse");
        }

        public JsonResult updateTracking(int RecipeId, int userId, string loggedInfo, string cuisineType)
        {
            TrackingInfo trackInfo = new TrackingInfo();
            int userid = GetLoggedCookie();
            trackInfo.RecipeId = RecipeId;
            trackInfo.UserId = userId;
            trackInfo.LoggedInfo = loggedInfo;
            trackInfo.CuisineType = cuisineType;
            trackInfo.SessionId = HttpContext.Session.SessionID;
            trackInfo.BrowserAgent = Request.UserAgent;
            RecipeRepo recipeDb = new RecipeRepo();
            int trackingId = recipeDb.SaveTracking(trackInfo);
            return Json(new { TrackingId = trackingId });
        }

        [HttpPost]
        public JsonResult updateRating(int userId, int RecipeId, int Rating, int likeDislike)
        {
            RecipeRepo recipeDb = new RecipeRepo();
            int userid = GetLoggedCookie();
            bool ld = likeDislike == 1 ? true : false;
            bool saved = recipeDb.SaveRating(RecipeId, userId, Rating, ld);
            recipeDb = null;
            return Json(new { success = saved });
        }

        public ActionResult UserRating (int UserId, int RecipeId)
        {
            UserRating urate = new Models.UserRating();
            RecipeRepo recipeDb = new RecipeRepo();
            urate = recipeDb.GetUserRating(UserId, RecipeId);
            return View(urate);
        }

        public ActionResult UserRatingReview(int UserId, int RecipeId)
        {
            UserRating urate = new Models.UserRating();
            RecipeRepo recipeDb = new RecipeRepo();
            urate = recipeDb.GetUserRating(UserId, RecipeId);
            return View(urate);
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