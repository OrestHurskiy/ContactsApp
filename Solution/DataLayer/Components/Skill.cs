using System;
using System.Collections.Generic;

namespace DataLayer.Components
{
    public class Skill
    {
        public int Id { get; set; }
        public string Development { get; set; }
        public string Certification { get; set; }
        public Degree Degree { get; set; }

        public Skill()
        {
            Persons = new List<Person>();
        }
        public virtual List<Person> Persons { get; set; } 
    }

    public enum Degree
    {
        Competent,
        Master,
        Professor
    }
}
