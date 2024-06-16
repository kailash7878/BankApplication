using BankApplication.Data.models;
using BankApplication.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Data;

public partial class BankAppContext : DbContext
{
    public BankAppContext()
    {
      
    }

    public BankAppContext(DbContextOptions<BankAppContext> options)
        : base(options)
    {
     
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<AccountLedger> AccountLedger { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=.;Database=BankDB;User ID=sa;password=Kailash7878;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC0782DB7DDD");

            entity.ToTable("Account");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Balance).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.InterestRate).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Account__Custome__3C69FB99");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bank__3214EC07A0F9D1CF");

            entity.ToTable("Bank");

            entity.Property(e => e.Addrerss)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0757994B08");

            entity.ToTable("Customer");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Bank).WithMany(p => p.Customers)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK__Customer__BankId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
