using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;
namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class PersonSkillRepositorySingleSubmitTest
    {
        private IContextManager contextManager;
        private IPersonSkillRepository personSkillRepository;
        private ISkillRepository skillRepository;
        private IPersonRepository personRepository;
        private Person personToCreate;
        private Skill skillToCreate;
        private Person personToAttach;
        private Skill skillToAttach;
        private Person personToDelete;
        private Skill skillToDelete;
        private Person personToGet;
        private Skill skillToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
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
        }

        [TearDown]
        public void TearDown()
        {
            personRepository.Delete(personToAttach);
            skillRepository.Delete(skillToAttach);
        }

        [Test]
        public void InsertPersonWithSkill_ToDatabase_PerRequest_Success()
        {
            personSkillRepository.CreatePersonWithSkill(personToCreate, skillToCreate);

            Assert.IsNotNull(personRepository.GetPersonById(personToCreate.Id));
            Assert.IsNotNull(skillRepository.GetSkillById(skillToCreate.Id));

            personRepository.Delete(personToCreate);
            skillRepository.Delete(skillToCreate);
        }

        [Test]
        public void AttachPersonToSkill_FromDatabase_PerRequest_Success()
        {
            personSkillRepository.AttachPersonToSkill(personToAttach,skillToAttach);

            Assert.IsTrue(personToAttach.Skills.Contains(skillToAttach));
            Assert.IsTrue(skillToAttach.Persons.Contains(personToAttach));

            personSkillRepository.DeletePersonWithSkill(personToAttach, skillToAttach);
        }

        [Test]
        public void DeletePersonWithSkill_FromDatabase_PerRequest_Success()
        {
            personSkillRepository.CreatePersonWithSkill(personToDelete,skillToDelete);

            Assert.IsTrue(personSkillRepository.DeletePersonWithSkill(personToDelete, skillToDelete));

            personRepository.Delete(personToDelete);
            skillRepository.Delete(skillToDelete);
        }
    }
}
