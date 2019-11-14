using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eShopAPI.Models
{
    [Table("Users")]
    public class User
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required(ErrorMessage = "Username is Required")]
            [StringLength(25, ErrorMessage = "Maximum 25 Characters Allowed")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is Required")]
            [StringLength(20, ErrorMessage = "Maximum 20 Characters allowed")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Fullname is Required")]
            [StringLength(20, ErrorMessage = "Maximum 50 Characters allowed")]
            public string Fullname { get; set; }

            [Required(ErrorMessage = "Email is Required")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required(ErrorMessage = "Mobile number is Required")]
            [StringLength(20, ErrorMessage = "Maximum 10 digits allowed")]
            public string Mobile { get; set; }

    }
}
