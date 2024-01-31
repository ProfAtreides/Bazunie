using System;
using System.Collections.Generic;

namespace Meetings.Models;

public partial class Filia
{
    public int Id { get; set; }

    public string? NazwaFilii { get; set; }

    public virtual ICollection<Dział> Działy { get; set; } = new List<Dział>();

    public virtual ICollection<Pracownik> Pracownicy { get; set; } = new List<Pracownik>();

    public virtual ICollection<Spotkanie> Spotkania { get; set; } = new List<Spotkanie>();
}
