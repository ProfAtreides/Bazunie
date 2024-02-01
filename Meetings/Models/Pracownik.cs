using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meetings.Models;

public partial class Pracownik
{
    public int Id { get; set; }

    public string? ImiePracownika { get; set; }

    public string? NazwiskoPracownika { get; set; }

    public string? Stanowisko { get; set; }

    public int IdFilii { get; set; }

    public int IdDzialu { get; set; }

    public string? Haslo { get; set; }

    public bool Admin { get; set; }

    [ValidateNever]
    public virtual ICollection<Grafik> Grafik { get; set; } = new List<Grafik>();

    [ValidateNever]
    public virtual Dział IdDzialuNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual Filia IdFiliiNavigation { get; set; } = null!;
}
