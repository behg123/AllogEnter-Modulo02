using Microsoft.EntityFrameworkCore;
using Univali.Api.Entities;

namespace Univali.Api.DbContexts;

public class CustomerContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;

    public CustomerContext(DbContextOptions<CustomerContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var customer = modelBuilder.Entity<Customer>(); 

        customer.Property(c => c.Name)
            .HasMaxLength(80)
            .IsRequired();

        customer.Property(c => c.Cpf)
            .HasMaxLength(11)
            .IsFixedLength();


        modelBuilder.Entity<Customer>()
            .HasData(
                new Customer()
                {
                    Id = 1,
                    Name = "Linus Torvalds",
                    Cpf = "73473943096",

                },
                new Customer
                {
                    Id = 2,
                    Name = "Bill Gates",
                    Cpf = "95395994076",
                });

        modelBuilder.Entity<Address>()
            .HasData(
                    new Address()
                    {
                        Id = 1,
                        Street = "Verão do Cometa",
                        City = "Elvira",
                        CustomerId = 1
                    },
                    new Address()
                    {
                        Id = 2,
                        Street = "Borboletas Psicodélicas",
                        City = "Perobia",
                        CustomerId = 1
                    },
                    new Address()
                    {
                        Id = 3,
                        Street = "Canção Excêntrica",
                        City = "Salandra",
                        CustomerId = 2
                    }
            );

        var address = modelBuilder.Entity<Address>();  

        address.Property(a => a.Street).HasMaxLength(50).IsRequired();

        address.Property(a => a.City).HasMaxLength(50).IsFixedLength();

        base.OnModelCreating(modelBuilder);
    }

}