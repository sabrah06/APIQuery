using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APIQuery.Controllers;
using System.Web.Mvc;
using APIQuery.Models;
using System.Collections.Generic;
using System.Linq;

namespace APIUnitTest
{
    [TestClass]
    public class RecipeControllerUnitTest
    {
        [TestMethod]
        public void RecipeBrowseUnitTest()
        {
            var controller = new BrowseController();
            var result = controller.Detail("Skinny-Chicken-Enchiladas-Skinnytaste-720050") as ViewResult;
            RecipeDetail recipeDetail = (RecipeDetail)result.ViewData.Model;
            Assert.AreEqual("720050", recipeDetail.External_Recipe_Id);
        }

        [TestMethod]
        public void LoadRecipesUnitTest()
        {
            var controller = new BrowseController();
            var result = controller.LoadRecipes("Indian", "Chicken") as ViewResult;
            IList<SpoonRecipeInfo> ListofRecipes = (List<SpoonRecipeInfo>)result.ViewData.Model;
            Assert.AreEqual("indian", ListofRecipes[0].Cuisine);
        }
    }
}
