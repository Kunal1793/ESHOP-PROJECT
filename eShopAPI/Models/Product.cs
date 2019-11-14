using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eShopAPI.Models
{
    [Table("Products")]

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(25, ErrorMessage = "Product Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Price is Required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Product Price is Required")]
        public int Quantity { get; set; }

        public int? ReorderLevel { get; set; }

        public double TaxRate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ManufacturingDate { get; set; }

        [Required(ErrorMessage = "Brand Name is Required")]
        [StringLength(30, ErrorMessage = "Maximum 30 characters allowed")]

        public string Brand { get; set; }

        public string  Imageurl { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<OrderProduct> orderProducts { get; set; }




    }
}
