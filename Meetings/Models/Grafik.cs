using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meetings.Models;

public partial class Grafik
{
    public int Id { get; set; }

    public TimeOnly? GodzinaRozpoczecia { get; set; }

    public TimeOnly? GodzinaZakonczenia { get; set; }

    public int? IloscGodzin { get; set; }

    public string? DzienTygodnia { get; set; }

    public int IdPracownika { get; set; }

    [ValidateNever]
    public virtual Pracownik IdPracownikaNavigation { get; set; } = null!;
}
