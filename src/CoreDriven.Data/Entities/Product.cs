using System;
using System.Collections.Generic;

namespace CoreDriven.Data.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string CodeEan13 { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal Price { get; set; }

    public bool Active { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual Category? Category { get; set; }
}
