using System;
using System.Collections.Generic;

namespace Meetings.Models;

public class Spotkanie
{
    public int Id { get; set; }

    public int? MaxLiczbaUczestnikow { get; set; }

    public TimeOnly? GodzinaSpotkania { get; set; }

    public string? DzienTygodnia { get; set; }

    public int IdSali { get; set; }

    public int IdFilii { get; set; }

    public virtual Filie IdFiliiNavigation { get; set; } = null!;

    public virtual Sala IdSaliNavigation { get; set; } = null!;

    public virtual ICollection<Pracownik> IdPracownikas { get; set; } = new List<Pracownik>();

    public Spotkanie()
    {
        
    }
}
