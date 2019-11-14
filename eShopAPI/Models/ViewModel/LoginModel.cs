using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eShopAPI.Models.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, ErrorMessage = "Maximum 25 characters allowed")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, ErrorMessage = "Maximum 20 characters allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
