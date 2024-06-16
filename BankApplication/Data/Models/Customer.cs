using System;
using System.Collections.Generic;

namespace BankApplication.Data.models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? BankId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Bank? Bank { get; set; }
}
