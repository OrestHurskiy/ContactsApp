using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;
namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class PersonRepositorySingleSubmitTest
    {
        private IContextManager contextManager;
        private IPersonRepository personRepository;
        private Person personToCreate;
        private Person personToUpdate;
        private Person personToDelete;
        private Person personToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            personRepository = new PersonRepository(contextManager);

            personToCreate = new Person
            {
                FirstName = "Orest",
                LastName = "Hurskiy",
                Adress = "Zhovkva",
                Mail = "orest@gmail.com",
                Phone = "09345353",
                Skype = "orest.hurskiy",
                Gender = Gender.Male
            };

            personToUpdate = new Person
            {
                FirstName = "Ivan",
                LastName = "Ivanovych",
                Adress = "Lviv",
                Mail = "ivan@gmail.com",
                Phone = "09345353",
                Skype = "ivan",
                Gender = Gender.Male
            };

            personToDelete = new Person
            {
                FirstName = "Orest",
                LastName = "Hurskiy",
                Adress = "Zhovkva",
                Mail = "orest@gmail.com",
                Phone = "09345353",
                Skype = "orest.hurskiy",
                Gender = Gender.Male
            };

            personToGet = new Person
            {
                FirstName = "Personage",
                LastName = "Second Name",
                Adress = "Kyiv",
                Mail = "personage@gmail.com",
                Phone = "0983784876",
                Skype = "personage",
                Gender = Gender.Male
            };

            personRepository.Create(personToUpdate);
            personRepository.Create(personToGet);
        }

        [TearDown]
        public void TearDown()
        {
            personRepository.Delete(personToUpdate);
            personRepository.Delete(personToGet);
        }

        [Test]
        public void InsertPerson_ToDatabase_PerRequest_Success()
        {
            personRepository.Create(personToCreate);

            Assert.IsNotNull(personRepository.GetPersonById(personToCreate.Id));

            personRepository.Delete(personToCreate);
        }

        [Test]
        public void UpdatePerson_FromDatabase_PerRequest_Success()
        {
            personToUpdate.FirstName = "Person";
            personToUpdate.LastName = "LastName";
            personToUpdate.Mail = "person@mail.ru";
            personToUpdate.Phone = "09344545";
            personToUpdate.Skype = "skype";
            personToUpdate.Gender = Gender.Male;
            personToUpdate.Adress = "Adress";
            personRepository.Update(personToUpdate);

            var updated = personRepository.GetPersonById(personToUpdate.Id);
            Assert.AreEqual("Person", updated.FirstName);
            Assert.AreEqual("LastName", updated.LastName);
            Assert.AreEqual("person@mail.ru", updated.Mail);
            Assert.AreEqual("09344545", updated.Phone);
            Assert.AreEqual("skype", updated.Skype);
            Assert.AreEqual(Gender.Male, updated.Gender);
            Assert.AreEqual("Adress", updated.Adress);
        }

        [Test]
        public void DeletePerson_FromDatabase_PerRequest_Success()
        {
            personRepository.Create(personToDelete);

            Assert.IsTrue(personRepository.Delete(personToDelete));

            Assert.IsNull(personRepository.GetPersonById(personToDelete.Id));
        }

        [Test]
        public void GetPersonById_FromDatabase_Success()
        {
            var person = personRepository.GetPersonById(personToGet.Id);
            Assert.AreEqual("Personage", person.FirstName);
            Assert.AreEqual("Second Name", person.LastName);
            Assert.AreEqual("Kyiv", person.Adress);
            Assert.AreEqual("personage@gmail.com", person.Mail);
            Assert.AreEqual("0983784876", person.Phone);
            Assert.AreEqual("personage", person.Skype);
            Assert.AreEqual(Gender.Male, person.Gender);
        }

        [Test]
        public void GetPersonByFirstName__FromDatabase_Success()
        {
            var person = personRepository.GetPersonByFirstName(personToGet.FirstName);
            Assert.AreEqual("Personage", person.FirstName);
            Assert.AreEqual("Second Name", person.LastName);
            Assert.AreEqual("Kyiv", person.Adress);
            Assert.AreEqual("personage@gmail.com", person.Mail);
            Assert.AreEqual("0983784876", person.Phone);
            Assert.AreEqual("personage", person.Skype);
            Assert.AreEqual(Gender.Male, person.Gender);
        }

        [Test]
        public void GetPersonByLastName_FromDatabase_Success()
        {
            var person = personRepository.GetPersonByLastName(personToGet.LastName);
            Assert.AreEqual("Personage", person.FirstName);
            Assert.AreEqual("Second Name", person.LastName);
            Assert.AreEqual("Kyiv", person.Adress);
            Assert.AreEqual("personage@gmail.com", person.Mail);
            Assert.AreEqual("0983784876", person.Phone);
            Assert.AreEqual("personage", person.Skype);
            Assert.AreEqual(Gender.Male, person.Gender);
        }

        [Test]
        public void GetPersonByMail_FromDatabase_Success()
        {
            var person = personRepository.GetPersonByMail(personToGet.Mail);
            Assert.AreEqual("Personage", person.FirstName);
            Assert.AreEqual("Second Name", person.LastName);
            Assert.AreEqual("Kyiv", person.Adress);
            Assert.AreEqual("personage@gmail.com", person.Mail);
            Assert.AreEqual("0983784876", person.Phone);
            Assert.AreEqual("personage", person.Skype);
            Assert.AreEqual(Gender.Male, person.Gender);
        }

        [Test]
        public void GetPersons_FromDatabase_Success()
        {
            var persons = personRepository.GetPersons();
            Assert.IsNotNull(persons);
            Assert.IsNotEmpty(persons);
            Assert.IsInstanceOf(typeof(List<Person>), persons);
        }
    }
}
