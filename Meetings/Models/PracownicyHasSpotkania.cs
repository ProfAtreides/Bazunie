using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meetings.Models;


public partial class PracownicyHasSpotkania
{
    public int IdPracownika { get; set; }

    public int IdSpotkania { get; set; }

    [ValidateNever]
    public virtual Pracownik IdPracownikaNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual Spotkanie IdSpotkaniaNavigation { get; set; } = null!;
}
