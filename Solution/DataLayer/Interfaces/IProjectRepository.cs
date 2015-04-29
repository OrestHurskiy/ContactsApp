using System.Collections;
using System.Collections.Generic;
using DataLayer.Components;

namespace DataLayer.Wrapper
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
