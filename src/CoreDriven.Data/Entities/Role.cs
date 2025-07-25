﻿using System;
using System.Collections.Generic;

namespace CoreDriven.Data.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
