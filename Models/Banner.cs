using System;
using System.Collections.Generic;
using NodaTime;

namespace G_UserInterface.Models;

public partial class Banner
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Status { get; set; }

    public int BannerLocation { get; set; }

    public string ImagePath { get; set; } = null!;

    public long RegUserId { get; set; }

    public LocalDate RegDate { get; set; }
}
