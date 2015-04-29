using System.Collections;
using System.Collections.Generic;
using DataLayer.Components;

namespace DataLayer.Wrapper
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
