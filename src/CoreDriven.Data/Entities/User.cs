using System;
using System.Collections.Generic;

namespace CoreDriven.Data.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? ImageName { get; set; }

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public bool? Active { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Role Role { get; set; } = null!;
}
