﻿using System;
using System.Collections.Generic;

namespace G_UserInterface.Models;

public partial class Service
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public short Status { get; set; }

    public string? Caption { get; set; }

    public string? AccessInfo { get; set; }

    public short ServiceType { get; set; }
}
