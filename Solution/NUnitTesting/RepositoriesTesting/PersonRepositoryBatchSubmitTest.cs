using System.Collections.Generic;
using System.Diagnostics;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class PersonRepositoryBatchSubmitTest
    {
        private IContextManager contextManager;
        private IPersonRepository personRepository;
        private Person personToCreate;
        private Person personToCreate1;
        private Person personToUpdate;
        private Person personToUpdate1;
        private Person personToDelete;
        private Person personToDelete1;
        private Person personToGet;

        [SetUp]
        public void SetUp()
        {

            contextManager = new ContextManager();
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

            personToCreate1 = new Person
            {
                FirstName = "Oleg",
                LastName = "Gerasumchyk",
                Adress = "Zhovkva",
                Mail = "oleg@gmail.com",
                Phone = "09734366",
                Skype = "oleg.gerasumchyk",
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

            personToUpdate1 = new Person
            {
                FirstName = "Olga",
                LastName = "Pogrognik",
                Adress = "Zhovkva",
                Mail = "olga@gmail.com",
                Phone = "09345553",
                Skype = "olga",
                Gender = Gender.Female
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

            personToDelete1 = new Person
            {
                FirstName = "Oleg",
                LastName = "Gerasumchyk",
                Adress = "Zhovkva",
                Mail = "oleg@gmail.com",
                Phone = "09734366",
                Skype = "oleg.gerasumchyk",
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
            personRepository.Create(personToUpdate1);
            personRepository.Create(personToGet);
            contextManager.BatchSave();
        }

        [TearDown]
        public void TearDown()
        {
            personRepository.Delete(personToUpdate);
            personRepository.Delete(personToUpdate1);
            personRepository.Delete(personToGet);
            contextManager.BatchSave();
        }


        [Test]
        public void InsertPerson_ToDatabase_InBatchMode_Success()
        {
            personRepository.Create(personToCreate);
            personRepository.Create(personToCreate1);
            contextManager.BatchSave();

            Assert.IsNotNull(personRepository.GetPersonById(personToCreate.Id));
            Assert.IsNotNull(personRepository.GetPersonById(personToCreate1.Id));

            personRepository.Delete(personToCreate);
            personRepository.Delete(personToCreate1);
        }

        [Test]
        public void UpdatePerson_FromDatabase_InBatchMode_Success()
        {
            personToUpdate.FirstName = "Person";
            personToUpdate.LastName = "LastName";
            personToUpdate.Mail = "person@mail.ru";
            personToUpdate.Phone = "09344545";
            personToUpdate.Skype = "skype";
            personToUpdate.Gender = Gender.Male;
            personToUpdate.Adress = "Adress";

            personToUpdate1.FirstName = "Person1";
            personToUpdate1.LastName = "LastName1";
            personToUpdate1.Mail = "person1@mail.ru";
            personToUpdate1.Phone = "09344532";
            personToUpdate1.Skype = "skype1";
            personToUpdate1.Gender = Gender.Male;
            personToUpdate1.Adress = "Adress1";

            personRepository.Update(personToUpdate);
            personRepository.Update(personToUpdate1);
            contextManager.BatchSave();

            var updated = personRepository.GetPersonById(personToUpdate.Id);
            Assert.AreEqual("Person", updated.FirstName);
            Assert.AreEqual("LastName", updated.LastName);
            Assert.AreEqual("person@mail.ru", updated.Mail);
            Assert.AreEqual("09344545", updated.Phone);
            Assert.AreEqual("skype",updated.Skype);
            Assert.AreEqual(Gender.Male, updated.Gender);
            Assert.AreEqual("Adress",updated.Adress);

            var updated1 = personRepository.GetPersonById(personToUpdate1.Id);
            Assert.AreEqual("Person1", updated1.FirstName);
            Assert.AreEqual("LastName1", updated1.LastName);
            Assert.AreEqual("person1@mail.ru", updated1.Mail);
            Assert.AreEqual("09344532", updated1.Phone);
            Assert.AreEqual("skype1", updated1.Skype);
            Assert.AreEqual(Gender.Male, updated1.Gender);
            Assert.AreEqual("Adress1", updated1.Adress);

        }

        [Test]
        public void DeletePerson_FromDatabase_InBatchMode_Success()
        {
            personRepository.Create(personToDelete);
            personRepository.Create(personToDelete1);
            contextManager.BatchSave();

            Assert.IsTrue(personRepository.Delete(personToDelete));
            Assert.IsTrue(personRepository.Delete(personToDelete1));
            contextManager.BatchSave();

            Assert.IsNull(personRepository.GetPersonById(personToDelete.Id));
            Assert.IsNull(personRepository.GetPersonById(personToDelete1.Id));
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
