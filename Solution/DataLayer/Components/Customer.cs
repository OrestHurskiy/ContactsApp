using System.Collections.Generic;

namespace DataLayer.Components
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }

        public Customer()
        {
            Projects = new List<Project>();
        }
        public virtual List<Project> Projects { get; set; }
    }
}
