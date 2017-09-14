using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APIQuery.Controllers;
using System.Web.Mvc;
using APIQuery.Models;

namespace APIUnitTest
{
    /// <summary>
    /// Summary description for RegisterUnitTest
    /// </summary>
    [TestClass]
    public class RegisterUnitTest
    {
        public RegisterUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void LoginUnitTest()
        {
            Login lv = new Login();
            lv.Username = "Abraham";
            lv.Password = "she123";
            lv.VerfiedPin = "she123";
            var controller = new RegisterController();
            ActionResult result = controller.Login(lv);
            //Login ValidLogin = (Login)result.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "index");
            Assert.AreEqual(routeResult.RouteValues["controller"], "browse");
            //Assert.IsTrue(ValidLogin.RatedRecipes.Count > 1);
        }
    }
}
