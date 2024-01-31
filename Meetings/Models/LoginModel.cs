using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using MIG.Gateways.Authentication;

namespace Meetings.Models;

public class LoginModel : IdentityUser
{
    public int Login { get; set; }
    public string Password { get; set; }
}