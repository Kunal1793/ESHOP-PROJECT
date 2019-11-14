using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private ShopDbContext db;

        public CatalogController(ShopDbContext dbContext)
        {
            this.db = dbContext;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)] // Gets Prodcuts
        [HttpGet("products")]
        public ActionResult<IEnumerable<Product>> GetProducts([FromQuery]
        int PageNo = 1, [FromQuery] int PageSize = 20)
        {
            int noOfRecordsToSkip = (PageNo - 1) * PageSize;
            return db.products.Skip(noOfRecordsToSkip).Take(PageSize).ToList();
        }

        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost("products/{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync([FromRoute] int id) 
        {
            var product = await db.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                return Ok(product);
            }
        }


        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost("Categories")]
        public async Task<ActionResult<Product>> AddProductAsync([FromBody] Product product) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await db.products.AddAsync(product);
            await db.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetProductByIdAsync),
                new { id = product.Id },
                product
                );

        }

        [ProducesResponseType((int)HttpStatusCode.OK)] // Gets Categories
        [HttpGet("categories")]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return db.categories.ToList();
        }

        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost("products")]
        public async Task<ActionResult<Category>> AddCategoryAsync([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.categories.AddAsync(category);
            await db.SaveChangesAsync();
            return Created("", category);
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

            //return CreatedAtAction(
            //    nameof(GetProductByIdAsync),
            //    new { id = product.Id },
            //    product
            //    );

        }
    }
}
    