using System.Collections.Generic;
using DataLayer.Components;
using DataLayer.Wrapper;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class SkillRepositoryTest
    {
        private IContextManager contextManager;
        private ISkillRepository skillRepository;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            skillRepository = new SkillRepository(contextManager);
        }

        [Test]
        public void CreateSkill()
        {
            var skill = new Skill { Development = "Java", Certification = "Ololowa" };
            var skill1 = new Skill { Development = "Useless", Certification = "Ahahah", Degree = Degree.Competent };
            skillRepository.Create(skill);
            skillRepository.Create(skill1);
            contextManager.BatchSave();

            Assert.That(skillRepository.GetSkillById(skill.Id), !Is.Null);
            Assert.That(skill.Id, !Is.NaN);
            Assert.That(skill.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), skill.Id);

            Assert.That(skillRepository.GetSkillById(skill1.Id), !Is.Null);
            Assert.That(skill1.Id, !Is.NaN);
            Assert.That(skill1.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), skill1.Id);

        }

        [Test]
        public void UpdateSkill()
        {
            var skill = skillRepository.GetSkillById(4);
            var skill1 = skillRepository.GetSkillByDevelopment("Junior .Net");
            Assert.That(skill, !Is.Null);
            Assert.That(skill1, !Is.Null);

            skill.Degree = Degree.Master;
            skill1.Certification = "lalalala";

            skillRepository.Update(skill);
            skillRepository.Update(skill1);
            contextManager.BatchSave();

            Assert.AreEqual(skillRepository.GetSkillByDegree(skill.Degree).Degree, Degree.Master);
            StringAssert.Contains(skillRepository.GetSkillByCertification("lalalala").Certification, "lalalala");

        }

        [Test]
        public void DeleteSkill()
        {
            var skillTodel = skillRepository.GetSkillByDegree(Degree.Competent);
            var skillTodel1 = skillRepository.GetSkillById(2);
            Assert.That(skillTodel, !Is.Null);
            Assert.That(skillTodel1, !Is.Null);

            Assert.IsTrue(skillRepository.Delete(skillTodel), "Something go wrong");
            Assert.IsTrue(skillRepository.Delete(skillTodel1), "skillRepository.Delete(skillTodel1)");
            contextManager.BatchSave();

        }

        [Test]
        public void GetSkillById()
        {
            var skill = skillRepository.GetSkillById(3);
            Assert.That(skill, !Is.Null);
            Assert.AreEqual(skill.Id, 3);
            StringAssert.Contains(skill.Development, "Junior .Net");

            var skill1 = skillRepository.GetSkillById(9);
            Assert.That(skill1, !Is.Null);
            Assert.AreEqual(skill1.Id, 9);
            StringAssert.Contains(skill1.Development, "Java");
            StringAssert.Contains(skill1.Certification, "Ololowa");
        }

        [Test]
        public void GetSkillByDevelopment()
        {
            var skill = skillRepository.GetSkillByDevelopment("ASP.NET MVC");
            Assert.That(skill, !Is.Null);
            Assert.AreEqual(skill.Id, 8);
            StringAssert.Contains(skill.Development, "ASP.NET MVC");

            var skill1 = skillRepository.GetSkillByDevelopment("Java");
            Assert.That(skill1, !Is.Null);
            Assert.AreEqual(skill1.Id, 9);
            StringAssert.Contains(skill1.Development, "Java");
            StringAssert.Contains(skill1.Certification, "Ololowa");
        }

        [Test]
        public void GetSkillByCertification()
        {
            var skill = skillRepository.GetSkillByCertification("Ololowa");
            Assert.That(skill, !Is.Null);
            Assert.AreEqual(skill.Id, 9);
            StringAssert.Contains(skill.Development, "Java");
            StringAssert.Contains(skill.Certification, "Ololowa");

            var skill1 = skillRepository.GetSkillByCertification("Ahahah");
            Assert.That(skill1, !Is.Null);
            Assert.AreEqual(skill1.Id, 10);
            StringAssert.Contains(skill1.Development, "Useless");
            StringAssert.Contains(skill1.Certification, "Ahahah");
        }

        [Test]
        public void GetSkillByDegree()
        {
            var skill = skillRepository.GetSkillByDegree(Degree.Competent);
            Assert.That(skill, !Is.Null);
            Assert.AreEqual(skill.Id, 3);
            Assert.AreEqual(skill.Degree, Degree.Competent);
            StringAssert.Contains(skill.Development, "Junior .Net");

            var skill1 = skillRepository.GetSkillByDegree(Degree.Master);
            Assert.That(skill1, !Is.Null);
            Assert.AreEqual(skill1.Id, 4);
            Assert.AreEqual(skill1.Degree, Degree.Master);
            StringAssert.Contains(skill1.Development, "Mid .Net");

        }

        [Test]
        public void GetSkills()
        {
            var skills = skillRepository.GetSkills();
            Assert.That(skills, !Is.Null);
            Assert.IsNotEmpty(skills);
            Assert.IsInstanceOf(typeof(List<Skill>), skills);
            CollectionAssert.AllItemsAreInstancesOfType(skills, typeof(Skill));
        }
    }
}
