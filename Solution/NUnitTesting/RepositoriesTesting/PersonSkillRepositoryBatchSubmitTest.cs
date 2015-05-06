using System.Linq;
using System.Runtime.InteropServices;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    class PersonSkillRepositoryBatchSubmitTest
    {
        private IContextManager contextManager;
        private IPersonSkillRepository personSkillRepository;
        private ISkillRepository skillRepository;
        private IPersonRepository personRepository;
        private Person personToCreate;
        private Skill skillToCreate;
        private Person personToCreate1;
        private Skill skillToCreate1;
        private Person personToAttach;
        private Skill skillToAttach;
        private Person personToAttach1;
        private Skill skillToAttach1;
        private Person personToDelete;
        private Skill skillToDelete;
        private Person personToDelete1;
        private Skill skillToDelete1;
        private Person personToGet;
        private Skill skillToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            personSkillRepository = new PersonSkillRepository(contextManager);
            skillRepository = new SkillRepository(contextManager);
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

            skillToCreate = new Skill
            {
                Certification = "Mircosoft",
                Development = ".Net",
                Degree = Degree.Competent
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

            skillToCreate1 = new Skill
            {
                Certification = "Useless",
                Development = "Java",
                Degree = Degree.Competent
            };

            personToAttach = new Person
            {
                FirstName = "Ivan",
                LastName = "Ivanovych",
                Adress = "Lviv",
                Mail = "ivan@gmail.com",
                Phone = "09345353",
                Skype = "ivan",
                Gender = Gender.Male
            };

            skillToAttach = new Skill
            {
                Certification = "Nothing",
                Development = "PHP",
                Degree = Degree.Master
            };

            personToAttach1 = new Person
            {
                FirstName = "Olga",
                LastName = "Pogrognik",
                Adress = "Zhovkva",
                Mail = "olga@gmail.com",
                Phone = "09345553",
                Skype = "olga",
                Gender = Gender.Female
            };

            skillToAttach1 = new Skill
            {
                Certification = "Ololowa",
                Development = "Ruby",
                Degree = Degree.Competent
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

            skillToDelete = new Skill
            {
                Certification = "Mircosoft",
                Development = ".Net",
                Degree = Degree.Competent
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

            skillToDelete1 = new Skill
            {
                Certification = "Useless",
                Development = "Java",
                Degree = Degree.Competent
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

            skillToGet = new Skill
            {
                Certification = "Some Certification",
                Development = ".Net",
                Degree = Degree.Professor
            };

            personRepository.Create(personToAttach);
            skillRepository.Create(skillToAttach);
            personRepository.Create(personToAttach1);
            skillRepository.Create(skillToAttach1);
            contextManager.BatchSave();

            

        }

        [TearDown]
        public void TearDown()
        {
            personRepository.Delete(personToAttach);
            skillRepository.Delete(skillToAttach);
            personRepository.Delete(personToAttach1);
            skillRepository.Delete(skillToAttach1);
            contextManager.BatchSave();
        }

        [Test]
        public void InsertPersonWithSkill_ToDatabase_InBatchMode_Success()
        {
            personSkillRepository.CreatePersonWithSkill(personToCreate, skillToCreate);
            personSkillRepository.CreatePersonWithSkill(personToCreate1,skillToCreate1);
            contextManager.BatchSave();

            Assert.IsNotNull(personRepository.GetPersonById(personToCreate.Id));
            Assert.IsNotNull(skillRepository.GetSkillById(skillToCreate.Id));
            Assert.IsNotNull(personRepository.GetPersonById(personToCreate1.Id));
            Assert.IsNotNull(skillRepository.GetSkillById(skillToCreate1.Id));

            personRepository.Delete(personToCreate);
            skillRepository.Delete(skillToCreate);
            personRepository.Delete(personToCreate1);
            skillRepository.Delete(skillToCreate1);
        }

        [Test]
        public void AttachPersonToSkill_FromDatabase_InBatchMode_Success()
        {
            personSkillRepository.AttachPersonToSkill(personToAttach,skillToAttach);
            personSkillRepository.AttachPersonToSkill(personToAttach1,skillToAttach1);
            contextManager.BatchSave();

            Assert.IsTrue(personToAttach.Skills.Contains(skillToAttach));
            Assert.IsTrue(skillToAttach.Persons.Contains(personToAttach));

            personSkillRepository.DeletePersonWithSkill(personToAttach, skillToAttach);
            personSkillRepository.DeletePersonWithSkill(personToAttach1, skillToAttach1);
        }

        [Test]
        public void DeletePersonWithSkill_FromDatabase_InBatchMode_Success()
        {
            personSkillRepository.CreatePersonWithSkill(personToDelete,skillToDelete);
            personSkillRepository.CreatePersonWithSkill(personToDelete1,skillToDelete1);
            contextManager.BatchSave();

            Assert.IsTrue(personSkillRepository.DeletePersonWithSkill(personToDelete, skillToDelete));
            Assert.IsTrue(personSkillRepository.DeletePersonWithSkill(personToDelete1, skillToDelete1));

            personRepository.Delete(personToDelete);
            skillRepository.Delete(skillToDelete);
            personRepository.Delete(personToDelete1);
            skillRepository.Delete(skillToDelete1);

        }

        [Test]
        public void GetPersonsBySkillId()
        {
            var persons = personSkillRepository.GetPersonsBySkillId(8);
            Assert.That(persons, !Is.Null);
            CollectionAssert.AllItemsAreInstancesOfType(persons, typeof(Person));
            CollectionAssert.IsNotEmpty(persons);
        }

        [Test]
        public void GetSkillsByPersonId()
        {
            var skills = personSkillRepository.GetSkillsByPersonId(10);
            Assert.That(skills, !Is.Null);
            CollectionAssert.AllItemsAreInstancesOfType(skills, typeof(Skill));
            CollectionAssert.IsNotEmpty(skills);
        }

    }
}
