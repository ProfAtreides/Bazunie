using System;
using System.Collections.Generic;

namespace Meetings.Models;

public class Filie
{
    public int Id { get; set; }

    public string? NazwaFilii { get; set; }

    public virtual ICollection<Działy> Działies { get; set; } = new List<Działy>();

    public virtual ICollection<Pracownik> Pracownicies { get; set; } = new List<Pracownik>();

    public virtual ICollection<Spotkanie> Spotkania { get; set; } = new List<Spotkanie>();

    public Filie()
    {
    }
}
