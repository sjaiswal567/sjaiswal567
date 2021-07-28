using AuthPen.Models;
using AuthPen.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthPen.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly PensionerDbContext _context;
        private IConfiguration _config;
        private readonly ITokenGeneratorService service;
        public AuthenticationController(PensionerDbContext context, IConfiguration config, ITokenGeneratorService _service)
        {
            _context = context;
            _config = config;
            service = _service;
        }

        [HttpPost("api/Login")]
        public IActionResult Login([FromBody] PensionUser User)
        {
            //IActionResult response = Unauthorized();
            var user = Authenticate(User);
            if (user != null)
            {
                var tokenString = service.GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }
            else
                return BadRequest("Invalid User");
        }
         public PensionUser Authenticate(PensionUser user)
        {
            PensionUser obj = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            return obj;
        }
    }
}
