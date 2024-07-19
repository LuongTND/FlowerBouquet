using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Email { get; set; }

    public string CustomerName { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Password { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
