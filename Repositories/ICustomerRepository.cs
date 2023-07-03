using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public interface ICustomerRepository
{
////////////////////////////////////////////////////////////////////////////
// Customer
////////////////////////////////////////////////////////////////////////////
    void AddCustomer(Customer customer);
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync();
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    Task<Customer?> GetCustomerWithAddressesByIdAsync(int customerId);
    void UpdateCustomer(Customer customer);
    void RemoveCustomer(Customer customer);
    
////////////////////////////////////////////////////////////////////////////
// Address
//////////////////////////////////////////////////////////////////////////// 
    Task<bool> CustomerExistsAsync(int customerId);
    Task<IEnumerable<Address>> GetAddressesAsync(int customerId);
    Task<Address?> GetAddressByIdAsync(int customerId, int addressId);
    void AddAddress(Address address);
    void RemoveAddress(Address address); 

////////////////////////////////////////////////////////////////////////////
// Save Changes
//////////////////////////////////////////////////////////////////////////// 
    Task<bool> SaveChangesAsync();

}
