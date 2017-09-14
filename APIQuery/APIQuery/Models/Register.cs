using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace APIQuery.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Requred")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Requred")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Requred")]
        public string Pass { get; set; }
        [Required(ErrorMessage = "Requred")]
        public string ConfirmPass { get; set; }
        [Required(ErrorMessage = "Requred")]
        public string E_mail { get; set; }
    }
}