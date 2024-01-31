using System;
using System.Collections.Generic;

namespace Meetings.Models;

public partial class Grafik
{
    public int Id { get; set; }

    public TimeOnly? GodzinaRozpoczecia { get; set; }

    public TimeOnly? GodzinaZakonczenia { get; set; }

    public int? IloscGodzin { get; set; }

    public string? DzienTygodnia { get; set; }

    public int IdPracownika { get; set; }

    public virtual Pracownik IdPracownikaNavigation { get; set; } = null!;
}
