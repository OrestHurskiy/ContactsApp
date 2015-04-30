using System.Data.Entity;
using Models.Entities;

namespace DataLayer.Context
{
    public class ContactsInitializer:DropCreateDatabaseAlways<ContactsContext>
    {
        protected override void Seed(ContactsContext context)
        {
            var person1 = new Person() {FirstName = "Taras", Gender = Gender.Male};
            var person2 = new Person() {FirstName = "Bogdan", Gender = Gender.Male};
            var person3 = new Person() {FirstName = "Olga", Gender = Gender.Female};

            var project1 = new Project() {ProjectName = "ContactApp"};
            var project2 = new Project() {ProjectName = "StudiyngApp"};
            var project3 = new Project() {ProjectName = "Useless"};

            context.Persons.Add(person1);
            context.Persons.Add(person2);
            context.Persons.Add(person3);

            context.Projects.Add(project1);
            context.Projects.Add(project2);
            context.Projects.Add(project3);

            

            base.Seed(context);
        }
    }
}
