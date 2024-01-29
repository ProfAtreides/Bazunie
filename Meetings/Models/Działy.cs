using System;
using System.Collections.Generic;
using MessagePack;

namespace Meetings.Models;

public class Działy
{
    
    public int Id { get; set; }

    public string? NazwaDzialu { get; set; }

    public int IdFilii { get; set; }

    public virtual Filie IdFiliiNavigation { get; set; } = null!;

    public virtual ICollection<Pracownik> Pracownicies { get; set; } = new List<Pracownik>();

    public Działy()
    {
            
    }
}
