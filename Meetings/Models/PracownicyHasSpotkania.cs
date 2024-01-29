using System;
using System.Collections.Generic;
using Meetings.Models;

namespace Meetings;

public partial class PracownicyHasSpotkania
{
    public int IdPracownika { get; set; }

    public int IdSpotkania { get; set; }

    public virtual Pracownik IdPracownikaNavigation { get; set; } = null!;

    public virtual Spotkanie IdSpotkaniaNavigation { get; set; } = null!;
}
