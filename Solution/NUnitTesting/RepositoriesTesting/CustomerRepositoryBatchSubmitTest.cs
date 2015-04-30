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
        private Customer customerToCreate1;
        private Customer customerToDelete;
        private Customer customerToDelete1;
        private Customer customerToUpdate;
        private Customer customerToUpdate1;
        private Customer customerToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            customerRepository = new CustomerRepository(contextManager);

            customerToCreate = new Customer { Id = 1, Mail = "ivan@gmail.com", Name = "Ivan" };
            customerToCreate1 = new Customer { Id = 2, Mail = "ivan1@gmail.com", Name = "Ivan1" };
            customerToUpdate = new Customer {Id = 5, Name = "Andrey", Mail = "andrey@gmail.com"};
            customerToUpdate1 = new Customer {Id = 6, Name = "Vanka", Mail = "lol@mail.ru"};
            customerToDelete = new Customer { Id = 17, Mail = "ivan@gmail.com", Name = "Ivan" };
            customerToDelete1 = new Customer { Id = 18, Mail = "ivan1@gmail.com", Name = "Ivan1" };
            customerToGet = new Customer {Id = 4, Name = "Marta", Mail = "marta@gmail.com"};
        }

        [TearDown]
        public void TearDown()
        {
            customerToCreate = null;
            customerToCreate1 = null;
            customerToUpdate = null;
            customerToUpdate1 = null;
            customerToDelete = null;
            customerToDelete1 = null;
        }

        [Test]
        public void InsertCustomerToDatabaseInBatchModeSuccess()
        {
            var initialId = customerToCreate.Id;
            var initialId1 = customerToCreate1.Id;

            customerRepository.Create(customerToCreate);
            customerRepository.Create(customerToCreate1);
            contextManager.BatchSave();

            Assert.AreNotEqual(initialId,customerToCreate.Id);
            Assert.AreNotEqual(initialId1,customerToCreate1.Id);
            Assert.IsNotNull(customerRepository.GetCustomerById(customerToCreate.Id));
            Assert.IsNotNull(customerRepository.GetCustomerById(customerToCreate1.Id));
        }

        [Test]
        public void UpdateCustomerFromDatabaseInBatchModeSuccess()
        {
            customerRepository.Update(customerToUpdate);
            customerRepository.Update(customerToUpdate1);
            contextManager.BatchSave();

            Assert.IsNotNull(customerToUpdate);
            Assert.AreEqual(customerToUpdate.Id,5);
            Assert.AreEqual(customerToUpdate.Mail,"andrey@gmail.com");
            Assert.AreEqual(customerToUpdate.Name, "Andrey");

            Assert.IsNotNull(customerToUpdate1);
            Assert.AreEqual(customerToUpdate1.Id, 6);
            Assert.AreEqual(customerToUpdate1.Mail, "lol@mail.ru");
            Assert.AreEqual(customerToUpdate1.Name, "Vanka");
        }

        [Test]
        public void DeleteCustomerFromDatabaseInBatchModeSuccess()
        {
            Assert.IsTrue(customerRepository.Delete(customerToDelete));
            Assert.IsTrue(customerRepository.Delete(customerToDelete1));
            contextManager.BatchSave();

            Assert.IsNull(customerRepository.GetCustomerByName(customerToDelete.Name));
            Assert.IsNull(customerRepository.GetCustomerByMail(customerToDelete.Mail));

            Assert.IsNull(customerRepository.GetCustomerByName(customerToDelete1.Name));
            Assert.IsNull(customerRepository.GetCustomerByMail(customerToDelete1.Mail));
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
            Assert.IsInstanceOf(typeof(List<Customer>), customers);
        }
    }
}
