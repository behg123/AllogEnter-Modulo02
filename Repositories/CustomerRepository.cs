using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;
using Univali.Api.Entities;

namespace Univali.Api.Repositories;

//Implementa a interface ICustomerRepository
public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext customerContext)
    {
        _context = customerContext;
    }



////////////////////////////////////////////////////////////////////////////
// Customer
////////////////////////////////////////////////////////////////////////////
    ///////////////////////////
    // Create
    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    ///////////////////////////
    // Read
    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
    }
    public async Task<Customer?> GetCustomerWithAddressesByIdAsync(int customerId)
    {
        return await _context.Customers.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == customerId);
    }
    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return await _context.Customers.OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync()
    {
        return await _context.Customers.Include(c => c.Addresses).OrderBy(c => c.Id).ToListAsync();
    }

    ///////////////////////////
    // Delete
    public void RemoveCustomer(Customer customer)
    {
        _context.Customers.Remove(customer);
    }

    ///////////////////////////
    // Utils
    public async Task<bool> CustomerExistsAsync(int customerId)
    {
        return await _context.Customers
             .AnyAsync(customer => customer.Id == customerId);
    }


////////////////////////////////////////////////////////////////////////////
// Address
////////////////////////////////////////////////////////////////////////////
    ///////////////////////////
    // Create
    public void AddAddress(Address address)
    {
        _context.Addresses.Add(address);
    }

    ///////////////////////////
    // Read
    public async Task<IEnumerable<Address>> GetAddressesAsync(int customerId)
    {
        return await _context
            .Addresses.OrderBy(a => a.Id).Where(a => a.CustomerId == customerId).ToListAsync();
    }

    public async Task<Address?> GetAddressByIdAsync(int customerId, int addressId)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerId == customerId && a.Id == addressId);
    }

    ///////////////////////////
    // Remove
    public void RemoveAddress(Address address)
    {
        _context.Addresses.Remove(address);
    }


////////////////////////////////////////////////////////////////////////////
// Save Changes
////////////////////////////////////////////////////////////////////////////
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
