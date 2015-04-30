using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Repositories.Interfaces;
using Models.Entities;

namespace DataLayer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IContextManager contextManager;

        public CustomerRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        public void Create(Customer customer)
        {
            if (customer == null) return;
            var context = contextManager.CurrentContext;
            context.Customers.Add(customer);
            contextManager.Save(context);
        }

        public void Update(Customer customer)
        {
            if (customer == null) return;
            var context = contextManager.CurrentContext;
            context.Entry(customer).State = EntityState.Modified;
            contextManager.Save(context);
        }

        public bool Delete(Customer customer)
        {
            if (customer == null) return false;
            var context = contextManager.CurrentContext;
            context.Entry(customer).State = EntityState.Deleted;
            contextManager.Save(context);
            return true;
        }

        public Customer GetCustomerById(int customerId)
        {
            return contextManager.CurrentContext.Customers.AsNoTracking().FirstOrDefault(c => c.Id == customerId);
        }

        public Customer GetCustomerByName(string name)
        {
            return contextManager.CurrentContext.Customers.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }

        public Customer GetCustomerByMail(string mail)
        {
            return contextManager.CurrentContext.Customers.AsNoTracking().FirstOrDefault(c => c.Mail == mail);
        }

        public IList<Customer> GetCustomers()
        {
            return contextManager.CurrentContext.Customers.AsNoTracking().ToList();
        }
        
    }
}
