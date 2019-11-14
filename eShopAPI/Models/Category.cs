using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eShopAPI.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Category Name is Required")]
        [StringLength(25, ErrorMessage = "Category Name is Required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products {get; set;}
    }
}
