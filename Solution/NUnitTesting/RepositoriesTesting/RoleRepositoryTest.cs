using DataLayer.Components;
using DataLayer.Wrapper;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class RoleRepositoryTest
    {
        private IContextManager contextManager;
        private IRoleRepository roleRepository;
        private IPersonRepository personRepository;
        private IProjectRepository projectRepository;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            roleRepository = new RoleRepository(contextManager);
            personRepository = new PersonRepository(contextManager);
            projectRepository = new ProjectRepository(contextManager);
        }

        [Test]
        public void CreateRole()
        {
            var person = new Person
            {
                FirstName = "Person1",
                LastName = "PLN1",
                Adress = "Kyiv",
                Phone = "12-121",
                Skype = "Ololowa"
            };

            var project = new Project {ProjectName = "App1", NumberOfEmployers = 5};
            var rolename = "Useless";

            roleRepository.CreateRole(person,project,rolename);
            contextManager.BatchSave();

            Assert.That(personRepository.GetPersonById(person.Id),!Is.Null);
            Assert.That(person.Id, !Is.NaN);
            Assert.That(person.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), person.Id);

            Assert.That(projectRepository.GetProjectById(project.Id), !Is.Null);
            Assert.That(project.Id, !Is.NaN);
            Assert.That(project.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), project.Id);

        }

        [Test]
        public void Attach()
        {
            var person = personRepository.GetPersonById(2);
            var project = projectRepository.GetProjectById(4);
            var rolename = "Junior .Net";
            Assert.IsNotNull(person);
            Assert.IsNotNull(project);

            roleRepository.AttachRole(person,project,rolename);

        }

        [Test]
        public void Delete()
        {
            var person = personRepository.GetPersonById(11);
            var project = projectRepository.GetProjectById(5);
            Assert.IsNotNull(person);
            Assert.IsNotNull(project);
            Assert.IsTrue(roleRepository.DeleteRole(person, project));
        }
    }
}
