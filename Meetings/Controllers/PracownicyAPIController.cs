using System.Security.Claims;
using Meetings.Data;
using Meetings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.Controllers
{
    
    
    //For testing in postman use https://localhost:44336/api/pracownicyapi/{ENDPOINT}
    [Route("api/[controller]")]
    [ApiController]
    public class PracownicyAPIController : ControllerBase
    {
        private AppDbContext _context;
        public PracownicyAPIController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            var pracownikCurrent = GetCurrentPracownik();

            if (pracownikCurrent == null)
            {
                return Ok("ERROR USER NOT FOUND");
            }
            
            return Ok($"Hej adminie {pracownikCurrent.ImiePracownika}");
        }
        
        [HttpGet("Regulars")]
        [Authorize(Roles = "Regular")]
        public IActionResult RegularEndpoint()
        {
            var pracownikCurrent = GetCurrentPracownik();

            if (pracownikCurrent == null)
            {
                return Ok("ERROR USER NOT FOUND");
            }
            
            return Ok($"Hej pracowniku {pracownikCurrent.ImiePracownika}");
        }
        
        [HttpGet("Public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("Public property, make yourself comfortable :)");
        }

        private Pracownik GetCurrentPracownik()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var pracownikClaimed = identity.Claims;

                int pracownikIndex = Convert.ToInt32(pracownikClaimed.FirstOrDefault(
                    u => u.Type == ClaimTypes.NameIdentifier)?.Value);
                
                return _context.Pracownicy.FirstOrDefault(u => u.Id == pracownikIndex);;
            }

            return null;
        }
    }
}
