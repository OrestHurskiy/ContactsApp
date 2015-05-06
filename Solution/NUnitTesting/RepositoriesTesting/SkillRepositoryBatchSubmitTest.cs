using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class SkillRepositoryBatchSubmitTest
    {
        private IContextManager contextManager;
        private ISkillRepository skillRepository;
        private Skill skillToCreate;
        private Skill skillToCreate1;
        private Skill skillToUpdate;
        private Skill skillToUpdate1;
        private Skill skillToDelete;
        private Skill skillToDelete1;
        private Skill skillToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            skillRepository = new SkillRepository(contextManager);

            skillToCreate = new Skill
            {
                Certification = "Mircosoft",
                Development = ".Net",
                Degree = Degree.Competent
            };

            skillToCreate1 = new Skill
            {
                Certification = "Useless",
                Development = "Java",
                Degree = Degree.Competent
            };

            skillToUpdate = new Skill
            {
                Certification = "Nothing",
                Development = "PHP",
                Degree = Degree.Master
            };

            skillToUpdate1 = new Skill
            {
                Certification = "Ololowa",
                Development = "Ruby",
                Degree = Degree.Competent
            };

            skillToDelete = new Skill
            {
                Certification = "Mircosoft",
                Development = ".Net",
                Degree = Degree.Competent
            };

            skillToDelete1 = new Skill
            {
                Certification = "Useless",
                Development = "Java",
                Degree = Degree.Competent
            };

            skillToGet = new Skill
            {
                Certification = "Some Certification",
                Development = ".Net",
                Degree = Degree.Professor
            };

            skillRepository.Create(skillToUpdate);
            skillRepository.Create(skillToUpdate1);
            skillRepository.Create(skillToGet);
            contextManager.BatchSave();
        }

        [TearDown]
        public void TearDown()
        {
            skillRepository.Delete(skillToUpdate);
            skillRepository.Delete(skillToUpdate1);
            skillRepository.Delete(skillToGet);
            contextManager.BatchSave();
        }

        [Test]
        public void InsertSkill_ToDatabase_InBatchMode_Success()
        {
            skillRepository.Create(skillToCreate);
            skillRepository.Create(skillToCreate1);
            contextManager.BatchSave();

            Assert.IsNotNull(skillRepository.GetSkillById(skillToCreate.Id));
            Assert.IsNotNull(skillRepository.GetSkillById(skillToCreate1.Id));

            skillRepository.Delete(skillToCreate);
            skillRepository.Delete(skillToCreate1);
        }

        [Test]
        public void UpdateSkill_FromDatabase_InBatchMode_Success()
        {
            skillToUpdate.Certification = "Certification";
            skillToUpdate.Development = ".Net";
            skillToUpdate.Degree = Degree.Competent;
            skillToUpdate1.Certification = "Certification1";
            skillToUpdate1.Development = "Java";
            skillToUpdate1.Degree = Degree.Competent;
            skillRepository.Update(skillToUpdate);
            skillRepository.Update(skillToUpdate1);
            contextManager.BatchSave();

            var updated = skillRepository.GetSkillById(skillToUpdate.Id);
            Assert.AreEqual("Certification", updated.Certification);
            Assert.AreEqual(".Net", updated.Development);
            Assert.AreEqual(Degree.Competent,updated.Degree);

            var updated1 = skillRepository.GetSkillById(skillToUpdate1.Id);
            Assert.AreEqual("Certification1", updated1.Certification);
            Assert.AreEqual("Java", updated1.Development);
            Assert.AreEqual(Degree.Competent, updated1.Degree);
        }

        [Test]
        public void DeleteSkill_FromDatabase_InBatchMode_Success()
        {
            skillRepository.Create(skillToDelete);
            skillRepository.Create(skillToDelete1);
            contextManager.BatchSave();

            Assert.IsTrue(skillRepository.Delete(skillToDelete));
            Assert.IsTrue(skillRepository.Delete(skillToDelete1));
            contextManager.BatchSave();

            Assert.IsNull(skillRepository.GetSkillById(skillToDelete.Id));
            Assert.IsNull(skillRepository.GetSkillById(skillToDelete1.Id));
        }

        [Test]
        public void GetSkillById_FromDatabase_Success()
        {
            var skill = skillRepository.GetSkillById(skillToGet.Id);
            Assert.IsNotNull(skill);
            Assert.AreEqual("Some Certification",skill.Certification);
            Assert.AreEqual(".Net",skill.Development);
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
