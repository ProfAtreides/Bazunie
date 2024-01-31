using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Meetings.Models;


public partial class Spotkanie
{
    public int Id { get; set; }

    public int? MaxLiczbaUczestnikow { get; set; }

    public TimeOnly? GodzinaSpotkania { get; set; }

    public string? DzienTygodnia { get; set; }

    public int IdSali { get; set; }

    public int IdFilii { get; set; }

    [ValidateNever]
    public virtual Filia IdFiliiNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual Sala IdSaliNavigation { get; set; } = null!;
}
