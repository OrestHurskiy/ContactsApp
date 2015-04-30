using System.Collections.Generic;
using Models.Entities;

namespace DataLayer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        void Update(Customer customer);
        bool Delete(Customer customer);
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByName(string name);
        Customer GetCustomerByMail(string mail);
        IList<Customer> GetCustomers();
    }
}
