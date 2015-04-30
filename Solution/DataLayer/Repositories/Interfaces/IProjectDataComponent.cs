#region UsedNamespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Components;

#endregion

namespace DataLayer.Interfaces
{
    #region ProjectDataComponent Interface

    public interface IProjectDataComponent
    {
        #region IProjectDataComponent Methods

        Project CreateProject(Project project);
        Project UpdateProject(Project project);
        bool DeleteProject(Project project);
        Project GetProjectById(int id);
        Project GeProjectByName(string name);
        Project GetProjectByEmployersNumber(int number);
        

        #endregion
    }

        #endregion
}
