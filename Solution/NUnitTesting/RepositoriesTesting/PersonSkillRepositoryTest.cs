using System;
using System.Linq;
using DataLayer.Components;
using DataLayer.Wrapper;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    class PersonSkillRepositoryTest
    {
        private IContextManager contextManager;
        private IPersonSkillRepository personSkillRepository;
        private ISkillRepository skillRepository;
        public IPersonRepository personRepository;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            personSkillRepository = new PersonSkillRepository(contextManager);
            skillRepository = new SkillRepository(contextManager);
            personRepository = new PersonRepository(contextManager);
        }

        [Test]
        public void CreatePersonWithSkill()
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

            var skill = new Skill { Development = "Ruby", Certification = "Nice one", Degree = Degree.Competent };

            personSkillRepository.CreatePersonWithSkill(person, skill);
            contextManager.BatchSave();

            Assert.That(personRepository.GetPersonById(person.Id), !Is.Null);
            Assert.IsInstanceOf(typeof(int), person.Id);
            Assert.That(person.Id, !Is.NaN);
            Assert.That(person.Id, Is.Positive);

            Assert.That(skillRepository.GetSkillById(skill.Id), !Is.Null);
            Assert.IsInstanceOf(typeof(int), skill.Id);
            Assert.That(skill.Id, !Is.NaN);
            Assert.That(skill.Id, Is.Positive);
        }

        [Test]
        public void AttachPersonToSkill()
        {
            var person = personRepository.GetPersonById(1);
            var skill = skillRepository.GetSkillById(3);
            Assert.That(person, !Is.Null);
            Assert.That(skill, !Is.Null);

            personSkillRepository.AttachPersonToSkill(person, skill);
            contextManager.BatchSave();
            var id = personRepository.GetPersonById(1).Skills.FirstOrDefault(s => s.Id == 3).Id;
            Assert.IsTrue(id == 3);
        }

        [Test]
        public void DeletePersonWithSkill()
        {
            var person = personRepository.GetPersonById(1);
            var skill = skillRepository.GetSkillById(3);
            Assert.That(person, !Is.Null);
            Assert.That(skill, !Is.Null);

            Assert.IsTrue(personSkillRepository.DeletePersonWithSkill(person, skill), "personSkillRepository.DeletePersonWithSkill(person, skill)");
            contextManager.BatchSave();
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
