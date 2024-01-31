using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meetings.Models;

public partial class Filia
{
    public int Id { get; set; }

    public string? NazwaFilii { get; set; }

    [ValidateNever]
    public virtual ICollection<Dział> Działy { get; set; } = new List<Dział>();

    [ValidateNever]
    public virtual ICollection<Pracownik> Pracownicy { get; set; } = new List<Pracownik>();

    [ValidateNever]
    public virtual ICollection<Spotkanie> Spotkania { get; set; } = new List<Spotkanie>();
}
