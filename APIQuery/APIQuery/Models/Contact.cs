using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Requred")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Requred")]
        public string ContactInfo { get; set; }
    }
}