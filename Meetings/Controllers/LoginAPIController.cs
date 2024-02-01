using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Meetings.Data;
using Meetings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Cms;

namespace Meetings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        private bool _wrongPassword;
        
        private IConfiguration _config;

        public LoginAPIController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
            _wrongPassword = false;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login loginData)
        {
            var user = Authenticate(loginData);

            if (user != null)
            {
                if (_wrongPassword)
                {
                    _wrongPassword = false;
                    return ValidationProblem("Wrong password");
                }
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("Pracownik nieznany");
        }

        private string Generate(Pracownik pracownik)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, pracownik.Id.ToString()),
                new Claim(ClaimTypes.Role,(pracownik.Admin==true)?"Admin":"Regular")
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }

        private Pracownik Authenticate(Login pracownik)
        {
            var tempPracownik = _context.Pracownicy.FirstOrDefault(u => u.Id == pracownik.Id);
            if (tempPracownik != null)
            {
                if (Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: pracownik.Password!,
                        salt: Encoding.UTF8.GetBytes("prosze3;)"),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8)) == tempPracownik.Haslo)
                {
                    return tempPracownik;
                }

                _wrongPassword = true;
            }

            return null;
        }
        
        // GET: api/Login
        
    }
}
