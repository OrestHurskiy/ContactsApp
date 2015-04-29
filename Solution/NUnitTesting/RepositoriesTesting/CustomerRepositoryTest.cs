using NUnit.Framework;
using DataLayer.Components;
using DataLayer.Wrapper;
using System.Collections.Generic;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class CustomerRepositoryTest
    {
        private IContextManager contextManager;
        private ICustomerRepository customerRepository;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            customerRepository = new CustomerRepository(contextManager);
        }

        [Test]
        public void CreateCustomer()
        {
            var customer = new Customer {Name = "Vanka", Mail = "ppg@mail.ru"};
            var customer1 = new Customer {Name = "Oleg", Mail = "andrey@gmail.com"};
            Assert.That(customer, !Is.Null);
            Assert.That(customer1, !Is.Null);
            customerRepository.Create(customer);
            customerRepository.Create(customer1);
            contextManager.BatchSave();

            Assert.That(customer, !Is.Null);
            Assert.That(customer.Id, !Is.NaN);
            Assert.That(customer.Id, Is.Positive);
            Assert.IsInstanceOf(typeof (int), customer.Id);

            Assert.That(customer1, !Is.Null);
            Assert.That(customer1.Id, !Is.NaN);
            Assert.That(customer1.Id, Is.Positive);
            Assert.IsInstanceOf(typeof (int), customer1.Id);
        }

        [Test]
        public void UpdateCustomer()
        {
            var customerUp = customerRepository.GetCustomerById(1);
            var customerUp1 = customerRepository.GetCustomerByName("Customer2");
            var customerUp2 = customerRepository.GetCustomerByMail("lol@mail.ru");
            Assert.That(customerUp, !Is.Null);
            Assert.That(customerUp1, !Is.Null);
            Assert.That(customerUp2, !Is.Null);

            customerUp.Name = "Olga";
            customerUp1.Mail = "Ololowa@mail.ru";
            customerUp2.Name = "Marta";
            customerRepository.Update(customerUp);
            customerRepository.Update(customerUp1);
            customerRepository.Update(customerUp2);
            contextManager.BatchSave();

            Assert.That(customerUp, !Is.Null);
            Assert.That(customerUp1, !Is.Null);
            Assert.That(customerUp2, !Is.Null);

        }

        [Test]
        public void DeleteCustomer()
        {
            var customerTodel = customerRepository.GetCustomerById(1);
            var customerTodel1 = customerRepository.GetCustomerByMail("Ololowa@mail.ru");
            var customerTodel2 = customerRepository.GetCustomerByName("Customer1");

            Assert.That(customerTodel, !Is.Null);
            Assert.That(customerTodel1, !Is.Null);
            Assert.That(customerTodel2, !Is.Null);

            Assert.IsTrue(customerRepository.Delete(customerTodel), "Something gone wrong");
            Assert.IsTrue(customerRepository.Delete(customerTodel1), "Something gone wrong");
            Assert.IsTrue(customerRepository.Delete(customerTodel2), "Something gone wrong");
            contextManager.BatchSave();
        }

        [Test]
        public void GetCustomerById()
        {
            var customer = customerRepository.GetCustomerById(4);
            Assert.That(customer, !Is.Null);
            Assert.AreEqual(customer.Id, 4);
            StringAssert.Contains(customer.Name, "Marta");
            StringAssert.Contains(customer.Mail, "lol@mail.ru");

            var customer1 = customerRepository.GetCustomerById(5);
            Assert.That(customer1, !Is.Null);
            Assert.AreEqual(customer1.Id, 5);
            StringAssert.Contains(customer1.Name, "Andrey");
            StringAssert.Contains(customer1.Mail, "andrey@gmail.com");
        }

        [Test]
        public void GetCustomerByName()
        {
            var customer = customerRepository.GetCustomerByName("Vanka");
            Assert.That(customer, !Is.Null);
            Assert.AreEqual(customer.Id, 6);
            StringAssert.Contains(customer.Name, "Vanka");
            StringAssert.Contains(customer.Mail, "ppg@mail.ru");

            var customer1 = customerRepository.GetCustomerByName("Oleg");
            Assert.That(customer1, !Is.Null);
            StringAssert.Contains(customer1.Name, "Oleg");
            Assert.AreEqual(customer1.Id, 7);
            StringAssert.Contains(customer1.Mail, "andrey@gmail.com");

        }

        [Test]
        public void GetCustomerByMail()
        {
            var customer = customerRepository.GetCustomerByMail("lol@mail.ru");
            Assert.That(customer, !Is.Null);
            Assert.AreEqual(customer.Id, 4);
            StringAssert.Contains(customer.Name, "Marta");
            StringAssert.Contains(customer.Mail, "lol@mail.ru");

            var customer1 = customerRepository.GetCustomerByMail("ppg@mail.ru");
            Assert.That(customer1, !Is.Null);
            Assert.AreEqual(customer1.Id, 6);
            StringAssert.Contains(customer1.Name, "Vanka");
            StringAssert.Contains(customer1.Mail, "ppg@mail.ru");

        }

        [Test]
        public void GetCustomers()
        {
            var customers = customerRepository.GetCustomers();
            Assert.That(customers, !Is.Null);
            Assert.IsNotEmpty(customers);
            Assert.IsInstanceOf(typeof (List<Customer>), customers);

        }
    }
}
