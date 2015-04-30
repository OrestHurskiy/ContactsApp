using DataLayer.Components;

namespace DataLayer.Wrapper
{
    public interface IRoleRepository
    {
        void CreateRole(Person person, Project project,string rolename);
        void AttachRole(Person person, Project project,string rolename);
        bool DeleteRole(Person person, Project project);

    }
}
