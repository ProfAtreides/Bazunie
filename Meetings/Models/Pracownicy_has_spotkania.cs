using Microsoft.EntityFrameworkCore;

namespace Meetings.Models;

[Keyless]
public class PracownicyHasSpotkania
{
    public int IdSpotkania { get; set; }
    public int IdPracownika { get; set; }
}