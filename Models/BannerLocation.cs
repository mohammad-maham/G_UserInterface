using System;
using System.Collections.Generic;

namespace G_UserInterface.Models;

public partial class BannerLocation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Size { get; set; } = null!;

    public int Status { get; set; }
}
