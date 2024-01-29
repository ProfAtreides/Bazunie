using System;
using System.Collections.Generic;

namespace Meetings.Models;

public class Sala
{
    public int Id { get; set; }

    public int? Pojemnosc { get; set; }

    public virtual ICollection<Spotkanie> Spotkania { get; set; } = new List<Spotkanie>();

    public Sala()
    {
        
    }
}
