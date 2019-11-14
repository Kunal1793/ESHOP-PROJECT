using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eShopAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }

        public ICollection<OrderProduct> orderProducts { get; set; }

    }
}
