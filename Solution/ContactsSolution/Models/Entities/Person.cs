using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("Person")]
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }

        public Person()
        {
            Skills = new List<Skill>();
            Roles = new List<Roles>();
            Equipments = new List<Equipment>();
        }
        public virtual List<Roles> Roles { get; set; }
        public virtual List<Skill> Skills { get; set; }
        public virtual List<Equipment> Equipments { get; set; } 
    }

    public enum Gender
    {
        Male,
        Female
    }
}
