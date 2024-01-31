using System;
using System.Collections.Generic;

namespace Meetings.Models;


public partial class PracownicyHasSpotkania
{
    public int IdPracownika { get; set; }

    public int IdSpotkania { get; set; }

    public virtual Pracownik IdPracownikaNavigation { get; set; } = null!;

    public virtual Spotkanie IdSpotkaniaNavigation { get; set; } = null!;
}
