using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meetings.Models;

public partial class Dział
{
    public int Id { get; set; }

    public string? NazwaDzialu { get; set; }

    public int IdFilii { get; set; }

   [ValidateNever]
    public virtual Filia IdFiliiNavigation { get; set; } = null!;

   [ValidateNever]
    public virtual ICollection<Pracownik> Pracownicy { get; set; } = new List<Pracownik>();
}
