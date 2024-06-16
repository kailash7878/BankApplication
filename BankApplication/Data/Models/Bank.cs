namespace BankApplication.Data.models;

public partial class Bank
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Addrerss { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
