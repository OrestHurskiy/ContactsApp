using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;
namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class ProjectRepositorySingleSubmitTest
    {
        private IContextManager contextManager;
        private IProjectRepository projectRepository;
        private Project projectToCreate;
        private Project projectToUpdate;
        private Project projectToDelete;
        private Project projectToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            projectRepository = new ProjectRepository(contextManager);

            projectToCreate = new Project {NumberOfEmployers = 10, ProjectName = "ContactsApp"};
            projectToUpdate = new Project { NumberOfEmployers = 5, ProjectName = "DesktopApp" };
            projectToDelete = new Project { NumberOfEmployers = 10, ProjectName = "ContactsApp" };
            projectToGet = new Project { NumberOfEmployers = 1, ProjectName = "Trainne" };

            projectRepository.Create(projectToUpdate);
            projectRepository.Create(projectToGet);
        }

        [TearDown]
        public void TearDown()
        {
            projectRepository.Delete(projectToUpdate);
            projectRepository.Delete(projectToGet);
        }

        [Test]
        public void InsertProject_ToDatabase_PerRequest_Success()
        {
            projectRepository.Create(projectToCreate);

            Assert.IsNotNull(projectRepository.GetProjectById(projectToCreate.Id));

            projectRepository.Delete(projectToCreate);
        }

        [Test]
        public void UpdateProject_FromDatabase_PerRequest_Success()
        {
            projectToUpdate.NumberOfEmployers = 10;
            projectToUpdate.ProjectName = "Trainee";
            projectRepository.Update(projectToUpdate);

            var updated = projectRepository.GetProjectById(projectToUpdate.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual(10, updated.NumberOfEmployers);
            Assert.AreEqual("Trainee", updated.ProjectName);
        }

        [Test]
        public void DeleteProject_FromDatabase_PerRequest_Success()
        {
            projectRepository.Create(projectToDelete);

            Assert.IsTrue(projectRepository.Delete(projectToDelete));

            Assert.IsNull(projectRepository.GetProjectById(projectToDelete.Id));
        }

        [Test]
        public void GetProjectById_FromDatabase_Success()
        {
            var project = projectRepository.GetProjectById(projectToGet.Id);
            Assert.IsNotNull(project);
            Assert.AreEqual(1, project.NumberOfEmployers);
            Assert.AreEqual("Trainne", project.ProjectName);

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
            Assert.IsInstanceOf(typeof (List<Project>), projects);
        }
    }
}
