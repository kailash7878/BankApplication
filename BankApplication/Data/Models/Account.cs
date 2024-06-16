using System;
using System.Collections.Generic;

namespace BankApplication.Data.models;

public partial class Account
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public string? AccountNumber { get; set; }

    public decimal? Balance { get; set; }

    public decimal? InterestRate { get; set; }

    public virtual Customer? Customer { get; set; }
}
