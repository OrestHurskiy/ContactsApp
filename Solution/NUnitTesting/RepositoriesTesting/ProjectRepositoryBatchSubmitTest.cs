using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class ProjectRepositoryBatchSubmitTest
    {
        private IContextManager contextManager;
        private IProjectRepository projectRepository;
        private Project projectToCreate;
        private Project projectToCreate1;
        private Project projectToUpdate;
        private Project projectToUpdate1;
        private Project projectToDelete;
        private Project projectToDelete1;
        private Project projectToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            projectRepository = new ProjectRepository(contextManager);

            projectToCreate = new Project {NumberOfEmployers = 10, ProjectName = "ContactsApp"};
            projectToCreate1 = new Project { NumberOfEmployers = 20, ProjectName = "WebApp" };
            projectToUpdate = new Project {NumberOfEmployers = 5, ProjectName = "DesktopApp"};
            projectToUpdate1 = new Project {NumberOfEmployers = 30, ProjectName = "Medical Web site"};
            projectToDelete = new Project {NumberOfEmployers = 10, ProjectName = "ContactsApp"};
            projectToDelete1 = new Project {NumberOfEmployers = 20, ProjectName = "WebApp"};
            projectToGet = new Project {NumberOfEmployers = 1, ProjectName = "Trainne"};

            projectRepository.Create(projectToUpdate);
            projectRepository.Create(projectToUpdate1);
            projectRepository.Create(projectToGet);
            contextManager.BatchSave();

        }

        [TearDown]
        public void TearDown()
        {
            projectRepository.Delete(projectToUpdate);
            projectRepository.Delete(projectToUpdate1);
            projectRepository.Delete(projectToGet);
            contextManager.BatchSave();
        }

        [Test]
        public void InsertProject_ToDatabase_InBatchMode_Success()
        {
            projectRepository.Create(projectToCreate);
            projectRepository.Create(projectToCreate1);
            contextManager.BatchSave();

            Assert.IsNotNull(projectRepository.GetProjectById(projectToCreate.Id));
            Assert.IsNotNull(projectRepository.GetProjectById(projectToCreate1.Id));

            projectRepository.Delete(projectToCreate);
            projectRepository.Delete(projectToCreate1);

        }

        [Test]
        public void UpdateProject_FromDatabase_InBatchMode_Success()
        {
            projectToUpdate.NumberOfEmployers = 10;
            projectToUpdate.ProjectName = "Trainee";
            projectToUpdate1.NumberOfEmployers = 7;
            projectToUpdate1.ProjectName = "Trainne1";
            projectRepository.Update(projectToUpdate);
            projectRepository.Update(projectToUpdate1);
            contextManager.BatchSave();

            var updated = projectRepository.GetProjectById(projectToUpdate.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual(10,updated.NumberOfEmployers);
            Assert.AreEqual("Trainee",updated.ProjectName);

            var updated1 = projectRepository.GetProjectById(projectToUpdate1.Id);
            Assert.IsNotNull(updated1);
            Assert.AreEqual(7,updated1.NumberOfEmployers);
            Assert.AreEqual("Trainne1",updated1.ProjectName);
        }

        [Test]
        public void DeleteProject_FromDatabase_InBatchMode_Success()
        {
            projectRepository.Create(projectToDelete);
            projectRepository.Create(projectToDelete1);
            contextManager.BatchSave();

            Assert.IsTrue(projectRepository.Delete(projectToDelete));
            Assert.IsTrue(projectRepository.Delete(projectToDelete1));
            contextManager.BatchSave();

            Assert.IsNull(projectRepository.GetProjectById(projectToDelete.Id));
            Assert.IsNull(projectRepository.GetProjectById(projectToDelete1.Id));
        }

        [Test]
        public void GetProjectById_FromDatabase_Success()
        {
            var project = projectRepository.GetProjectById(projectToGet.Id);
            Assert.IsNotNull(project);
            Assert.AreEqual(1,project.NumberOfEmployers);
            Assert.AreEqual("Trainne",project.ProjectName);

        }

        [Test]
        public void GetProjectByName_FromDatabaseSuccess()
        {
            var project = projectRepository.GetProjectByName(projectToGet.ProjectName);
            Assert.IsNotNull(project);
            Assert.AreEqual(1, project.NumberOfEmployers);
            Assert.AreEqual("Trainne", project.ProjectName);
        }

        [Test]
        public void GetProjectByNumberOfEmployers_FromDatabase_Success()
        {
            var project = projectRepository.GetProjectByNumberOfEmployers(projectToGet.NumberOfEmployers);
            Assert.IsNotNull(project);
            Assert.AreEqual(1, project.NumberOfEmployers);
            Assert.AreEqual("Trainne", project.ProjectName);
        }

        [Test]
        public void GetProjects_FromDatabase_Success()
        {
            var projects = projectRepository.GetProjects();
            Assert.IsNotNull(projects);
            Assert.IsNotEmpty(projects);
            Assert.IsInstanceOf(typeof(List<Project>), projects);
        }
    }
}
