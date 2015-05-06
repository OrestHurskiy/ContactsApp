using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class RoleRepositorySingleSubmitTest
    {
        private IContextManager contextManager;
        private IRoleRepository roleRepository;
        private IPersonRepository personRepository;
        private IProjectRepository projectRepository;
        private Person personToCreate;
        private Project projectToCreate;
        private Person personToAttach;
        private Project projectToAttach;
        private Person personToDelete;
        private Project projectToDelete;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
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
           
        }


        [Test]
        public void InsertRole_ToDatabase_PerRequest_Success()
        {
            var rolename = "Developer";
            roleRepository.CreateRole(personToCreate,projectToCreate,rolename);

            Assert.IsNotNull(personRepository.GetPersonById(personToCreate.Id));
            Assert.IsNotNull(projectRepository.GetProjectById(projectToCreate.Id));

            var person = personRepository.GetPersonById(personToCreate.Id);
            var project = projectRepository.GetProjectById(projectToCreate.Id);
           
            roleRepository.DeleteRole(person, project);
            personRepository.Delete(person);
            projectRepository.Delete(project);
           

        }

        [Test]
        public void AttachRole_FromDatabase_PerRequest_Success()
        {
            personRepository.Create(personToAttach);
            projectRepository.Create(projectToAttach);
            var rolename = "VVVVV";
            roleRepository.AttachRole(personToAttach,projectToAttach,rolename);

            var person = personRepository.GetPersonById(personToAttach.Id);
            var project = projectRepository.GetProjectById(projectToAttach.Id);
            Assert.IsNotNull(person);
            Assert.IsNotNull(project);
            roleRepository.DeleteRole(person, project);
            personRepository.Delete(person);
            projectRepository.Delete(project);
        }

        [Test]
        public void DeleteRole_FromDatabase_PerRequest_Success()
        {
            var rolename = "Developer";
            roleRepository.CreateRole(personToDelete, projectToDelete, rolename);

            var person = personRepository.GetPersonById(personToDelete.Id);
            var project = projectRepository.GetProjectById(projectToDelete.Id);
            Assert.IsNotNull(person);
            Assert.IsNotNull(project);
            Assert.IsTrue(roleRepository.DeleteRole(person, project));
            Assert.IsTrue(personRepository.Delete(person));
            Assert.IsTrue(projectRepository.Delete(project));
      
        }
    }
}
