using System;
using System.Collections.Generic;

namespace CoreDriven.Data.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
