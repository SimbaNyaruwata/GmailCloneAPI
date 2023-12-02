using GmailClone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;

namespace GmailClone.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly GmailCloneDbContext _context;

        
        private IConfiguration _config;
        public LoginController(IConfiguration config, GmailCloneDbContext context)
        {
            _config = config;
            _context = context;
        }




        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginRequest loginRequest)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(m => m.Email == loginRequest.email);

                if (user == null || !user.VerifyPassword(loginRequest.password))
                {
                    // Invalid credentials
                    return Unauthorized("Invalid email or password");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(token);
            }
            
            catch(Exception e)
            {
                throw;
                    
             }




            }
            
    }
}

