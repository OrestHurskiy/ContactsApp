using System.Collections.Generic;

namespace Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int NumberOfEmployers { get; set; }
        public Customer Customer { get; set; }

        public Project()
        {
            Roles = new List<Roles>();
        }
        public virtual List<Roles> Roles { get; set; }
    }
}
