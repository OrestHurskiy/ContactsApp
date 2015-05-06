using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;
namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class RoleRepositoryBatchSubmitTest
    {
        private IContextManager contextManager;
        private IRoleRepository roleRepository;
        private IPersonRepository personRepository;
        private IProjectRepository projectRepository;
        private Person personToCreate;
        private Project projectToCreate;
        private Person personToCreate1;
        private Project projectToCreate1;
        private Person personToAttach;
        private Project projectToAttach;
        private Person personToAttach1;
        private Project projectToAttach1;
        private Person personToDelete;
        private Project projectToDelete;
        private Person personToDelete1;
        private Project projectToDelete1;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            roleRepository = new RoleRepository(contextManager);
            personRepository = new PersonRepository(contextManager);
            projectRepository = new ProjectRepository(contextManager);

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

            projectToCreate = new Project
            {
                NumberOfEmployers = 10,
                ProjectName = "ContactsApp"
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

            projectToCreate1 = new Project
            {
                NumberOfEmployers = 20,
                ProjectName = "WebApp"
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

            projectToAttach = new Project
            {
                NumberOfEmployers = 5,
                ProjectName = "DesktopApp"
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

            projectToAttach1 = new Project
            {
                NumberOfEmployers = 30, 
                ProjectName = "Medical Web site"
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

            projectToDelete = new Project
            {
                NumberOfEmployers = 10,
                ProjectName = "ContactsApp"
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

            projectToDelete1 = new Project
            {
                NumberOfEmployers = 20, 
                ProjectName = "WebApp"
            };

        }


        [Test]
        public void InsertRole_ToDatabase_InBatchMode_Success()
        {
            var rolename = "Developer";
            roleRepository.CreateRole(personToCreate, projectToCreate, rolename);
            contextManager.BatchSave();

            Assert.IsNotNull(personRepository.GetPersonById(personToCreate.Id));
            Assert.IsNotNull(projectRepository.GetProjectById(projectToCreate.Id));
          
            var person = personRepository.GetPersonById(personToCreate.Id);
            var project = projectRepository.GetProjectById(projectToCreate.Id);
         
            roleRepository.DeleteRole(person, project);
            personRepository.Delete(person);
            projectRepository.Delete(project);
            contextManager.BatchSave();


        }

        [Test]
        public void AttachRole_FromDatabase_InBatchMode_Success()
        {
            personRepository.Create(personToAttach);
            projectRepository.Create(projectToAttach);
            contextManager.BatchSave();

            var rolename = "VVVVV";
            roleRepository.AttachRole(personToAttach, projectToAttach, rolename);
            contextManager.BatchSave();

            var person = personRepository.GetPersonById(personToAttach.Id);
            var project = projectRepository.GetProjectById(projectToAttach.Id);
            Assert.IsNotNull(person);
            Assert.IsNotNull(project);
            roleRepository.DeleteRole(person, project);
            personRepository.Delete(person);
            projectRepository.Delete(project);
            contextManager.BatchSave();
        }

        [Test]
        public void DeleteRole_FromDatabase_InBatchMode_Success()
        {
            var rolename = "Developer";
            roleRepository.CreateRole(personToDelete, projectToDelete, rolename);
            contextManager.BatchSave();

            var person = personRepository.GetPersonById(personToDelete.Id);
            var project = projectRepository.GetProjectById(projectToDelete.Id);
            Assert.IsNotNull(person);
            Assert.IsNotNull(project);
            Assert.IsTrue(roleRepository.DeleteRole(person, project));
            Assert.IsTrue(personRepository.Delete(person));
            Assert.IsTrue(projectRepository.Delete(project));
            contextManager.BatchSave();

        }
    }
}
