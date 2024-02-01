using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using MIG.Gateways.Authentication;

namespace Meetings.Models;

public class Login
{
    public int Id { get; set; }
    public string Password { get; set; }

    public static string Token { get; set; } = null;

    public static bool loggedIn { get; set; } = false;

    public static Pracownik Pracownik { get; set;}
}


