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

            customerToCreate = new Customer {Id = 1, Mail = "orest@gmail.com", Name = "Orest"};
            customerToDelete = new Customer {Id = 15, Mail = "orest@gmail.com", Name = "Orest"};
            customerToGet = new Customer {Id = 4, Name = "Marta", Mail = "marta@gmail.com"};
            customerToUpdate = new Customer { Id = 6, Name = "Vanka", Mail = "lol@mail.ru" };

        }

        [TearDown]
        public void TearDown()
        {
            customerToCreate = null;
            customerToDelete = null;
            customerToUpdate = null;
            customerToGet = null;
        }

        [Test]
        public void InsertCustomerToDatabasePerRequestSuccess()
        {
            var initialId = customerToCreate.Id;
            customerRepository.Create(customerToCreate);
            Assert.AreNotEqual(initialId,customerToCreate.Id);
            Assert.IsNotNull(customerRepository.GetCustomerById(customerToCreate.Id));
            
        }

        [Test]
        public void UpdateCustomerFromDatabasePerRequestSuccess()
        {
            customerRepository.Update(customerToUpdate);
            Assert.IsNotNull(customerToUpdate);
            Assert.AreEqual(customerToUpdate.Mail,"lol@mail.ru");
            Assert.AreEqual(customerToUpdate.Id, 6);
            Assert.AreEqual(customerToUpdate.Name, "Vanka");
        }

        [Test]
        public void DeleteCustomerFromDatabasePerRequestSuccess()
        {
            Assert.IsTrue(customerRepository.Delete(customerToDelete));
            Assert.IsNull(customerRepository.GetCustomerByName(customerToDelete.Name));
            Assert.IsNull(customerRepository.GetCustomerByMail(customerToDelete.Mail));
        }

        [Test]
        public void GetCustomerByIdFromDatabaseSuccess()
        {
            var customer = customerRepository.GetCustomerById(customerToGet.Id);
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Id, 4);
            Assert.AreEqual(customer.Name, "Marta");
            Assert.AreEqual(customer.Mail, "marta@gmail.com");

        }

        [Test]
        public void GetCustomerByNameFromDatabaseSuccess()
        {
            var customer = customerRepository.GetCustomerByName(customerToGet.Name);
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Id, 4);
            Assert.AreEqual(customer.Name, "Marta");
            Assert.AreEqual(customer.Mail, "marta@gmail.com");

        }

        [Test]
        public void GetCustomerByMailFromDatabaseSuccess()
        {
            var customer = customerRepository.GetCustomerByMail(customerToGet.Mail);
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Id, 4);
            Assert.AreEqual(customer.Name, "Marta");
            Assert.AreEqual(customer.Mail, "marta@gmail.com");

        }

        [Test]
        public void GetCustomersFromDatabaseSuccess()
        {
            var customers = customerRepository.GetCustomers();
            Assert.IsNotNull(customers);
            Assert.IsNotEmpty(customers);
            Assert.IsInstanceOf(typeof (List<Customer>), customers);
        }
    }
}
