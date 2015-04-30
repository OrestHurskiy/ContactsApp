using Models.Entities;

namespace DataLayer.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        void CreateRole(Person person, Project project,string rolename);
        void AttachRole(Person person, Project project,string rolename);
        bool DeleteRole(Person person, Project project);

    }
}
