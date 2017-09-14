using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIQuery.Models;
using APIQuery.Repositories;
using System.Collections;

namespace APIQuery.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index(int UserId=1)
        {
            NewReg NewRegVM = new NewReg();
            int loggedUserId = GetLoggedCookie();
            //populate the view items
            Populate(ref NewRegVM);
            if (loggedUserId == 0)
                loggedUserId = UserId;
            else
            {
                RecipeRepo recipeDb = new RecipeRepo();
                recipeDb.GetUserPreference(loggedUserId, ref NewRegVM);
            }
            NewRegVM.AppUserId = loggedUserId;
            if (NewRegVM.VeggieOptions.Any(v=> v.IsSelected))
            {
                IList<CheckBoxItem> selList = NewRegVM.VeggieOptions.Where(s => s.IsSelected).ToList();
                ViewBag.VeggieOptions = new MultiSelectList(NewRegVM.VeggieOptions, "SelectedItemValue", "SelectedItemText", selList);
            }
            else
                ViewBag.VeggieOptions = new MultiSelectList(NewRegVM.VeggieOptions, "SelectedItemValue", "SelectedItemText");
            return View(NewRegVM);
        }
        public void Populate(ref NewReg NewRegVM)
        { 
            NewRegVM.MealType = new List<CheckBoxItem>();
            NewRegVM.MealType.Add(new CheckBoxItem("Pasta"));
            NewRegVM.MealType.Add(new CheckBoxItem("Rice"));
            NewRegVM.MealType.Add(new CheckBoxItem("Bread"));
            NewRegVM.MealType.Add(new CheckBoxItem("Salads"));
            NewRegVM.MealType.Add(new CheckBoxItem("Stir Fry"));
            NewRegVM.MealType.Add(new CheckBoxItem("Baked"));
            NewRegVM.MealType.Add(new CheckBoxItem("Grilled"));
            NewRegVM.MealType.Add(new CheckBoxItem("Soups"));
            NewRegVM.MealType.Add(new CheckBoxItem("Desserts"));
            //cusine types
            NewRegVM.CuisineType = new List<CheckBoxItem>();
            NewRegVM.CuisineType.Add(new CheckBoxItem("Chinese"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("Indian"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("Mexican"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("Oriental"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("Spanish"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("Italian"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("Mediteranean"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("American"));
            NewRegVM.CuisineType.Add(new CheckBoxItem("Thai"));
            //Veggie options
            NewRegVM.VeggieOptions = new List<CheckBoxItem>();
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Carrot"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Cabbage"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Beans"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Peas"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Okra"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Celery"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Spinach"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Runner Beans"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Spring Onions"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Tomato"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Lettuce"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Cucumber"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Beetroot"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Corn"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Potato"));
            NewRegVM.VeggieOptions.Add(new CheckBoxItem("Marrow"));
            //Meat options
            NewRegVM.MeatOptions = new List<CheckBoxItem>();
            NewRegVM.MeatOptions.Add(new CheckBoxItem("Beef"));
            NewRegVM.MeatOptions.Add(new CheckBoxItem("Lamb"));
            NewRegVM.MeatOptions.Add(new CheckBoxItem("Chicken"));
            NewRegVM.MeatOptions.Add(new CheckBoxItem("Pork"));
            NewRegVM.MeatOptions.Add(new CheckBoxItem("Egg"));
            //Sea food
            NewRegVM.SeafoodOptions = new List<CheckBoxItem>();
            NewRegVM.SeafoodOptions.Add(new CheckBoxItem("Fish"));
            NewRegVM.SeafoodOptions.Add(new CheckBoxItem("Squid"));
            NewRegVM.SeafoodOptions.Add(new CheckBoxItem("Prawns"));
            NewRegVM.SeafoodOptions.Add(new CheckBoxItem("Clamps"));
            NewRegVM.SeafoodOptions.Add(new CheckBoxItem("Lobster"));
            //Healthy option
            NewRegVM.HealthyOptions = new List<CheckBoxItem>();
            NewRegVM.HealthyOptions.Add(new CheckBoxItem("Diary Free"));
            NewRegVM.HealthyOptions.Add(new CheckBoxItem("Gluten Free"));
            NewRegVM.HealthyOptions.Add(new CheckBoxItem("Low carbs"));
            NewRegVM.HealthyOptions.Add(new CheckBoxItem("Vegan"));
            NewRegVM.HealthyOptions.Add(new CheckBoxItem("Low Sugar"));
            NewRegVM.HealthyOptions.Add(new CheckBoxItem("Low Salt"));
            NewRegVM.HealthyOptions.Add(new CheckBoxItem("Vegetarian"));
        }
        [HttpPost]
        public ActionResult Save(NewReg newreg)
        {
            RecipeRepo recipeDB = new RecipeRepo();
            ViewBag.result = "ok";
            bool Saved = recipeDB.SaveUserPreferences(newreg);
            if (!Saved)
                ViewBag.result = "failed";
            if (newreg.VeggieOptions == null)
            {
                newreg.VeggieOptions = new List<CheckBoxItem>();
                newreg.VeggieOptions.Add(new CheckBoxItem("Carrot"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Cabbage"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Beans"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Peas"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Okra"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Celery"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Spinach"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Runner Beans"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Spring Onions"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Tomato"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Lettuce"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Cucumber"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Beetroot"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Corn"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Potato"));
                newreg.VeggieOptions.Add(new CheckBoxItem("Marrow"));
            }
            ViewBag.VeggieOptions = new MultiSelectList(newreg.VeggieOptions, "SelectedItemValue", "SelectedItemText");
            return View("index", newreg);
        }
        [HttpPost]
        public ActionResult Register(Register reg)
        {
            if (!ModelState.IsValid)
            {
                return View(reg);
            }
            RecipeRepo recipeDB = new RecipeRepo();
            ViewBag.result = "ok";
            bool Saved = recipeDB.SaveUserRegistration(reg);
            if (!Saved)
                ViewBag.result = "failed";
            return View(reg);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            Login lv = new Login();
            return View(lv);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact ctnt)
        {
            if (!ModelState.IsValid)
            {
                return View(ctnt);
            }
            RecipeRepo recipeDb =new RecipeRepo();
            recipeDb.SaveContactInfo(ctnt.FullName, ctnt.ContactInfo);
            ViewBag.Success = true;
            return View(ctnt);
        }
        public ActionResult Logout()
        {
            ClearCookies();
            Session.Abandon();
            return RedirectToAction("login", "register");
        }

        public ActionResult Home()
        {
            Homeuser homeuser = new Homeuser();
            if (HttpContext.Session["UserId"] == null)
                homeuser.UserId = Convert.ToInt32(GetLoggedCookie());
            else
                homeuser.UserId = Convert.ToInt32(HttpContext.Session["UserId"].ToString());
            HttpContext.Session["UserId"] = homeuser.UserId.ToString();
            IList<RecipeRecommendaton> ListofRecipes = new List<RecipeRecommendaton>();
            RecipeRepo recipeDB = new RecipeRepo();
            homeuser.FullName = recipeDB.GetUserInfo(homeuser.UserId);
            ListofRecipes = recipeDB.ViewRecommendation(homeuser.UserId);

            IList<RecipeRecommendaton> RatedRecipes = new List<RecipeRecommendaton>();
            RatedRecipes = recipeDB.ViewRatedRecipes(homeuser.UserId);
            homeuser.ListofRecipes = ListofRecipes;
            homeuser.RatedRecipes = RatedRecipes;
            return View(homeuser);
        }

        [HttpPost]
        public ActionResult Login(Login lv)
        {
            if (!ModelState.IsValid)
            { 
                return View(lv);
            }
            RecipeRepo recipeDB = new RecipeRepo();
            Login validLogin = recipeDB.ValidateLogin(lv);
            if (validLogin.UserId <= 0)
            {
                ViewBag.invalidLogin = true;
                return View(lv);
            }
            //Log in logged in cookie
            if (HttpContext != null)
            {
                SetLoggedCookie(validLogin);
                HttpContext.Session["UserId"] = validLogin.UserId.ToString();
            }
            if (!recipeDB.CheckUserPreferenceExists(validLogin.UserId))
                return RedirectToAction("index", "register");
            else
                return RedirectToAction("index", "browse");

            //validLogin.ListofRecipes = new List<RecipeRecommendaton>();
            //validLogin.ListofRecipes = recipeDB.ViewRecommendation(validLogin.UserId);

            //validLogin.RatedRecipes = new List<RecipeRecommendaton>();
            //validLogin.RatedRecipes = recipeDB.ViewRatedRecipes(validLogin.UserId);
            //recipeDB = null;
            //return View(validLogin);
        }

        public void SetLoggedCookie(Login validLogin)
        {
            HttpCookie logCookie = new HttpCookie("RLog");

            // Set the cookie value.
            logCookie.Value = validLogin.UserId.ToString();
            // Set the cookie expiration date.
            logCookie.Expires = DateTime.Today.AddYears(1);

            // Add the cookie.
            Response.Cookies.Add(logCookie);
        }

        public ActionResult Feedback()
        {
            RecipeRepo recipeDB = new RecipeRepo();
            Survey survey = new Survey();
            survey.ListOfQuestions = recipeDB.GetQuestions("0");
            return View(survey);
        }

        [HttpPost]
        public ActionResult Feedback(Survey survey)
        {
            bool saved = false;
            RecipeRepo recipeDB = new RecipeRepo();
            survey.UserId = GetLoggedCookie();
            survey.ListOfQuestions = recipeDB.GetQuestions("0");
            saved = recipeDB.SaveFeedback(survey.UserId, survey.ListOfQuestions[0].QuestionId, survey.Feedback1 == "yes" ? true : false, string.Empty, string.Empty);
            saved = recipeDB.SaveFeedback(survey.UserId, survey.ListOfQuestions[1].QuestionId, false, survey.Feedback2, string.Empty);
            saved = recipeDB.SaveFeedback(survey.UserId, survey.ListOfQuestions[2].QuestionId, false, survey.Feedback3, string.Empty);
            saved = recipeDB.SaveFeedback(survey.UserId, survey.ListOfQuestions[3].QuestionId, false, survey.Feedback4, string.Empty);
            saved = recipeDB.SaveFeedback(survey.UserId, survey.ListOfQuestions[4].QuestionId, false, string.Empty, survey.Feedback5);
            ViewBag.Saved = true;
            return View(survey);
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

        public void ClearCookies()
        {
            if (Request.Cookies["RLog"] != null)
            {
                HttpCookie userid = Request.Cookies["RLog"];
                userid.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userid);
                HttpContext.Session["UserId"] = "";
            }
        }
    }
}