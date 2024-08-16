using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class Employeedatum
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
