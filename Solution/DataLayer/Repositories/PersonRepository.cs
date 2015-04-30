using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Repositories.Interfaces;
using Models.Entities;

namespace DataLayer.Repositories
{
    public class PersonRepository:IPersonRepository
    {
        private readonly IContextManager contextManager;

        public PersonRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        public void Create(Person person)
        {
            if (person == null) return;
            var context = contextManager.CurrentContext;
            context.Persons.Add(person);
            contextManager.Save(context);
        }

        public void Update(Person person)
        {
            if(person==null) return;
            var context = contextManager.CurrentContext;
            context.Entry(person).State = EntityState.Modified;
            contextManager.Save(context);
        }

        public bool Delete(Person person)
        {
            if (person == null) return false;
            var context = contextManager.CurrentContext;
            context.Entry(person).State = EntityState.Deleted;
            contextManager.Save(context);
            return true;
        }

        public Person GetPersonById(int personId)
        {
            return contextManager.CurrentContext.Persons.AsNoTracking().FirstOrDefault(p => p.Id == personId);
        }

        public Person GetPersonByFirstName(string firstname)
        {
            return contextManager.CurrentContext.Persons.AsNoTracking().FirstOrDefault(p => p.FirstName == firstname);
        }

        public Person GetPersonByLastName(string lastname)
        {
            return contextManager.CurrentContext.Persons.AsNoTracking().FirstOrDefault(p => p.LastName == lastname);
        }

        public Person GetPersonByMail(string mail)
        {
            return contextManager.CurrentContext.Persons.AsNoTracking().FirstOrDefault(p => p.Mail == mail);
        }

        public IList<Person> GetPersons()
        {
            return contextManager.CurrentContext.Persons.AsNoTracking().ToList();
        }

        
    }
}
