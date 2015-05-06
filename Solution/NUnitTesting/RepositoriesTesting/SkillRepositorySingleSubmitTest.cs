using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;
namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class SkillRepositorySingleSubmitTest
    {
        private IContextManager contextManager;
        private ISkillRepository skillRepository;
        private Skill skillToCreate;
        private Skill skillToUpdate;
        private Skill skillToDelete;
        private Skill skillToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            skillRepository = new SkillRepository(contextManager);

            skillToCreate = new Skill
            {
                Certification = "Mircosoft",
                Development = ".Net",
                Degree = Degree.Competent
            };

            skillToUpdate = new Skill
            {
                Certification = "Nothing",
                Development = "PHP",
                Degree = Degree.Master
            };

            skillToDelete = new Skill
            {
                Certification = "Mircosoft",
                Development = ".Net",
                Degree = Degree.Competent
            };

            skillToGet = new Skill
            {
                Certification = "Some Certification",
                Development = ".Net",
                Degree = Degree.Professor
            };

            skillRepository.Create(skillToUpdate);
            skillRepository.Create(skillToGet);
        }

        [TearDown]
        public void TearDown()
        {
            skillRepository.Delete(skillToUpdate);
            skillRepository.Delete(skillToGet);
        }

        [Test]
        public void InsertSkill_ToDatabase_PerRequest_Success()
        {
            skillRepository.Create(skillToCreate);

            Assert.IsNotNull(skillRepository.GetSkillById(skillToCreate.Id));

            skillRepository.Delete(skillToCreate);
        }

        [Test]
        public void UpdateSkill_FromDatabase_PerRequest_Success()
        {
            skillToUpdate.Certification = "Certification";
            skillToUpdate.Development = ".Net";
            skillToUpdate.Degree = Degree.Competent;
            skillRepository.Update(skillToUpdate);

            var updated = skillRepository.GetSkillById(skillToUpdate.Id);
            Assert.AreEqual("Certification", updated.Certification);
            Assert.AreEqual(".Net", updated.Development);
            Assert.AreEqual(Degree.Competent, updated.Degree);
        }

        [Test]
        public void DeleteSkill_FromDatabase_RepRequest_Success()
        {
            skillRepository.Create(skillToDelete);

            Assert.IsTrue(skillRepository.Delete(skillToDelete));

            Assert.IsNull(skillRepository.GetSkillById(skillToDelete.Id));
        }

        public void GetSkillById_FromDatabase_Success()
        {
            var skill = skillRepository.GetSkillById(skillToGet.Id);
            Assert.IsNotNull(skill);
            Assert.AreEqual("Some Certification", skill.Certification);
            Assert.AreEqual(".Net", skill.Development);
            Assert.AreEqual(Degree.Professor, skill.Degree);
        }

        [Test]
        public void GetSkillByDevelopment_FromDatabase_Success()
        {
            var skill = skillRepository.GetSkillByDevelopment(skillToGet.Development);
            Assert.IsNotNull(skill);
            Assert.AreEqual("Some Certification", skill.Certification);
            Assert.AreEqual(".Net", skill.Development);
            Assert.AreEqual(Degree.Professor, skill.Degree);
        }

        [Test]
        public void GetSkillByCertification_FromDatabase_Success()
        {
            var skill = skillRepository.GetSkillByCertification(skillToGet.Certification);
            Assert.IsNotNull(skill);
            Assert.AreEqual("Some Certification", skill.Certification);
            Assert.AreEqual(".Net", skill.Development);
            Assert.AreEqual(Degree.Professor, skill.Degree);
        }

        [Test]
        public void GetSkillByDegree_FromDatabase_Success()
        {
            var skill = skillRepository.GetSkillByDegree(skillToGet.Degree);
            Assert.IsNotNull(skill);
            Assert.AreEqual("Some Certification", skill.Certification);
            Assert.AreEqual(".Net", skill.Development);
            Assert.AreEqual(Degree.Professor, skill.Degree);
        }

        [Test]
        public void GetSkills_FromDatabase_Success()
        {
            var skills = skillRepository.GetSkills();
            Assert.IsNotNull(skills);
            Assert.IsNotEmpty(skills);
            Assert.IsInstanceOf(typeof(List<Skill>), skills);
        }
    }
}
