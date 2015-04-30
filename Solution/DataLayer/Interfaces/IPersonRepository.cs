using System.Collections;
using System.Collections.Generic;
using DataLayer.Components;

namespace DataLayer.Wrapper
{
    public interface IPersonRepository
    {
        void Create(Person person);
        void Update(Person person);
        bool Delete(Person person);
        Person GetPersonById(int personId);
        Person GetPersonByFirstName(string firstname);
        Person GetPersonByLastName(string lastname);
        Person GetPersonByMail(string mail);
        IList<Person> GetPersons();
    }
}
