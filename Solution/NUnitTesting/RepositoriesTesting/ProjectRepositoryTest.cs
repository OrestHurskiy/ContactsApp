using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class ProjectRepositoryTest
    {
        private IContextManager contextManager;
        private IProjectRepository projectRepository;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            projectRepository = new ProjectRepository(contextManager);
        }

        [Test]
        public void CreateProject()
        {
            var project = new Project { ProjectName = "Project1", NumberOfEmployers = 10 };
            var project1 = new Project { ProjectName = "Project2", NumberOfEmployers = 20 };
            projectRepository.Create(project);
            projectRepository.Create(project1);
            contextManager.BatchSave();

            Assert.That(projectRepository.GetProjectById(project.Id), !Is.Null);
            Assert.That(project.Id, !Is.NaN);
            Assert.That(project.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), project.Id);

            Assert.That(projectRepository.GetProjectById(project1.Id), !Is.Null);
            Assert.That(project1.Id, !Is.NaN);
            Assert.That(project1.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), project1.Id);

        }

        [Test]
        public void UpdateProject()
        {
            var projectUp = projectRepository.GetProjectById(1);
            var projectUp1 = projectRepository.GetProjectByName("Desktop");
            Assert.That(projectUp, !Is.Null);
            Assert.That(projectUp1, !Is.Null);

            projectUp.ProjectName = "WebApp";
            projectUp1.NumberOfEmployers = 10;

            projectRepository.Update(projectUp);
            projectRepository.Update(projectUp1);
            contextManager.BatchSave();

            StringAssert.Contains(projectRepository.GetProjectByName("WebApp").ProjectName, "WebApp");
            Assert.AreEqual(projectRepository.GetProjectByNumberOfEmployers(10).NumberOfEmployers, 10);
        }

        [Test]
        public void DeleteProject()
        {
            var projectTodel = projectRepository.GetProjectById(14);
            var projectTodel1 = projectRepository.GetProjectByNumberOfEmployers(20);
            Assert.That(projectTodel, !Is.Null);
            Assert.That(projectTodel1, !Is.Null);

            Assert.IsTrue(projectRepository.Delete(projectTodel), "Something go wrong");
            Assert.IsTrue(projectRepository.Delete(projectTodel1), "Something go wrong");
            contextManager.BatchSave();

            Assert.That(projectRepository.GetProjectByName(projectTodel.ProjectName), Is.Null);
            Assert.That(projectRepository.GetProjectByName(projectTodel1.ProjectName), Is.Null);
        }

        [Test]
        public void GetProjectById()
        {
            var project = projectRepository.GetProjectById(1);
            Assert.That(project, !Is.Null);
            Assert.AreEqual(project.Id, 1);
            StringAssert.Contains(project.ProjectName, "WebApp");
            Assert.AreEqual(project.NumberOfEmployers, 0);

            var project1 = projectRepository.GetProjectById(2);
            Assert.That(project1, !Is.Null);
            Assert.AreEqual(project1.Id, 2);
            StringAssert.Contains(project1.ProjectName, "Desktop");
            Assert.AreEqual(project1.NumberOfEmployers, 10);
        }

        [Test]
        public void GetProjectByName()
        {
            var project = projectRepository.GetProjectByName("WebApp");
            Assert.That(project, !Is.Null);
            Assert.AreEqual(project.Id, 1);
            StringAssert.Contains(project.ProjectName, "WebApp");
            Assert.AreEqual(project.NumberOfEmployers, 0);

            var project1 = projectRepository.GetProjectByName("Desktop");
            Assert.That(project1, !Is.Null);
            Assert.AreEqual(project1.Id, 2);
            StringAssert.Contains(project1.ProjectName, "Desktop");
            Assert.AreEqual(project1.NumberOfEmployers, 10);
        }

        [Test]
        public void GetProjectByNumberOfEmployers()
        {
            var project = projectRepository.GetProjectByNumberOfEmployers(0);
            Assert.That(project, !Is.Null);
            Assert.AreEqual(project.Id, 1);
            StringAssert.Contains(project.ProjectName, "WebApp");
            Assert.AreEqual(project.NumberOfEmployers, 0);

            var project1 = projectRepository.GetProjectByNumberOfEmployers(10);
            Assert.That(project1, !Is.Null);
            Assert.AreEqual(project1.Id, 2);
            StringAssert.Contains(project1.ProjectName, "Desktop");
            Assert.AreEqual(project1.NumberOfEmployers, 10);
        }

        [Test]
        public void GetProjects()
        {
            var projects = projectRepository.GetProjects();
            Assert.That(projects, !Is.Null);
            Assert.IsNotEmpty(projects);
            Assert.IsInstanceOf(typeof(List<Project>), projects);
        }
    }
}
