using NUnit.Framework;
using Models.Entities;
using DataLayer.Repositories;
using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories.Interfaces;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class CustomerRepositorySingleSubmitTest
    {
        private IContextManager contextManager;
        private ICustomerRepository customerRepository;
        private Customer customerToCreate;
        private Customer customerToDelete;
        private Customer customerToUpdate;
        private Customer customerToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            customerRepository = new CustomerRepository(contextManager);

            customerToCreate = new Customer { Mail = "ivan@gmail.com", Name = "Ivan" };
            customerToUpdate = new Customer { Mail = "maryan@gmail.com", Name = "Maryan" };
            customerToDelete = new Customer { Mail = "ivan2@gmail.com", Name = "Ivan2" };
            customerToGet = new Customer { Mail = "ivan4@gmail.com", Name = "Ivan4" };

            customerRepository.Create(customerToUpdate);
            customerRepository.Create(customerToGet);
        }

        [TearDown]
        public void TearDown()
        {
            customerRepository.Delete(customerToUpdate);
            customerRepository.Delete(customerToGet);
        }

        [Test]
        public void InsertCustomer_ToDatabase_PerRequest_Success()
        {
            customerRepository.Create(customerToCreate);

            Assert.IsNotNull(customerRepository.GetCustomerById(customerToCreate.Id));
            
            customerRepository.Delete(customerToCreate);
            
        }

        [Test]
        public void UpdateCustomer_FromDatabase_PerRequest_Success()
        {
            customerToUpdate.Name = "Taras";
            customerToUpdate.Mail = "taras@mail.ru";
            customerRepository.Update(customerToUpdate);

            var updated = customerRepository.GetCustomerById(customerToUpdate.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual("taras@mail.ru", updated.Mail);
            Assert.AreEqual("Taras", updated.Name);
        }

        [Test]
        public void DeleteCustomer_FromDatabase_PerRequest_Success()
        {
            customerRepository.Create(customerToDelete);

            Assert.IsTrue(customerRepository.Delete(customerToDelete));

            Assert.IsNull(customerRepository.GetCustomerById(customerToDelete.Id));
        }

        [Test]
        public void GetCustomerById_FromDatabase_Success()
        {
            var customer = customerRepository.GetCustomerById(customerToGet.Id);
            Assert.IsNotNull(customer);
            Assert.AreEqual("Ivan4", customer.Name);
            Assert.AreEqual("ivan4@gmail.com", customer.Mail);

        }

        [Test]
        public void GetCustomerByName_FromDatabase_Success()
        {
            var customer = customerRepository.GetCustomerByName(customerToGet.Name);
            Assert.IsNotNull(customer);
            Assert.AreEqual("Ivan4", customer.Name);
            Assert.AreEqual("ivan4@gmail.com", customer.Mail);
        }

        [Test]
        public void GetCustomerByMail_FromDatabase_Success()
        {
            var customer = customerRepository.GetCustomerByMail(customerToGet.Mail);
            Assert.IsNotNull(customer);
            Assert.AreEqual("Ivan4", customer.Name);
            Assert.AreEqual("ivan4@gmail.com", customer.Mail);

        }

        [Test]
        public void GetCustomers_FromDatabase_Success()
        {
            var customers = customerRepository.GetCustomers();
            Assert.IsNotNull(customers);
            Assert.IsNotEmpty(customers);
            Assert.IsInstanceOf(typeof (List<Customer>), customers);
        }
    }
}
