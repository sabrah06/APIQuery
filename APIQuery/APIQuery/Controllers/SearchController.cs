using APIQuery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using APIQuery.Repositories;

namespace APIQuery.Controllers
{
    public class SearchController : Controller
    {
        // GET: Api
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetRecipe(string searchparam)
        {
            string apiResponse = string.Empty;
            RecipeInfo recipe = new RecipeInfo();
            string apiResource = ConfigurationManager.AppSettings["ApiResourceSearch"];
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];
            if (string.IsNullOrEmpty(searchparam))
                searchparam = "shredded%20chicken";
            string searchParam = $"{apiResource}?key={apiKey}&q={searchparam}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(searchParam);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    apiResponse = reader.ReadToEnd();
                    recipe = JsonConvert.DeserializeObject<RecipeInfo>(apiResponse);
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
            return new JsonResult { Data = recipe, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult GetRecipeIngredients(string recipe_id)
        {
            string apiResponse = string.Empty;
            RecipeInfo recipe = new RecipeInfo();
            string apiResource = ConfigurationManager.AppSettings["ApiResourceGet"];
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];
            if (string.IsNullOrEmpty(recipe_id))
                recipe_id = "12142";
            string searchParam = $"{apiResource}?key={apiKey}&rId={recipe_id}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(searchParam);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    apiResponse = reader.ReadToEnd();
                    recipe = JsonConvert.DeserializeObject<RecipeInfo>(apiResponse);
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
            return new JsonResult { Data = recipe, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetRecipebyCuisineDiet(string cusine, string diet)
        {
            string apiResponse = string.Empty;
            SpoonRecipe recipe = new SpoonRecipe();
            string apiResource = "https://spoonacular-recipe-food-nutrition-v1.p.mashape.com/recipes/search";
            string MashappKey = ConfigurationManager.AppSettings["MashappKey"];
            //string searchParam = $"{apiResource}?cuisine=chinese&diet=beef%2C+rice%2C+soy+sauce%2C+broccoli&limitLicense=false";
            cusine = HttpUtility.UrlEncode(cusine);
            diet = HttpUtility.UrlEncode(diet);
            string searchParam = $"{apiResource}?cuisine={cusine}&diet={diet}&limitLicense=false";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(searchParam);
            request.Headers["X-Mashape-Key"] = MashappKey;
            request.Accept = "application/json";
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    apiResponse = reader.ReadToEnd();
                    recipe = JsonConvert.DeserializeObject<SpoonRecipe>(apiResponse);
                    recipe.Cuisine = cusine;
                    recipe.Diet = diet;
                    RecipeRepo recipeDB = new RecipeRepo();
                    searchParam = $"cuisine={cusine}&diet={diet}";
                    bool Saved = recipeDB.SaveRecipesForUserPreference(recipe, searchParam);
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
            return new JsonResult { Data = recipe, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
     
        [HttpGet]
        public ActionResult Search()
        {
            IList<CheckBoxItem> CuisineType = new List<CheckBoxItem>();
            CuisineType.Add(new CheckBoxItem("Chinese"));
            CuisineType.Add(new CheckBoxItem("Indian"));
            CuisineType.Add(new CheckBoxItem("Mexican"));
            CuisineType.Add(new CheckBoxItem("Oriental"));
            CuisineType.Add(new CheckBoxItem("Spanish"));
            CuisineType.Add(new CheckBoxItem("Italian"));
            CuisineType.Add(new CheckBoxItem("Mediteranean"));
            CuisineType.Add(new CheckBoxItem("American"));
            CuisineType.Add(new CheckBoxItem("Thai"));
            ViewBag.CuisineType = new SelectList(CuisineType, "SelectedItemValue", "SelectedItemText");
            return View();
        }
    }
}