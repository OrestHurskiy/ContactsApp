using System.Collections.Generic;
using Models.Entities;

namespace DataLayer.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        void Create(Project project);
        void Update(Project project);
        bool Delete(Project project);
        Project GetProjectById(int projectId);
        Project GetProjectByName(string projectname);
        Project GetProjectByNumberOfEmployers(int numberofemloyers);
        IList<Project> GetProjects();
    }
}
