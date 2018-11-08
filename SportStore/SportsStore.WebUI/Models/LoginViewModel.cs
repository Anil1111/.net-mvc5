using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please input your user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please input your password")]
        public string Password { get; set; }
    }
}