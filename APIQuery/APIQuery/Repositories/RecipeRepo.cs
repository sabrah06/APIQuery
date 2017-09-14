using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIQuery.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace APIQuery.Repositories
{
    public class RecipeRepo
    {
        public bool SaveRecipesForUserPreference(SpoonRecipe userRecipes, string SerachParam)
        {
            //insert into db
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.AddRecipe",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.Add("@RecipeId", SqlDbType.Int);
                idlCommand.Parameters["@Recipeid"].Direction = ParameterDirection.Output;
                idlCommand.Parameters.AddWithValue("@ExternalRecipeId", 0);
                idlCommand.Parameters.AddWithValue("@Title", "");
                idlCommand.Parameters.AddWithValue("@Cusine", "");
                idlCommand.Parameters.AddWithValue("@SearchParams", "");
                idlCommand.Parameters.AddWithValue("@ImageLink", "");
                idlCommand.Parameters.AddWithValue("@ExternallLink", "");
                idlCommand.Parameters.AddWithValue("@ReadyinMinutes", "");
                foreach (SpoonRecipeinfo rcp in userRecipes.Results)
                {                   
                    idlCommand.Parameters["@ExternalRecipeId"].Value= rcp.Id;
                    idlCommand.Parameters["@Title"].Value = rcp.title;
                    idlCommand.Parameters["@Cusine"].Value = userRecipes.Cuisine;
                    idlCommand.Parameters["@SearchParams"].Value = SerachParam;
                    idlCommand.Parameters["@ImageLink"].Value = rcp.image;
                    idlCommand.Parameters["@ExternallLink"].Value = userRecipes.baseUri;
                    idlCommand.Parameters["@ReadyinMinutes"].Value = rcp.readyInMinutes;
                    idlCommand.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool SaveRating(int recipeId, int userId, int rating, bool likeDislike)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.updateRating",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@UserId", userId);
                idlCommand.Parameters.AddWithValue("@RecipeId", recipeId);
                idlCommand.Parameters.AddWithValue("@Rating", rating);
                idlCommand.Parameters.AddWithValue("@LikeDislike", likeDislike);
                idlCommand.ExecuteNonQuery();
            }
            return true;
        }

        public bool SaveContactInfo(string fullname, string contactInfo)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.SaveContactInfo",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@Fullname", fullname);
                idlCommand.Parameters.AddWithValue("@ContactInfo", contactInfo);
                idlCommand.ExecuteNonQuery();
            }
            return true;
        }
        public bool SaveFeedback(int UserId, int questionId, bool yesNo, string response, string feedback)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.SaveFeedBack",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@UserId", UserId);
                idlCommand.Parameters.AddWithValue("@QuestionId", questionId);
                idlCommand.Parameters.AddWithValue("@YesNo", yesNo);
                idlCommand.Parameters.AddWithValue("@Response", response);
                idlCommand.Parameters.AddWithValue("@Feedback", feedback);
                 idlCommand.ExecuteNonQuery();
            }
            return true;
        }

        public UserRating GetUserRating(int userId, int recipeId)
        {
            UserRating urate = new UserRating();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetUserRatingReview",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@userId", userId);
                idlCommand.Parameters.AddWithValue("@recipeId", recipeId);
                SqlDataReader idlReader = idlCommand.ExecuteReader();
                if (idlReader.Read())
                {
                    urate = new UserRating
                    {
                        UserRatingId = Convert.ToInt32(idlReader["UserRatingId"].ToString()),
                        RecipeId = Convert.ToInt32(idlReader["RecipeId"].ToString()),
                        UserId = Convert.ToInt32(idlReader["UserId"].ToString()),
                        Rating = Convert.ToInt32(idlReader["Rating"].ToString()),
                        LikeDislike = Convert.ToBoolean(idlReader["LikeDislike"].ToString()),
                        PredictedRating = idlReader["PredictedRating"].ToString(),
                        PredictedRatingValue = idlReader["PedictedRatingValue"].ToString(),
                    };
                }
            }
            return urate;
        }
                    
        public int SaveTracking(TrackingInfo trackInfo)
        {
            int TrackingId = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.UpdateTracking",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.Add("@TrackingId", SqlDbType.Int);
                idlCommand.Parameters["@TrackingId"].Direction = ParameterDirection.Output;
                idlCommand.Parameters.AddWithValue("@UserId", trackInfo.UserId);
                idlCommand.Parameters.AddWithValue("@RecipeId", trackInfo.RecipeId);
                idlCommand.Parameters.AddWithValue("@SessionId", trackInfo.SessionId);
                idlCommand.Parameters.AddWithValue("@CuisineType", trackInfo.CuisineType);
                idlCommand.Parameters.AddWithValue("@browserAgent", trackInfo.BrowserAgent); 
                idlCommand.Parameters.AddWithValue("@LoggedInfo", trackInfo.LoggedInfo);
                idlCommand.ExecuteNonQuery();
                TrackingId = Convert.ToInt32(idlCommand.Parameters["@TrackingId"].Value);
            }
            return TrackingId;
        }
        public bool SaveUserRegistration(Register reg)
        {
            //insert into db
            //nureg.AppUserId = 2  ;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.SaveUserRegistration",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.Add("@UserId", SqlDbType.Int);
                idlCommand.Parameters["@UserId"].Direction = ParameterDirection.Output;
                idlCommand.Parameters.AddWithValue("@FullName", reg.FullName);
                idlCommand.Parameters.AddWithValue("@UserName", reg.UserName);
                idlCommand.Parameters.AddWithValue("@Pass", reg.Pass);
                idlCommand.Parameters.AddWithValue("@E_mail", reg.E_mail);
                idlCommand.ExecuteNonQuery();
            }
            return true;
        }

        public Login ValidateLogin(Login lv)
        {
            string fullName = string.Empty;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.ValidateLogin",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@Username", lv.Username);
                idlCommand.Parameters.AddWithValue("@pass", lv.Password);
                idlCommand.Parameters.AddWithValue("@Verifiedpin", lv.VerfiedPin);

                SqlDataReader idlReader = idlCommand.ExecuteReader();
                if (idlReader.Read())
                {
                    lv.FullName = idlReader["FullName"].ToString();
                    lv.UserId = Convert.ToInt32(idlReader["UserId"].ToString()==string.Empty ? "0": idlReader["UserId"].ToString());
                }
            }
            return lv;
        }

        public ErrorMetrics GetErrorMetrics()
        {
            ErrorMetrics em = new ErrorMetrics();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetErrorMetrics",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();

                SqlDataReader idlReader = idlCommand.ExecuteReader();
                if (idlReader.Read())
                {
                    em.MeanAbsoluteError = Convert.ToDecimal(idlReader["MAE"].ToString());
                    em.MeanSquareError = Convert.ToDecimal(idlReader["MSE"].ToString());
                    em.RootMeanSquareError = Convert.ToDecimal(idlReader["RMSE"].ToString());
                }
            }
            return em;
        }

        public void GetUserPreference(int userId, ref NewReg OptValue)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetUserOptions",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader idlReader = idlCommand.ExecuteReader();
                while (idlReader.Read())
                {
                    switch (idlReader["ChosenOption"].ToString())
                    {
                        case "CookingEffort":
                            OptValue.CookingEffort = idlReader["OptionValue"].ToString();
                            break;
                        case "CusineType":
                            OptValue.CuisineType.Where(c => c.SelectedItemValue == idlReader["OptionValue"].ToString()).FirstOrDefault().IsSelected = true;
                            break;
                        case "HealthyOptions":
                            OptValue.HealthyOptions.Where(c => c.SelectedItemValue == idlReader["OptionValue"].ToString()).FirstOrDefault().IsSelected = true;
                            break;
                        case "MealType":
                            OptValue.MealType.Where(c => c.SelectedItemValue == idlReader["OptionValue"].ToString()).FirstOrDefault().IsSelected = true;
                            break;
                        case "MeatOptions":
                            OptValue.MeatOptions.Where(c => c.SelectedItemValue == idlReader["OptionValue"].ToString()).FirstOrDefault().IsSelected = true;
                            break;
                        case "SeafoodOptions":
                            OptValue.SeafoodOptions.Where(c =>c.SelectedItemValue == idlReader["OptionValue"].ToString()).FirstOrDefault().IsSelected = true;
                            break;
                        case "Veggie":
                            OptValue.VeggieOptions.Where(c => c.SelectedItemValue == idlReader["OptionValue"].ToString()).FirstOrDefault().IsSelected = true;
                            break;
                    }

                }
            }
        }

        public IList<RecipeRecommendaton> ViewRecommendation(int UserId)
        {
            string fullName = string.Empty;
            IList<RecipeRecommendaton> ListOfRecipes = new List<RecipeRecommendaton>();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetRecipeRecommendation",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@UserId", UserId);

                SqlDataReader idlReader = idlCommand.ExecuteReader();
                while (idlReader.Read())
                {
                    ListOfRecipes.Add(new RecipeRecommendaton
                    {
                        RecipeId = Convert.ToInt32(idlReader["RecipeId"].ToString()),
                        External_Recipe_Id = idlReader["External_Recipe_Id"].ToString(),
                        Title = idlReader["title"].ToString(),
                        CookingEffort= idlReader["CookingEffort"].ToString(),
                        CuisineMealType = idlReader["CuisineMealType"].ToString(),
                        PredictedRating = idlReader["PredictedRating"].ToString(),
                        PredictedRatingValue = idlReader["PredictedRatingValue"].ToString(),
                    });
                }
            }
            return ListOfRecipes;
        }

        public IList<RecipeRecommendaton> ViewRecipeForEvaluation(int UserId)
        {
            string fullName = string.Empty;
            IList<RecipeRecommendaton> ListOfRecipes = new List<RecipeRecommendaton>();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetRecipeRecoForEvaluation",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@UserId", UserId);

                SqlDataReader idlReader = idlCommand.ExecuteReader();
                while (idlReader.Read())
                {
                    ListOfRecipes.Add(new RecipeRecommendaton
                    {
                        RecipeId = Convert.ToInt32(idlReader["RecipeId"].ToString()),
                        External_Recipe_Id = idlReader["External_Recipe_Id"].ToString(),
                        Title = idlReader["title"].ToString(),
                        Image_link = idlReader["Imagelink"].ToString(),
                        CookingEffort = idlReader["CookingEffort"].ToString(),
                        CuisineMealType = idlReader["CuisineMealType"].ToString(),
                        PredictedRating = idlReader["PredictedRating"].ToString(),
                        PredictedRatingValue = idlReader["PredictedRatingValue"].ToString(),
                    });
                }
            }
            return ListOfRecipes;
        }

        public IList<MenuNav> GetCuisineNav()
        {
            string fullName = string.Empty;
            IList<MenuNav> MenuItems = new List<MenuNav>();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetCuisineNav",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();

                SqlDataReader idlReader = idlCommand.ExecuteReader();
                while (idlReader.Read())
                {
                    MenuItems.Add(new MenuNav
                    {
                        Cuisine = idlReader["Cuisine"].ToString(),
                        Section = idlReader["Section"].ToString(),
                    });
                }
            }
            return MenuItems;
        }

        public string GetUserInfo(int UserId)
        {
            string fullName = string.Empty;
            RecipeDetail RecipeDetail = new RecipeDetail();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetUserInfo",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@userId", UserId);
                SqlDataReader idlReader = idlCommand.ExecuteReader();
                if (idlReader.Read())
                {
                    fullName = idlReader["FullName"].ToString();
                }
            }
            return fullName;
        }

        public IList<Questions> GetQuestions(string questionId)
        {
            IList<Questions> questions = new List<Questions>();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetQuestions",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                SqlDataReader idlReader = idlCommand.ExecuteReader();
                while (idlReader.Read())
                {
                    questions.Add(new Questions
                    {
                        QuestionId = Convert.ToInt32(idlReader["QuestionId"].ToString()),
                        Question = idlReader["Question"].ToString(),
                        QuestionOrder = Convert.ToInt16(idlReader["QuestionOrder"].ToString()),
                        QuestionType = idlReader["QuestionType"].ToString(),
                        OptionsValue1 = idlReader["OptionValue1"].ToString(),
                        OptionsValue2 = idlReader["OptionValue2"].ToString(),
                        OptionsValue3 = idlReader["OptionValue3"].ToString(),
                        OptionsValue4 = idlReader["OptionValue4"].ToString(),
                        OptionsValue5 = idlReader["OptionValue5"].ToString(),
                    });
                }
            }
            return questions;
        }

        public RecipeDetail GetRecipeDetail(string Ext_recipeId, int UserId)
        {
            string fullName = string.Empty;
            RecipeDetail RecipeDetail = new RecipeDetail();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetRecipeDetail",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@external_recipeId", Ext_recipeId);
                idlCommand.Parameters.AddWithValue("@userId", UserId);
                SqlDataReader idlReader = idlCommand.ExecuteReader();
                if (idlReader.Read())
                {
                    RecipeDetail = new RecipeDetail
                    {
                        RecipeId = Convert.ToInt32(idlReader["RecipeId"].ToString()),
                        External_Recipe_Id = idlReader["External_Recipe_Id"].ToString(),
                        Title = idlReader["title"].ToString(),
                        CookingEffort = idlReader["CookingEffort"].ToString(),
                        Cuisine = idlReader["CuisineMealType"].ToString(),
                        baseUri = idlReader["ExternalLink"].ToString(),
                        Rating = idlReader["Rating"].ToString(),
                        ImageLink = idlReader["ImageLink"].ToString(),
                        Vegan = idlReader["Vegan"].ToString() == "1" ? true : false,
                        Vegetarian = idlReader["Vegetarian"].ToString() == "1" ? true: false,
                        VeryHeathy = idlReader["Veryhealthy"].ToString() == "1" ? true : false,
                        VeryPopular = idlReader["VeryPopular"].ToString() == "1" ? true : false,
                        GlutenFree = idlReader["GlutenFree"].ToString() == "1" ? true : false,
                        DiaryFree = idlReader["DairyFree"].ToString() == "1" ? true : false,
                        CookingInstr1 = idlReader["CookingInstruction1"].ToString(),
                        CookingInstr2 = idlReader["CookingInstruction2"].ToString(),
                        CookingInstr3 = idlReader["CookingInstruction3"].ToString(),
                        CookingInstr4 = idlReader["CookingInstruction4"].ToString(),
                        CookingInstr5 = idlReader["CookingInstruction5"].ToString(),
                    };
                }
            }
            return RecipeDetail;
        }

        public IList<SpoonRecipeInfo> GetRecipeInfo(string cuisine, string diet)
        {
            string fullName = string.Empty;
            IList<SpoonRecipeInfo> ListOfRecipes = new List<SpoonRecipeInfo>();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetRecipeInfo",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@cuisine", cuisine);
                idlCommand.Parameters.AddWithValue("@diet", diet);
                SqlDataReader idlReader = idlCommand.ExecuteReader();
                while (idlReader.Read())
                {
                    ListOfRecipes.Add(new SpoonRecipeInfo
                    {
                        RecipeId = Convert.ToInt32(idlReader["RecipeId"].ToString()),
                        External_Recipe_Id = idlReader["External_Recipe_Id"].ToString(),
                        Title = idlReader["title"].ToString(),
                        CookingEffort = idlReader["CookingEffort"].ToString(),
                        Cuisine = idlReader["CuisineMealType"].ToString(),
                        baseUri = idlReader["ExternalLink"].ToString(),
                        Rating = idlReader["Rating"].ToString(),
                        ImageLink = idlReader["ImageLink"].ToString()
                    });
                }
            }
            return ListOfRecipes;
        }

        public IList<RecipeRecommendaton> ViewRatedRecipes(int UserId)
        {
            string fullName = string.Empty;
            IList<RecipeRecommendaton> ListOfRecipes = new List<RecipeRecommendaton>();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GetRatedRecipes",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@UserId", UserId);

                SqlDataReader idlReader = idlCommand.ExecuteReader();
                while (idlReader.Read())
                {
                    ListOfRecipes.Add(new RecipeRecommendaton
                    {
                        RecipeId = Convert.ToInt32(idlReader["RecipeId"].ToString()),
                        External_Recipe_Id = idlReader["External_Recipe_Id"].ToString(),
                        Title = idlReader["title"].ToString(),
                        CookingEffort = idlReader["CookingEffort"].ToString(),
                        CuisineMealType = idlReader["CuisineMealType"].ToString(),
                        PredictedRating = idlReader["PredictedRating"].ToString(),
                        PredictedRatingValue = idlReader["PredictedRatingValue"].ToString(),
                        Rating = idlReader["Rating"].ToString()
                    });
                }
            }
            return ListOfRecipes;
        }

        public bool CheckUserPreferenceExists(int UserId)
        {
            bool isExists = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.CheckUserPreference",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.AddWithValue("@UserId", UserId);

                SqlDataReader idlReader = idlCommand.ExecuteReader();
                if (idlReader.Read())
                {
                    isExists = Convert.ToInt32(idlReader["isExists"].ToString()) == 1 ? true : false;
                }
            }
            return isExists;
        }
        public bool SaveUserPreferences(NewReg nureg)
        {
            //insert into db
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString))
            {
                SqlCommand idlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.SaveUserPreference",
                    Connection = sqlConnection,
                    CommandTimeout = 0
                };
                sqlConnection.Open();
                idlCommand.Parameters.Add("@UserPrefId", SqlDbType.Int);
                idlCommand.Parameters["@UserPrefId"].Direction = ParameterDirection.Output;
                idlCommand.Parameters.AddWithValue("@UserId", 0);
                idlCommand.Parameters.AddWithValue("@ChosenOption", 0);
                idlCommand.Parameters.AddWithValue("@OptionValue", "");
                foreach (var ct in nureg.CuisineType.Where(c => c.IsSelected == true).ToList())
                {
                    idlCommand.Parameters["@UserId"].Value = nureg.AppUserId;
                    idlCommand.Parameters["@OptionValue"].Value = ct.SelectedItemValue;
                    idlCommand.Parameters["@ChosenOption"].Value = "CusineType";
                    idlCommand.ExecuteNonQuery();
                }
                foreach (var ct in nureg.MealType.Where(m => m.IsSelected == true).ToList())
                {
                    idlCommand.Parameters["@UserId"].Value = nureg.AppUserId;
                    idlCommand.Parameters["@OptionValue"].Value = ct.SelectedItemValue;
                    idlCommand.Parameters["@ChosenOption"].Value = "MealType";
                    idlCommand.ExecuteNonQuery();
                }
                foreach (var ct in nureg.MeatOptions.Where(m => m.IsSelected == true).ToList())
                {
                    idlCommand.Parameters["@UserId"].Value = nureg.AppUserId;
                    idlCommand.Parameters["@OptionValue"].Value = ct.SelectedItemValue;
                    idlCommand.Parameters["@ChosenOption"].Value = "MeatOptions";
                    idlCommand.ExecuteNonQuery();
                }
                foreach (var ct in nureg.SeafoodOptions.Where(m => m.IsSelected == true).ToList())
                {
                    idlCommand.Parameters["@UserId"].Value = nureg.AppUserId;
                    idlCommand.Parameters["@OptionValue"].Value = ct.SelectedItemValue;
                    idlCommand.Parameters["@ChosenOption"].Value = "SeafoodOptions";
                    idlCommand.ExecuteNonQuery();
                }
                foreach (var ct in nureg.HealthyOptions.Where(h => h.IsSelected == true).ToList())
                {
                    idlCommand.Parameters["@UserId"].Value = nureg.AppUserId;
                    idlCommand.Parameters["@OptionValue"].Value = ct.SelectedItemValue;
                    idlCommand.Parameters["@ChosenOption"].Value = "HealthyOptions";
                    idlCommand.ExecuteNonQuery();
                }
                if (nureg.SelectedVeggieOptions != null)
                {
                    foreach (var ct in nureg.SelectedVeggieOptions)
                    {
                        idlCommand.Parameters["@UserId"].Value = nureg.AppUserId;
                        idlCommand.Parameters["@OptionValue"].Value = ct;
                        idlCommand.Parameters["@ChosenOption"].Value = "Veggie";
                        idlCommand.ExecuteNonQuery();
                    }
                }
                if (!string.IsNullOrEmpty(nureg.CookingEffort))
                {
                    idlCommand.Parameters["@UserId"].Value = nureg.AppUserId;
                    idlCommand.Parameters["@OptionValue"].Value = nureg.CookingEffort;
                    idlCommand.Parameters["@ChosenOption"].Value = "CookingEffort";
                    idlCommand.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}