using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;
namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class CustomerRepositoryBatchSubmitTest
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
            contextManager = new ContextManager();
            customerRepository = new CustomerRepository(contextManager);

            customerToCreate = new Customer { Mail = "ivan@gmail.com", Name = "Ivan" };
            customerToUpdate = new Customer { Mail = "ivan1@gmail.com", Name = "Ivan1" };
            customerToDelete = new Customer { Mail = "ivan2@gmail.com", Name = "Ivan2" };
            customerToGet = new Customer {Mail = "ivan3@gmail.com", Name = "Ivan3"};
         
            customerRepository.Create(customerToUpdate);
            customerRepository.Create(customerToDelete);
            customerRepository.Create(customerToGet);
            contextManager.BatchSave();
        }

        [TearDown]
        public void TearDown()
        {
            customerRepository.Delete(customerToCreate);
            customerRepository.Delete(customerToGet);
            customerRepository.Delete(customerToUpdate);
            contextManager.BatchSave();
        }

        [Test]
        public void InsertCustomerToDatabaseInBatchModeSuccess()
        {
            customerRepository.Create(customerToCreate);
            contextManager.BatchSave();

            Assert.IsNotNull(customerRepository.GetCustomerById(customerToCreate.Id));
        }

        [Test]
        public void UpdateCustomerFromDatabaseInBatchModeSuccess()
        {
            customerToUpdate.Name = "Taras";
            customerToUpdate.Mail = "taras@mail.ru";
            customerRepository.Update(customerToUpdate);
            contextManager.BatchSave();

            Assert.IsNotNull(customerToUpdate);
            Assert.AreEqual(customerToUpdate.Id,31);
            Assert.AreEqual(customerToUpdate.Mail, "taras@mail.ru");
            Assert.AreEqual(customerToUpdate.Name, "Taras");
        }

        [Test]
        public void DeleteCustomerFromDatabaseInBatchModeSuccess()
        {
            Assert.IsTrue(customerRepository.Delete(customerToDelete));
            contextManager.BatchSave();
            Assert.IsNull(customerRepository.GetCustomerByName(customerToDelete.Name));
        }

        [Test]
        public void GetCustomerByIdFromDatabaseSuccess()
        {
            var customer = customerRepository.GetCustomerById(customerToGet.Id);
            Assert.IsNotNull(customer);
            Assert.AreEqual(20, customer.Id);
            Assert.AreEqual("Ivan3",customer.Name);
            Assert.AreEqual("ivan3@gmail.com", customer.Mail);
            
        }

        [Test]
        public void GetCustomerByNameFromDatabaseSuccess()
        {
            var customer = customerRepository.GetCustomerByName(customerToGet.Name);
            Assert.IsNotNull(customer);
            Assert.AreEqual(20, customer.Id);
            Assert.AreEqual("Ivan3", customer.Name);
            Assert.AreEqual("ivan3@gmail.com", customer.Mail);
        }

        [Test]
        public void GetCustomerByMailFromDatabaseSuccess()
        {
            var customer = customerRepository.GetCustomerByMail(customerToGet.Mail);
            Assert.IsNotNull(customer);
            Assert.AreEqual(20, customer.Id);
            Assert.AreEqual("Ivan3", customer.Name);
            Assert.AreEqual("ivan3@gmail.com", customer.Mail);
        }

        [Test]
        public void GetCustomersFromDatabaseSuccess()
        {
            var customers = customerRepository.GetCustomers();
            Assert.IsNotNull(customers);
            Assert.IsNotEmpty(customers);
            Assert.IsInstanceOf(typeof(List<Customer>), customers);
        }
    }
}
