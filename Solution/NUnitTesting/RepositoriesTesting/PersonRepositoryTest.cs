using System.Collections.Generic;
using DataLayer.Components;
using DataLayer.Wrapper;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class PersonRepositoryTest
    {
        private IContextManager contextManager;
        private IPersonRepository personRepository;

        [SetUp]
        public void SetUp()
        {

            contextManager = new ContextManager();
            personRepository = new PersonRepository(contextManager);
        }


        [Test]
        public void CreatePerson()
        {
            var person = new Person
            {
                FirstName = "Marta",
                LastName = "Kylavech",
                Adress = "Zhovkva",
                Gender = Gender.Female,
                Mail = "marta@gmail.com",
                Phone = "222221"
            };
            var person1 = new Person
            {
                FirstName = "Ivan",
                LastName = "Ivanovich",
                Adress = "Lviv",
                Gender = Gender.Male,
                Mail = "ivan@gmail.com",
                Phone = "32145"
            };

            personRepository.Create(person);
            personRepository.Create(person1);
            contextManager.BatchSave();

            Assert.That(personRepository.GetPersonById(person.Id), !Is.Null);
            Assert.That(person, !Is.Null);
            Assert.That(person.Id, !Is.NaN);
            Assert.That(person.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), person.Id);

            Assert.That(personRepository.GetPersonById(person1.Id), !Is.Null);
            Assert.That(person1, !Is.Null);
            Assert.That(person1, !Is.Null);
            Assert.That(person1.Id, !Is.NaN);
            Assert.That(person1.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), person1.Id);

        }

        [Test]
        public void UpdatePerson()
        {
            var personUp = personRepository.GetPersonById(1);
            var personUp1 = personRepository.GetPersonByFirstName("Oleg");
            var personUp2 = personRepository.GetPersonByLastName("LastName1");
            Assert.That(personUp, !Is.Null);
            Assert.That(personUp1, !Is.Null);
            Assert.That(personUp2, !Is.Null);

            personUp.FirstName = "Orest";
            personUp1.LastName = "Gerasym";
            personUp2.Adress = "Lviv";

            personRepository.Update(personUp);
            personRepository.Update(personUp1);
            personRepository.Update(personUp2);
            contextManager.BatchSave();

            StringAssert.Contains(personUp.FirstName, "Orest");
            StringAssert.Contains(personUp1.LastName, "Gerasym");
            StringAssert.Contains(personUp2.Adress, "Lviv");

        }

        [Test]
        public void DeletePerson()
        {
            var personTodel = personRepository.GetPersonById(4);
            var personTodel1 = personRepository.GetPersonByFirstName("Marta");

            Assert.That(personTodel, !Is.Null);
            Assert.That(personTodel1, !Is.Null);

            Assert.IsTrue(personRepository.Delete(personTodel), "Something go wrong");
            Assert.IsTrue(personRepository.Delete(personTodel1), "Something go wrong");
            contextManager.BatchSave();
        }

        [Test]
        public void GetPersonById()
        {
            var person = personRepository.GetPersonById(2);
            Assert.That(person, !Is.Null);
            Assert.AreEqual(person.Id, 2);
            StringAssert.Contains(person.FirstName, "Oleg");
            StringAssert.Contains(person.LastName, "Gerasym");

            var person1 = personRepository.GetPersonById(16);
            Assert.That(person1, !Is.Null);
            Assert.AreEqual(person1.Id, 16);
            StringAssert.Contains(person1.FirstName, "FisrtName1");
            StringAssert.Contains(person1.LastName, "LastName1");
            StringAssert.Contains(person1.Adress, "Lviv");
            Assert.AreEqual(person1.Gender, Gender.Female);

        }

        [Test]
        public void GetPersonByFirstName()
        {
            var person = personRepository.GetPersonByFirstName("Orest");
            Assert.That(person, !Is.Null);
            Assert.AreEqual(person.Id, 1);
            StringAssert.Contains(person.FirstName, "Orest");

            var person1 = personRepository.GetPersonByFirstName("Oleg");
            Assert.That(person1, !Is.Null);
            Assert.AreEqual(person1.Id, 2);
            StringAssert.Contains(person1.FirstName, "Oleg");

        }

        [Test]
        public void GetPersonByLastName()
        {
            var person = personRepository.GetPersonByLastName("Gerasym");
            Assert.That(person, !Is.Null);
            Assert.AreEqual(person.Id, 2);
            StringAssert.Contains(person.FirstName, "Oleg");
            StringAssert.Contains(person.LastName, "Gerasym");

            var person1 = personRepository.GetPersonByLastName("LastName2");
            Assert.That(person1, !Is.Null);
            Assert.AreEqual(person1.Id, 17);
            StringAssert.Contains(person1.FirstName, "FisrtName2");
            StringAssert.Contains(person1.LastName, "LastName2");
            StringAssert.Contains(person1.Adress, "Adress2");
            Assert.AreEqual(person1.Gender, Gender.Male);

        }

        [Test]
        public void GetPersonByMail()
        {
            var person = personRepository.GetPersonByMail("mail1@gmail.com");
            Assert.That(person, !Is.Null);
            Assert.AreEqual(person.Id, 16);
            StringAssert.Contains(person.FirstName, "FisrtName1");
            StringAssert.Contains(person.LastName, "LastName1");
            StringAssert.Contains(person.Adress, "Lviv");
            Assert.AreEqual(person.Gender, Gender.Female);

            var person1 = personRepository.GetPersonByMail("mail2@gmail.com");
            Assert.That(person1, !Is.Null);
            Assert.AreEqual(person1.Id, 17);
            StringAssert.Contains(person1.FirstName, "FisrtName2");
            StringAssert.Contains(person1.LastName, "LastName2");
            StringAssert.Contains(person1.Adress, "Adress2");
            Assert.AreEqual(person1.Gender, Gender.Male);


        }

        [Test]
        public void GetPersons()
        {
            var persons = personRepository.GetPersons();
            Assert.That(persons, !Is.Null);
            Assert.IsNotEmpty(persons);
            Assert.IsInstanceOf(typeof(List<Person>), persons);
        }
    }
}
