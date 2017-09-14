using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class NewReg
    {
        public int RegId { get; set; }
        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public IList<CheckBoxItem> CuisineType { get; set; }
        public IList<CheckBoxItem> MealType { get; set; }
        public IList<CheckBoxItem> MeatOptions { get; set; }
        public IList<CheckBoxItem> SeafoodOptions { get; set; }
        public IList<CheckBoxItem> VeggieOptions { get; set; }
        public IList<CheckBoxItem> HealthyOptions { get; set; }
        public string CookingEffort { get; set; }
        public IList<string> SelectedMealType { get; set; }
        public IList<string> SelectMeatOptions { get; set; }
        public IList<string> SelectedSeafoodOptions { get; set; }
        public IList<string> SelectedVeggieOptions { get; set; }
        public IList<string> SelectedHealthyOptions { get; set; }
    }

    public class CheckBoxItem
    {
        public string SelectedItemText { get; set; }
        public string SelectedItemValue { get; set; }
        public bool IsSelected { get; set; }
        public CheckBoxItem()
        {
        }
        public CheckBoxItem(string value, string id=null)
        {
            SelectedItemText = value;
            if (id == null)
                SelectedItemValue = value;
        }
    }
}