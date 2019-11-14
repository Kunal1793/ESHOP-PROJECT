using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShopAPI.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ShopDbContext db;

        public OrderController(ShopDbContext dbContext)
        {
            this.db = dbContext;
        }


        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost("Orders")]
        public async Task<ActionResult<Product>> AddOrderAsync([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await db.orders.AddAsync(order);
            await db.SaveChangesAsync();
            return Created("", order);
        }

        [HttpGet("Orders")]
        public ActionResult<IEnumerable<Order>> GetCustomerOrders(int userId) 
        {
            List<Order> CustomerOrders = new List<Order>();
            return CustomerOrders = db.orders.Where(o => o.UserID == userId).ToList();
        }

        [HttpGet("{orderId}")]
        public ActionResult<IEnumerable<Product>> GetOrderDetails(int orderId)
        {
            var OrderDetails = db.orders.Find(orderId);
            var Orderproduct = from OP in db.orderProducts
                          where OP.OrderId.Equals(OrderDetails.Id)
                          select OP.ProductId;
            var ProductDetails = from Product in db.products where Product.Id.Equals(Orderproduct) select Product;
            return ProductDetails.ToList();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrderAsync(int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var DeleteOrder = await db.orders.FindAsync(orderId);
            if (DeleteOrder == null)
            {
                return NotFound();
            }

            db.orders.Remove(DeleteOrder);
            await db.SaveChangesAsync();

            return Ok(DeleteOrder);
        }

    }
}