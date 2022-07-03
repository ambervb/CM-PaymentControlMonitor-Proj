using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMPaymentControlMonitor.WebInterface.ViewModels
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Please fill in your username")]
        public string Username { get; set; }

        [Required (ErrorMessage = "Please fill in your password")]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
