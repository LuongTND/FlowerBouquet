using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class FlowerBouquet
{
    public int FlowerBouquetId { get; set; }

    public int CategoryId { get; set; }

    public string FlowerBouquetName { get; set; }

    public string Description { get; set; }

    public decimal UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public byte? FlowerBouquetStatus { get; set; }

    public int? SupplierId { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier Supplier { get; set; }
}
