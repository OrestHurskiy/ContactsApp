using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Components;

namespace DataLayer.Wrapper
{
    public class ProjectRepository:IProjectRepository
    {
        private readonly IContextManager contextManager;

        public ProjectRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        public void Create(Project project)
        {
            if (project == null) return;
            var context = contextManager.CurrentContext;
            context.Projects.Add(project);
            contextManager.Save(context);
        }

        public void Update(Project project)
        {
            if (project == null) return;
            var context = contextManager.CurrentContext;
            context.Entry(project).State = EntityState.Modified;
            contextManager.Save(context);
        }

        public bool Delete(Project project)
        {
            if (project == null) return false;
            var context = contextManager.CurrentContext;
            context.Entry(project).State = EntityState.Deleted;
            contextManager.Save(context);
            return true;
        }

        public Project GetProjectById(int projectId)
        {
            return contextManager.CurrentContext.Projects.AsNoTracking().FirstOrDefault(pr => pr.Id == projectId);
        }

        public Project GetProjectByName(string projectname)
        {
            return contextManager.CurrentContext.Projects.AsNoTracking().FirstOrDefault(pr=>pr.ProjectName==projectname);
        }

        public Project GetProjectByNumberOfEmployers(int numberofemloyers)
        {
            return contextManager.CurrentContext.Projects.AsNoTracking().FirstOrDefault(pr=>pr.NumberOfEmployers==numberofemloyers);
        }

        public IList<Project> GetProjects()
        {
            return contextManager.CurrentContext.Projects.AsNoTracking().ToList();
        }
    }
}
