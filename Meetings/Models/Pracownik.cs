using System;
using System.Collections.Generic;

namespace Meetings.Models;

public class Pracownik
{
    public bool admin { get; set; }
    public int Id { get; set; }

    public string? ImiePracownika { get; set; }

    public string? NazwiskoPracownika { get; set; }

    public string? Stanowisko { get; set; }

    public int IdFilii { get; set; }

    public int IdDzialu { get; set; }

    public string? Haslo { get; set; }

    public virtual ICollection<Grafik> Grafiks { get; set; } = new List<Grafik>();

    public virtual Działy IdDzialuNavigation { get; set; } = null!;

    public virtual Filie IdFiliiNavigation { get; set; } = null!;

    public virtual ICollection<Spotkanie> IdSpotkania { get; set; } = new List<Spotkanie>();

    public Pracownik()
    {
    }
}