using Meetings.Data;
using System.Text;
using Meetings.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Meetings.Controllers;

public class LoginController : Controller
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult LogOut()
    {
        Login.Token = null;
        Login.loggedIn = false;
        Login.Pracownik = null;
        return Redirect("../");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([Bind("Id,Password")] Login login)
    {
        var tempPracownik = _context.Pracownicy.FirstOrDefault(u => u.Id == login.Id);
        if (tempPracownik != null)
        {
            if (Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: login.Password!,
                    salt: Encoding.UTF8.GetBytes("prosze3;)"),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8)) == tempPracownik.Haslo)
            {
               Login.loggedIn = true;
               Login.Pracownik = tempPracownik;
               return Redirect("../"); 
            }
        }
        return View();
    }
}