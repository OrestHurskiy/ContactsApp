using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.ExceptionServices;
using DataLayer;
using DataLayer.Context;
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

            customerToCreate = new Customer { Mail = "ivan@gmail.com", Name = "Ivan" };
            customerToCreate1 = new Customer { Mail = "ivan1@gmail.com", Name = "Ivan1" };
            customerToUpdate = new Customer { Mail = "maryan@gmail.com", Name = "Maryan" };
            customerToUpdate1 = new Customer { Mail = "maryan1@gmail.com", Name = "Maryan1" };
            customerToDelete = new Customer { Mail = "ivan2@gmail.com", Name = "Ivan2" };
            customerToDelete1 = new Customer { Mail = "ivan3@gmail.com", Name = "Ivan3" };
            customerToGet = new Customer {Mail = "ivan4@gmail.com", Name = "Ivan4"};

            customerRepository.Create(customerToUpdate);
            customerRepository.Create(customerToUpdate1);
            customerRepository.Create(customerToGet);
            contextManager.BatchSave();
         
        }

        [TearDown]
        public void TearDown()
        {
            customerRepository.Delete(customerToUpdate);
            customerRepository.Delete(customerToUpdate1);
            customerRepository.Delete(customerToGet);
            contextManager.BatchSave();
        }
               
        

        [Test]
        public void InsertCustomer_ToDatabase_InBatchMode_Success()
        {
            customerRepository.Create(customerToCreate);
            customerRepository.Create(customerToCreate1);
            contextManager.BatchSave();

            Assert.IsNotNull(customerRepository.GetCustomerById(customerToCreate.Id));
            Assert.IsNotNull(customerRepository.GetCustomerById(customerToCreate1.Id));

            customerRepository.Delete(customerToCreate);
            customerRepository.Delete(customerToCreate1);

        }

        [Test]
        public void UpdateCustomer_FromDatabase_InBatchMode_Success()
        {
            customerToUpdate.Name = "Taras";
            customerToUpdate.Mail = "taras@mail.ru";
            customerToUpdate1.Name = "Taras1";
            customerToUpdate1.Mail = "taras1@mail.ru";
            customerRepository.Update(customerToUpdate);
            customerRepository.Update(customerToUpdate1);
            contextManager.BatchSave();

            var updated = customerRepository.GetCustomerById(customerToUpdate.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual("taras@mail.ru",updated.Mail);
            Assert.AreEqual("Taras",updated.Name);

            var updated1 = customerRepository.GetCustomerById(customerToUpdate1.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual("taras1@mail.ru",updated1.Mail);
            Assert.AreEqual("Taras1",updated1.Name);
        }

        [Test]
        public void DeleteCustomer_FromDatabase_InBatchMode_Success()
        {
            customerRepository.Create(customerToDelete);
            customerRepository.Create(customerToDelete1);

            Assert.IsTrue(customerRepository.Delete(customerToDelete));
            Assert.IsTrue(customerRepository.Delete(customerToDelete1));
            contextManager.BatchSave();

            Assert.IsNull(customerRepository.GetCustomerById(customerToDelete.Id));
            Assert.IsNull(customerRepository.GetCustomerById(customerToDelete1.Id));
        }

        [Test]
        public void GetCustomerById_FromDatabase_Success()
        {
            var customer = customerRepository.GetCustomerById(customerToGet.Id);
            Assert.IsNotNull(customer);
            Assert.AreEqual("Ivan4",customer.Name);
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
        public void GetCustomersFrom_Database_Success()
        {
            var customers = customerRepository.GetCustomers();
            Assert.IsNotNull(customers);
            Assert.IsNotEmpty(customers);
            Assert.IsInstanceOf(typeof(List<Customer>), customers);
        }
    }
}
