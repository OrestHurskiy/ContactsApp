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
    #region Person Interface

    
    public interface IPersonDataComponent
    {
        #region IPersonDataComponent Methods

        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        bool DetetePerson(Person person);
        List<Person> GetPersons();
        Person GetPersonByid(int id);
        Person GetPersonByFirstName(string firstname);
        Person GetPersonByLastName(string lastname);

        #endregion
    }

    #endregion
}
