using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eShopAPI.Models;
using eShopAPI.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace eShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private ShopDbContext db;
        private IConfiguration configuration;

        public IdentityController(ShopDbContext dbContext, IConfiguration configuration)
        {
            this.db = dbContext;
            this.configuration = configuration;
        }

        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost("register")]

        public async Task<ActionResult<dynamic>> RegisterAsync([FromBody] User users)
        {
            TryValidateModel(users);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.users.AddAsync(users);
            await db.SaveChangesAsync();
            return Created("", new {
                users.Id,
                users.Fullname,
                users.Email,
                users.Username,
                users.Mobile,

            });
        }
        [HttpPost("token", Name = "GetToken")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public ActionResult<dynamic> GetToken(LoginModel model)
        {
            TryValidateModel(model);
           if (ModelState.IsValid)
            {
                var user = db.users.SingleOrDefault
                      (u => (u.Username == model.Username
                       || u.Email == model.Username
                       && u.Password == model.Password) && u.Password == model.Password);

                if (user == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(GenerateToken(user));
                }
            }
            else 
            {
                return BadRequest(ModelState);
            }

           }

        private dynamic GenerateToken(User users)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, users.Fullname),
                new Claim(JwtRegisteredClaimNames.Email, users.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, configuration.GetValue<string>("Jwt:Audience")));
            var SecurityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("Jwt:ValidityInMinutes")), 
                signingCredentials: Credentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new
            {
                users.Id, users.Fullname, users.Email, users.Mobile, token = tokenString
            };

        }

    }





}
