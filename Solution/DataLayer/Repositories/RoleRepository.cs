using System.Data.Entity;
using System.Linq;
using DataLayer.Components;

namespace DataLayer.Wrapper
{
    public class RoleRepository:IRoleRepository
    {
        private readonly IContextManager contextManager;

        public RoleRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        public void CreateRole(Person person, Project project,string rolename)
        {
            if (person == null || project == null) return;
            var context = contextManager.CurrentContext;
            context.Persons.Add(person);
            context.Projects.Add(project);
            var role = new Roles {Person = person, Project = project, Role = rolename};
            context.Roles.Add(role);
            contextManager.Save(context);

        }
        public void AttachRole(Person person,Project project,string rolename)
        {
            if (person == null || project == null) return;
            var context = contextManager.CurrentContext;
            context.Entry(person).State = EntityState.Modified;
            context.Entry(project).State = EntityState.Modified;
            var role = new Roles {Person = person, Project = project, Role = rolename};
            context.Roles.Add(role);
            contextManager.Save(context);
        }

        public bool DeleteRole(Person person, Project project)
        {
            if (person == null || project == null) return false;
            var context = contextManager.CurrentContext;
            context.Entry(person).State = EntityState.Modified;
            context.Entry(project).State = EntityState.Modified;
            var role = new Roles {PersonId = person.Id, ProjectId = project.Id};
            context.Entry(role).State = EntityState.Deleted;
            contextManager.Save(context);
            return true;
        }

    }
}
